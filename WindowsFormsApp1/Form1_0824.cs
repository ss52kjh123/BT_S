using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;  // 추가1
using System.Net; // 추가
using System.Net.Sockets;  // 추가
using System.IO;  // 추가
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace My_Client
{
    public partial class Form1 : Form
    {
        Slave_Set_Form slave_set_form;
        int cmd, ThreadBit = 1; byte Count = 1;
        public string IP1, IP2, IP3, IP4, IP, Port, HName, Mac;
        static string BT_Slave_Data = string.Empty;

        public byte[] Tcp_tx_data = new byte[50];
        public byte[] Tcp_tx_buf = new byte[50];
        public string sendData1;

        byte[] Tcp_rx_data = new byte[50];
        byte[] Tcp_rx_buf = new byte[50];

        string[] Slave_DataSplit = new string[50];
        string[] Slave_Mac = new string[254];
        int Slave_Mac_Length;

        NetworkStream stream;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Slave_Set_Form a)
        {
            InitializeComponent();
            slave_set_form = a;
        }


        public void cmd100()
        {
            cmd = 100;
            Tcp_tx_data[0] = 2; //STX
            Tcp_tx_data[1] = (byte)(Tcp_tx_buf.Length + 7); //Data Length
            Tcp_tx_data[2] = Count; //Count
            Tcp_tx_data[3] = 100; //Cmd
            for (int i = 0; i < Tcp_tx_buf.Length; i++)
                Tcp_tx_data[i + 4] = Tcp_tx_buf[i];
            Tcp_tx_data[Tcp_tx_buf.Length + 4] = 13; //\r
            Tcp_tx_data[Tcp_tx_buf.Length + 5] = 10; //\n
            Tcp_tx_data[(byte)(Tcp_tx_data[1] - 1)] = 3; //ETX
        }

        private void Slave_Add_btn_Click(object sender, EventArgs e) // Slave Setting Form 열기
        {
            cmd = 100;
            sendData1 = "AT+ADDR?";  //폼 열때 Mac Address를 Mac 변수에 저장
            Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(sendData1);
            cmd100();
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
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));  // IP주소와 Port번호를 할당
            tcpClient1.Connect(ipEnd);  // 서버에 연결 요청
            writeRichTextbox("서버 연결됨...");


            stream = tcpClient1.GetStream();
 
            while (tcpClient1.Connected)  // 클라이언트가 연결되어 있는 동안
            {
                stream.Read(Tcp_rx_buf, 0, Tcp_rx_buf.Length); //Data Receive
                int rx_Length=0;
                if (cmd ==2)
                    rx_Length = 16;
                else
                    rx_Length = ASCIIEncoding.ASCII.GetString(Tcp_rx_buf).IndexOf("\0"); // Receive Data Length

                for (int i = 0; i < rx_Length - 6; i++)
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
            stream.Write(a, 0, a.Length);
            Tcp_tx_data[2] = Count++;
        }

        private void Sendbtn_Click(object sender, EventArgs e)  // '보내기' 버튼이 클릭되면
        {
            sendData1 = textBox3.Text; // testBox3 의 내용을 sendData1 변수에 저장
            Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(sendData1);
           Parsing(Tcp_tx_buf);

            if (cmd == 100)
            {
                Tcp_tx_data[0] = 2; //STX
                Tcp_tx_data[1] = (byte)(Tcp_tx_buf.Length + 7); //Data Length
                Tcp_tx_data[2] = Count; //Count
                Tcp_tx_data[3] = 100; //Cmd
                for (int i = 0; i < Tcp_tx_buf.Length; i++)
                    Tcp_tx_data[i + 4] = Tcp_tx_buf[i];
                Tcp_tx_data[Tcp_tx_buf.Length + 4] = 13; //\r
                Tcp_tx_data[Tcp_tx_buf.Length + 5] = 10; //\n
                Tcp_tx_data[Tcp_tx_buf.Length + 6] = 3; //ETX
                DataWrite(Tcp_tx_data);
            }
        }

        private void Cmd1btn_Click(object sender, EventArgs e) //Cmd 1. TCP Open?  
        {
            cmd = 1;

            Tcp_tx_data[0] = 2; //STX
            Tcp_tx_data[1] = 5; //Data Length
            Tcp_tx_data[2] = Count; //Count
            Tcp_tx_data[3] = 1; //Cmd
            Tcp_tx_data[4] = 3; //ETX
            DataWrite(Tcp_tx_data);
        }


        private void Cmd3btn_Click(object sender, EventArgs e) //Cmd 3. Serial Number Request
        {
            cmd = 3;

            Tcp_tx_data[0] = 2; //STX
            Tcp_tx_data[1] = 5; //Data Length
            Tcp_tx_data[2] = Count; //Count
            Tcp_tx_data[3] = 3; //Cmd
            Tcp_tx_data[4] = 3; //ETX
            DataWrite(Tcp_tx_data);
        }

        private void cmd10Startbtn_Click(object sender, EventArgs e) //Cmd 10. Get Joystick Value Start
        {
            cmd = 10;
            ThreadBit = 1;
            Tcp_tx_data[0] = 2; //STX
            Tcp_tx_data[1] = 5; //Data Length
            Tcp_tx_data[2] = Count; //Count
            Tcp_tx_data[3] = 10; //Cmd
            Tcp_tx_data[4] = 3; //ETX
            DataWrite(Tcp_tx_data);

            ThreadPool.QueueUserWorkItem((_) => {
                while (ThreadBit == 1)
                {
                    while (Tcp_rx_buf[2] == Count - 1) //응답받고 보냄
                    {
                        DataWrite(Tcp_tx_data);
                        Thread.Sleep(1000);
                    }

                }
            });
        }

        private void cmd10Stopbtn_Click(object sender, EventArgs e) //Cmd 10. Get Joystick Value Stop
        {
            ThreadBit = 0;
            cmd = 1;
        }

        private void Cmd100btn_Click(object sender, EventArgs e)//Cmd 100. AT Setting Mode
        {
            cmd = 100;
            Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes("AT");
            cmd100();
            DataWrite(Tcp_tx_data);

        }

        
        

        private void Loadbtn_Click(object sender, EventArgs e)
        {
            string line; int num = 0;
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

        private void Mac_Sendbtn_Click(object sender, EventArgs e)
        {
            
            cmd = 100;
            Tcp_tx_data[0] = 2; //STX
            Tcp_tx_data[1] = (byte)Convert.ToInt16((Slave_Mac_Length * 12) + 5); //Data Length
            Tcp_tx_data[2] = Count; //Count
            Tcp_tx_data[3] = 100; //Cmd

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
/*
 *                    ※개발※
 * 1.Mac 프로토콜
 * 1-1. 폼간 데이터 전달(Count)
 * 2.세이브창 따로빼서 저장누르면 콜백으로 그리드 뷰에 데이터 바로업데이트 
 * 2-2.세이브창에서 데이터쓸때 제한검사
 * 2-3 save 누르면 리치박스에 정보 표시
 * 3. 데이터 고유 id부여, 중간수정 가능?
 * 
 * 
 * 
 *                  ※수정사항※
 * 1.cmd 10 Thread 무조건 보내지말고 응답 받고 보내기 콜백으로 
 * 2.Tcp_tx_data 보내고 초기화(안해도됨)
 * 
 * 
 */ 