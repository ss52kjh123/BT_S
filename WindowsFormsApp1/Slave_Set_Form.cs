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
        Main_Form frm1;
        public string BT_Slave_Data, IP, Port, Name;
        public string[] Split_IP = new string[4]; 
        public Slave_Set_Form()
        {
            InitializeComponent();
        }
        public Slave_Set_Form(Main_Form a)
        {
            InitializeComponent();
            frm1 = a;
        }
        
        private void Savebtn_Click(object sender, EventArgs e)
        {
            
            IP = IPTB.Text;
            Split_IP = IP.Split('.'); //IP분리
            Port = PortTB.Text;
            //Name = NameTB.Text;
           

            //frm1.sendData1 = "AT+NAME="+Name;
            //frm1.Tcp_tx_buf = ASCIIEncoding.ASCII.GetBytes(frm1.sendData1);
            frm1.cmd = 100;
            frm1.Cmd_Data_Insert();
            frm1.DataWrite(frm1.Tcp_tx_data);
            Thread.Sleep(3000);

            frm1.Slave_ReceivedData = Split_IP; //Main_Form으로 Split_IP 전달

            frm1.cmd = 2;
            frm1.Cmd_Data_Insert();
            
            frm1.DataWrite(frm1.Tcp_tx_data);

            BT_Slave_Data = Name + "," + IP + "," + Port + "," + frm1.Mac;
            StreamWriter FS = new StreamWriter(".mac.txt", true);
            FS.WriteLine(BT_Slave_Data);
            FS.Close();
            

            this.Close();
        }
    }
}
