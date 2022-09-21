using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets; 
using System.IO; 
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace My_Client
{
    public partial class Main_Form : Form
    {
        Slave_Set_Form slave_set_form;
        public string[] Slave_ReceivedData = new string[4]; //Slave_Set_Form에서 Split_IP 전달받을 곳
        public int cmd=1; public int ThreadBit = 0; public byte Count = 0;
        public string IP1, IP2, IP3, IP4, IP, Port, HName, Mac;
        static string BT_Slave_Data = string.Empty;

        public byte[] Tcp_tx_data = new byte[50];
        public byte[] Tcp_tx_buf = new byte[50];
        public string sendData1;

        public byte[] Tcp_rx_data = new byte[50];
        public  byte[] Tcp_rx_buf = new byte[50];

        string[] Slave_DataSplit = new string[50];
        string[] Slave_Mac = new string[254];
        int Slave_Mac_Length;
        
        NetworkStream stream;
        public Main_Form()
        {
            InitializeComponent();
        }
        public Main_Form(Slave_Set_Form a)
        {
            InitializeComponent();
            slave_set_form = a;
        }

        public void Cmd_Data_Insert() //Cmd에 따른 데이터 프로토콜
        {
            switch(cmd)
            {
                case 1: //Cmd 1. TCP Connection Check
                case 3: //Cmd 3. Serial Number Request
                case 10: //Cmd 10. Get Joystick Value
                    Tcp_tx_data[0] = 2; //STX
                    Tcp_tx_data[1] = 5; //Data Length
                    Tcp_tx_data[2] = Count; //Count
                    Tcp_tx_data[3] = (byte)cmd;  //Cmd
                    Tcp_tx_data[4] = 3; //ETX
                    break;

                case 2: //IP,Port Set
                    Tcp_tx_data[0] = 2; //STX
                    Tcp_tx_data[1] = 15; //Data Length
                    Tcp_tx_data[2] = Count; //Count
                    Tcp_tx_data[3] = (byte)cmd; //Cmd
                    Tcp_tx_data[4] = (byte)Convert.ToInt16(Slave_ReceivedData[0]); //IP1
                    Tcp_tx_data[5] = 0;
                    Tcp_tx_data[6] = (byte)Convert.ToInt16(Slave_ReceivedData[1]); //IP2
                    Tcp_tx_data[7] = 0;
                    Tcp_tx_data[8] = (byte)Convert.ToInt16(Slave_ReceivedData[2]); //IP3
                    Tcp_tx_data[9] = 0;
                    Tcp_tx_data[10] = (byte)Convert.ToInt16(Slave_ReceivedData[3]);//IP4
                    Tcp_tx_data[11] = 0;
                    Tcp_tx_data[12] = (byte)Convert.ToInt16(Port); // Port = (Tcp_tx_buf[13] << 8) + Tcp_tx_buf[12]
                    Tcp_tx_data[13] = (byte)(Convert.ToInt16(Port) >> 8);
                    Tcp_tx_data[14] = 3; //ETX
                    break;

                case 100: //Cmd 100. AT Setting Mode
                    Tcp_tx_data[0] = 2; //STX
                    Tcp_tx_data[1] = (byte)(Tcp_tx_buf.Length + 7); //Data Length
                    Tcp_tx_data[2] = Count; //Count
                    Tcp_tx_data[3] = (byte)cmd; //Cmd
                    for (int i = 0; i < Tcp_tx_buf.Length; i++)
                        Tcp_tx_data[i + 4] = Tcp_tx_buf[i];
                    Tcp_tx_data[Tcp_tx_data[1] - 3] = 13; //\r
                    Tcp_tx_data[Tcp_tx_data[1] - 2] = 10; //\n
                    Tcp_tx_data[Tcp_tx_data[1] - 1] = 3; //ETX
                    break;
            }
        }

        private void Slave_Add_btn_Click(object sender, EventArgs e) // Slave Setting Form 열기
        {
            cmd = 100;
            sendData1 = "AT+ADDR?";  //폼 열때 Mac Address를 Mac 변수에 저장
            Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(sendData1);
            Cmd_Data_Insert();
            DataWrite(Tcp_tx_data);
            Slave_Set_Form slave_Set_Form = new Slave_Set_Form(this);
            slave_Set_Form.ShowDialog();
            

        }

        public static void Parsing(byte[] b) // Send Data Parsing
        {
            for (int i = 0; i < b.Length; i++)
            {
                string s = b[i].ToString();
                if (Convert.ToInt32(s) >= 48 && Convert.ToInt32(s) <= 57)
                {
                    int result = Convert.ToInt32(s) - 48;
                    b[i] = (byte)result;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)  //'연결하기' 버튼이 클릭되면
        {
            Thread thread1 = new Thread(connect);  // Thread 객채 생성, Form과는 별도 쓰레드에서 connect 함수가 실행됨.
            thread1.IsBackground = true;  // Form이 종료되면 thread1도 종료.
            thread1.Start();  // thread1 시작.
        }

        private void connect()  // thread1에 연결된 함수. 메인폼과는 별도로 동작한다.
        {
            TcpClient tcpClient1 = new TcpClient();  // TcpClient 객체 생성
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ConectIPTB.Text), int.Parse(ConectPortTB.Text));  // IP주소와 Port번호를 할당
            tcpClient1.Connect(ipEnd);  // 서버에 연결 요청
            writeRichTextbox("서버 연결됨...");

            stream = tcpClient1.GetStream();
 
            while (tcpClient1.Connected)  // 클라이언트가 연결되어 있는 동안
            {
                stream.Read(Tcp_rx_buf, 0, Tcp_rx_buf.Length); //Data Receive
                int rx_Length=0;
                rx_Length = ASCIIEncoding.ASCII.GetString(Tcp_rx_buf).IndexOf("\0"); // Receive Data Length

                for (int i = 0; i < rx_Length - 6; i++) // 불필요한 데이터 없애기
                    Tcp_rx_data[i] = Tcp_rx_buf[i + 4];
                
                writeRichTextbox(ASCIIEncoding.ASCII.GetString(Tcp_rx_data)); //RichTextBox에 Receive Data 표시
                if(cmd==100)
                {
                    if (sendData1 == "AT+ADDR?")
                        Mac = ASCIIEncoding.ASCII.GetString(Tcp_rx_data, 6, 14);

                    if (sendData1 == "AT+NAME?")
                        HName = ASCIIEncoding.ASCII.GetString(Tcp_rx_data, 6, rx_Length - 17);
                }
            }
        }

        private void writeRichTextbox(string data)  // richTextbox1 에 쓰기 함수
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(data); }); //  데이타를 수신창에 표시, 반드시 invoke 사용. 충돌피함.
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); });  // 스크롤을 젤 밑으로.
        }

        public void DataWrite(byte[] a) // Data Send
        {
            Tcp_tx_data[2] = ++Count;
            stream.Write(a, 0, a.Length);
            
        }

        private void Sendbtn_Click(object sender, EventArgs e)  // '보내기' 버튼이 클릭되면
        {
            sendData1 = ATTB.Text; // testBox3 의 내용을 sendData1 변수에 저장
            Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(sendData1);
           Parsing(Tcp_tx_buf);

            if (cmd == 100)
            {
                Cmd_Data_Insert();
                DataWrite(Tcp_tx_data);
            }
        }

        private void Cmd1btn_Click(object sender, EventArgs e) //Cmd 1. TCP Connection Check  
        {
            cmd = 1;
            Cmd_Data_Insert();
            DataWrite(Tcp_tx_data);
        }

        private void Cmd3btn_Click(object sender, EventArgs e) //Cmd 3. Serial Number Request
        {
            cmd = 3;
            Cmd_Data_Insert();
            DataWrite(Tcp_tx_data);
        }

        private void cmd10Startbtn_Click(object sender, EventArgs e) //Cmd 10. Get Joystick Value Start
        {
            Joystick_Monitoring_Form joystick_Monitoring_Form = new Joystick_Monitoring_Form(this);
            joystick_Monitoring_Form.Show();
        }

        private void Cmd100btn_Click(object sender, EventArgs e)//Cmd 100. AT Setting Mode
        {
            cmd = 100;
            Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes("AT"); //연결확인
            Cmd_Data_Insert();
            DataWrite(Tcp_tx_data);
            ATTB.Visible = true;
            Sendbtn.Visible = true;
        }

        private void Loadbtn_Click(object sender, EventArgs e)// 저장된 Slave Mac data Load
        {
            string line;
            int num = 0; //Slave 개수
            string[] FullData = new string[254];
           
            
            StreamReader FS = new StreamReader(".mac.txt");
            
            while ((line = FS.ReadLine()) != null)
            {
                FullData[num] = num + "," + line;
                num++; 
            }
            FS.Close();

            for (int i = 0; i < num; i++)
            {
                Slave_DataSplit = FullData[i].Split(',');
                Slave_Mac[i] = Slave_DataSplit[4]; //각 Slave_Mac Data만 저장
                dataGridView1.Rows.Add(Slave_DataSplit); //DataSheet에 삽입
            }
            Slave_Mac_Length = num;
        }

        private void Mac_Sendbtn_Click(object sender, EventArgs e) //Master로 Slave Mac 보내기
        {
            cmd = 100;
            Cmd_Data_Insert();
            //Tcp_tx_data[0] = 2; //STX
            Tcp_tx_data[1] = (byte)Convert.ToInt16((Slave_Mac_Length * 12) + 5); //Data Length
            //Tcp_tx_data[2] = Count; //Count
            //Tcp_tx_data[3] = 100; //Cmd

            for (int i = 0; i < Slave_Mac_Length; i++)
            {
                Slave_Mac[i] = Slave_Mac[i].Replace(":", "");
                Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(Slave_Mac[i]);
                Parsing(Tcp_tx_buf);

                for (int j = 0; j < Tcp_tx_buf.Length; j++)
                    Tcp_tx_data[j + 4 + (i * 12)] = Tcp_tx_buf[j];
            }
            Tcp_tx_data[(byte)(Tcp_tx_data[1] - 1)] = 3; //ETX
            DataWrite(Tcp_tx_data);
        }
    }
}
