
namespace My_Client
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Sendbtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.IP1TB = new System.Windows.Forms.TextBox();
            this.IP2TB = new System.Windows.Forms.TextBox();
            this.IP3TB = new System.Windows.Forms.TextBox();
            this.IP4TB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.PortTB = new System.Windows.Forms.TextBox();
            this.Savebtn = new System.Windows.Forms.Button();
            this.Data_Sheetbtn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cmd10Startbtn = new System.Windows.Forms.Button();
            this.cmd10Stopbtn = new System.Windows.Forms.Button();
            this.Cmd1btn = new System.Windows.Forms.Button();
            this.Cmd3btn = new System.Windows.Forms.Button();
            this.Cmd100btn = new System.Windows.Forms.Button();
            this.Cmd2btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Mac_Sendbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DataNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 36);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 25);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(202, 36);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(89, 25);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(14, 276);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(277, 25);
            this.textBox3.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 98);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(277, 170);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(310, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 44);
            this.button1.TabIndex = 6;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Sendbtn
            // 
            this.Sendbtn.Location = new System.Drawing.Point(297, 276);
            this.Sendbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Sendbtn.Name = "Sendbtn";
            this.Sendbtn.Size = new System.Drawing.Size(86, 29);
            this.Sendbtn.TabIndex = 8;
            this.Sendbtn.Text = "Send";
            this.Sendbtn.UseVisualStyleBackColor = true;
            this.Sendbtn.Click += new System.EventHandler(this.Sendbtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(534, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = ".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "1-TCP Open?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "2-IP,Port setting";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(171, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "3-Serial Number Request";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(307, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "100-AT Setting Mode";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "Receive Data";
            // 
            // IP1TB
            // 
            this.IP1TB.Location = new System.Drawing.Point(431, 125);
            this.IP1TB.Name = "IP1TB";
            this.IP1TB.Size = new System.Drawing.Size(50, 25);
            this.IP1TB.TabIndex = 13;
            // 
            // IP2TB
            // 
            this.IP2TB.Location = new System.Drawing.Point(487, 125);
            this.IP2TB.Name = "IP2TB";
            this.IP2TB.Size = new System.Drawing.Size(50, 25);
            this.IP2TB.TabIndex = 13;
            // 
            // IP3TB
            // 
            this.IP3TB.Location = new System.Drawing.Point(543, 125);
            this.IP3TB.Name = "IP3TB";
            this.IP3TB.Size = new System.Drawing.Size(50, 25);
            this.IP3TB.TabIndex = 13;
            // 
            // IP4TB
            // 
            this.IP4TB.Location = new System.Drawing.Point(599, 125);
            this.IP4TB.Name = "IP4TB";
            this.IP4TB.Size = new System.Drawing.Size(50, 25);
            this.IP4TB.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(478, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = ".";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(590, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = ".";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(650, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 15);
            this.label12.TabIndex = 11;
            this.label12.Text = ":";
            // 
            // PortTB
            // 
            this.PortTB.Location = new System.Drawing.Point(664, 125);
            this.PortTB.Name = "PortTB";
            this.PortTB.Size = new System.Drawing.Size(58, 25);
            this.PortTB.TabIndex = 13;
            // 
            // Savebtn
            // 
            this.Savebtn.Font = new System.Drawing.Font("굴림", 8F);
            this.Savebtn.Location = new System.Drawing.Point(738, 287);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Size = new System.Drawing.Size(75, 23);
            this.Savebtn.TabIndex = 14;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = true;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // Data_Sheetbtn
            // 
            this.Data_Sheetbtn.Location = new System.Drawing.Point(740, 12);
            this.Data_Sheetbtn.Name = "Data_Sheetbtn";
            this.Data_Sheetbtn.Size = new System.Drawing.Size(85, 30);
            this.Data_Sheetbtn.TabIndex = 15;
            this.Data_Sheetbtn.Text = "Next>>";
            this.Data_Sheetbtn.UseVisualStyleBackColor = true;
            this.Data_Sheetbtn.Click += new System.EventHandler(this.Data_Sheetbtn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(307, 196);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 15);
            this.label13.TabIndex = 11;
            this.label13.Text = "10-Get Joystick Value";
            // 
            // cmd10Startbtn
            // 
            this.cmd10Startbtn.Location = new System.Drawing.Point(488, 192);
            this.cmd10Startbtn.Name = "cmd10Startbtn";
            this.cmd10Startbtn.Size = new System.Drawing.Size(75, 23);
            this.cmd10Startbtn.TabIndex = 16;
            this.cmd10Startbtn.Text = "Start";
            this.cmd10Startbtn.UseVisualStyleBackColor = true;
            this.cmd10Startbtn.Click += new System.EventHandler(this.cmd10Startbtn_Click);
            // 
            // cmd10Stopbtn
            // 
            this.cmd10Stopbtn.Location = new System.Drawing.Point(591, 192);
            this.cmd10Stopbtn.Name = "cmd10Stopbtn";
            this.cmd10Stopbtn.Size = new System.Drawing.Size(75, 23);
            this.cmd10Stopbtn.TabIndex = 17;
            this.cmd10Stopbtn.Text = "Stop";
            this.cmd10Stopbtn.UseVisualStyleBackColor = true;
            this.cmd10Stopbtn.Click += new System.EventHandler(this.cmd10Stopbtn_Click);
            // 
            // Cmd1btn
            // 
            this.Cmd1btn.Location = new System.Drawing.Point(431, 94);
            this.Cmd1btn.Name = "Cmd1btn";
            this.Cmd1btn.Size = new System.Drawing.Size(75, 23);
            this.Cmd1btn.TabIndex = 18;
            this.Cmd1btn.Text = "Send";
            this.Cmd1btn.UseVisualStyleBackColor = true;
            this.Cmd1btn.Click += new System.EventHandler(this.Cmd1btn_Click);
            // 
            // Cmd3btn
            // 
            this.Cmd3btn.Location = new System.Drawing.Point(488, 160);
            this.Cmd3btn.Name = "Cmd3btn";
            this.Cmd3btn.Size = new System.Drawing.Size(75, 23);
            this.Cmd3btn.TabIndex = 19;
            this.Cmd3btn.Text = "Send";
            this.Cmd3btn.UseVisualStyleBackColor = true;
            this.Cmd3btn.Click += new System.EventHandler(this.Cmd3btn_Click);
            // 
            // Cmd100btn
            // 
            this.Cmd100btn.Location = new System.Drawing.Point(488, 225);
            this.Cmd100btn.Name = "Cmd100btn";
            this.Cmd100btn.Size = new System.Drawing.Size(75, 23);
            this.Cmd100btn.TabIndex = 20;
            this.Cmd100btn.Text = "Set";
            this.Cmd100btn.UseVisualStyleBackColor = true;
            this.Cmd100btn.Click += new System.EventHandler(this.Cmd100btn_Click);
            // 
            // Cmd2btn
            // 
            this.Cmd2btn.Location = new System.Drawing.Point(728, 127);
            this.Cmd2btn.Name = "Cmd2btn";
            this.Cmd2btn.Size = new System.Drawing.Size(75, 23);
            this.Cmd2btn.TabIndex = 21;
            this.Cmd2btn.Text = "Set";
            this.Cmd2btn.UseVisualStyleBackColor = true;
            this.Cmd2btn.Click += new System.EventHandler(this.Cmd2btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(620, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Slave Info Save";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(600, 263);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 15);
            this.label14.TabIndex = 11;
            this.label14.Text = "Send Mac Address";
            // 
            // Mac_Sendbtn
            // 
            this.Mac_Sendbtn.Font = new System.Drawing.Font("굴림", 8F);
            this.Mac_Sendbtn.Location = new System.Drawing.Point(738, 259);
            this.Mac_Sendbtn.Name = "Mac_Sendbtn";
            this.Mac_Sendbtn.Size = new System.Drawing.Size(75, 23);
            this.Mac_Sendbtn.TabIndex = 22;
            this.Mac_Sendbtn.Text = "Send";
            this.Mac_Sendbtn.UseVisualStyleBackColor = true;
            this.Mac_Sendbtn.Click += new System.EventHandler(this.Mac_Sendbtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataNo,
            this.DataName,
            this.DataIP,
            this.DataPort,
            this.DataMac});
            this.dataGridView1.Location = new System.Drawing.Point(14, 367);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(480, 150);
            this.dataGridView1.TabIndex = 23;
            // 
            // DataNo
            // 
            this.DataNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataNo.HeaderText = "No.";
            this.DataNo.MinimumWidth = 6;
            this.DataNo.Name = "DataNo";
            this.DataNo.Width = 59;
            // 
            // DataName
            // 
            this.DataName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataName.HeaderText = "Name";
            this.DataName.MinimumWidth = 6;
            this.DataName.Name = "DataName";
            this.DataName.Width = 72;
            // 
            // DataIP
            // 
            this.DataIP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataIP.HeaderText = "IP Address";
            this.DataIP.MinimumWidth = 6;
            this.DataIP.Name = "DataIP";
            this.DataIP.Width = 107;
            // 
            // DataPort
            // 
            this.DataPort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataPort.HeaderText = "Port";
            this.DataPort.MinimumWidth = 6;
            this.DataPort.Name = "DataPort";
            this.DataPort.Width = 63;
            // 
            // DataMac
            // 
            this.DataMac.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataMac.HeaderText = "Mac Address";
            this.DataMac.MinimumWidth = 6;
            this.DataMac.Name = "DataMac";
            this.DataMac.Width = 112;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 597);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Mac_Sendbtn);
            this.Controls.Add(this.Cmd2btn);
            this.Controls.Add(this.Cmd100btn);
            this.Controls.Add(this.Cmd3btn);
            this.Controls.Add(this.Cmd1btn);
            this.Controls.Add(this.cmd10Stopbtn);
            this.Controls.Add(this.cmd10Startbtn);
            this.Controls.Add(this.Data_Sheetbtn);
            this.Controls.Add(this.Savebtn);
            this.Controls.Add(this.PortTB);
            this.Controls.Add(this.IP4TB);
            this.Controls.Add(this.IP3TB);
            this.Controls.Add(this.IP2TB);
            this.Controls.Add(this.IP1TB);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Sendbtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "클라이언트 Form";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Sendbtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox IP1TB;
        private System.Windows.Forms.TextBox IP2TB;
        private System.Windows.Forms.TextBox IP3TB;
        private System.Windows.Forms.TextBox IP4TB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox PortTB;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.Button Data_Sheetbtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button cmd10Startbtn;
        private System.Windows.Forms.Button cmd10Stopbtn;
        private System.Windows.Forms.Button Cmd1btn;
        private System.Windows.Forms.Button Cmd3btn;
        private System.Windows.Forms.Button Cmd100btn;
        private System.Windows.Forms.Button Cmd2btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button Mac_Sendbtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataMac;
    }
}

