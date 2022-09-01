using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Client
{
    public partial class Slave_Set_Form : Form
    {
        Form1 frm1;
        public string BT_Slave_Data, IP, Port, Name;
        public string[] Split_IP = new string[4]; 
        public Slave_Set_Form()
        {
            InitializeComponent();
        }
        public Slave_Set_Form(Form1 a)
        {
            InitializeComponent();
            frm1 = a;
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            
            IP = IPTB.Text;
            Split_IP = IP.Split('.');
            Port = PortTB.Text;
            Name = NameTB.Text;
           

            frm1.sendData1 = "AT+NAME="+Name;
            frm1.Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(frm1.sendData1);
            frm1.cmd100();
            frm1.DataWrite(frm1.Tcp_tx_data);
            Thread.Sleep(3000);

            frm1.Tcp_tx_data[0] = 2; //STX
            frm1.Tcp_tx_data[1] = 15; //Data Length
            frm1.Tcp_tx_data[2] = 1; //Count
            frm1.Tcp_tx_data[3] = 2; //Cmd
            frm1.Tcp_tx_data[4] = (byte)Convert.ToInt16(Split_IP[0]); //IP1
            frm1.Tcp_tx_data[5] = 0;
            frm1.Tcp_tx_data[6] = (byte)Convert.ToInt16(Split_IP[1]); //IP2
            frm1.Tcp_tx_data[7] = 0;
            frm1.Tcp_tx_data[8] = (byte)Convert.ToInt16(Split_IP[2]); //IP3
            frm1.Tcp_tx_data[9] = 0;
            frm1.Tcp_tx_data[10] = (byte)Convert.ToInt16(Split_IP[3]);//IP4
            frm1.Tcp_tx_data[11] = 0;
            frm1.Tcp_tx_data[12] = (byte)Convert.ToInt16(Port); // Port=(Tcp_tx_buf[13] << 8) + Tcp_tx_buf[12]
            frm1.Tcp_tx_data[13] = (byte)(Convert.ToInt16(Port) >> 8);
            frm1.Tcp_tx_data[14] = 3; //ETX


            //frm1.cmd2();
            frm1.DataWrite(frm1.Tcp_tx_data);

            BT_Slave_Data = Name + "," + IP + "," + Port + "," + frm1.Mac;
            StreamWriter FS = new StreamWriter(".mac.txt", true);
            FS.WriteLine(BT_Slave_Data);
            FS.Close();
            

            this.Close();
        }
    }
}
