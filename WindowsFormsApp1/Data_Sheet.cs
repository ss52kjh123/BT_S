using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Client
{
    public partial class Data_Sheet : Form
    {

        
        string NO, Name, IP, Port, Mac = String.Empty;
        string BT_Slave_Data1 = string.Empty; //Main Form에서 받아올 데이터 변수
        string[] Slave_DataSplit;
        public Data_Sheet(string data)
        {
            InitializeComponent();
            BT_Slave_Data1 = data; //Main Form에서 넘어온 값 저장
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 ShowForm1 = new Form1(); 
            ShowForm1.ShowDialog(); //Main Form으로 넘어가기
        }

        private void Loadbtn_Click(object sender, EventArgs e)
        {
            string line; int count = 0;
            string[] FullData = new string[254];
            StreamReader FS = new StreamReader(".mac.txt");

            while((line = FS.ReadLine())!=null)
            {
                FullData[count] = line+","+count;
                count++;
            }
            FS.Close();


            for (int i = 0; i < count; i++)
            {
                Slave_DataSplit = FullData[i].Split(',');
                DataGridView1.Rows.Add(Slave_DataSplit); //DataSheet에 삽입
            }
            DataGridView1.Columns[4].DisplayIndex = 0; //NO. Columns삽입















            //string[] Slave_DataSplit  = BT_Slave_Data1.Split(',');

            //Name = Slave_DataSplit[0];
            //IP= Slave_DataSplit[1];
            //Port = Slave_DataSplit[2];
            //Mac = Slave_DataSplit[3];
            //DataGridView1.Rows.Add(1,Slave_DataSplit,1);



            //DataGridView1["DataName", 0].Value = Slave_DataSplit[0];
            //DataGridView1["DataIP", 0].Value = Slave_DataSplit[1];
            //DataGridView1["DataPort", 0].Value = Slave_DataSplit[2];
            //DataGridView1["DataMac", 0].Value = Slave_DataSplit[3];

        }

        
    }
}
