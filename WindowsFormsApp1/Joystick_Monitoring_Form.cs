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
    public partial class Joystick_Monitoring_Form : Form
    {
        Main_Form MForm;
        public Joystick_Monitoring_Form()
        {
            InitializeComponent();
        }
        public Joystick_Monitoring_Form(Main_Form c)
        {
            InitializeComponent();
            MForm = c;
        }

        private void visible(Control item)
        {
            if (item.InvokeRequired)
                item.Invoke(new MethodInvoker(delegate { item.Visible = true; }));
        }
        private void Invisible(Control item)
        {
            if (item.InvokeRequired)
                item.Invoke(new MethodInvoker(delegate { item.Visible = false; }));
        }

        void cmd10Thread() //joystick monitoring
        {
            while (MForm.Tcp_rx_buf[2] == MForm.Count) //응답받고 보냄
            {
                MForm.DataWrite(MForm.Tcp_tx_data);
                Thread.Sleep(1000);
                if (MForm.ThreadBit == 0)
                    break;
                
                if (MForm.Tcp_rx_buf[4]==1) //Pairing on
                {
                    visible(PairingBit_off);
                    if (MForm.Tcp_rx_buf[5] == 0) //Up off
                        Invisible(Up_on);
                    if (MForm.Tcp_rx_buf[6] == 0) //Down off
                        Invisible(Down_on);
                    if (MForm.Tcp_rx_buf[7] == 0) //Left off
                        Invisible(Left_on);
                    if (MForm.Tcp_rx_buf[8] == 0) //Right off
                        Invisible(Right_on);
                    if (MForm.Tcp_rx_buf[9] == 0) //EMO off
                        Invisible(EMO_off);

                    if (MForm.Tcp_rx_buf[5] == 1) //Up on
                        visible(Up_on);
                    if (MForm.Tcp_rx_buf[6] == 1) //Down on
                        visible(Down_on);
                    if (MForm.Tcp_rx_buf[7] == 1) //Left on
                        visible(Left_on);
                    if (MForm.Tcp_rx_buf[8] == 1) //Right on
                        visible(Right_on);
                    if (MForm.Tcp_rx_buf[9]==1) //EMO on
                        visible(EMO_on);
                }
            }
        }

        private void StartBtn2_Click(object sender, EventArgs e)
        {
            MForm.cmd = 10;
            MForm.ThreadBit = 1;
            MForm.Cmd_Data_Insert();
            MForm.DataWrite(MForm.Tcp_tx_data);

            Thread cmd10 = new Thread(new ThreadStart(cmd10Thread));
            if (MForm.ThreadBit == 1)
                cmd10.Start();
        }

        private void Stopbtn2_Click(object sender, EventArgs e)
        {
            MForm.ThreadBit = 0;
            MForm.cmd = 1;
            this.Close();
        }
    }
}

