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
        string[] Slave_DataSplit = new string[50];
        string[] Slave_Mac = new string[254];


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
                FullData[count] = count+","+line;
                count++;
            }
            FS.Close();


            for (int i = 0; i < count; i++)
            {
                Slave_DataSplit = FullData[i].Split(',');
                Slave_Mac[i] = Slave_DataSplit[3]; //각 Slave_Mac Data만 저장
                DataGridView1.Rows.Add(Slave_DataSplit); //DataSheet에 삽입
            }
            

            
        }

        
    }

    
}
