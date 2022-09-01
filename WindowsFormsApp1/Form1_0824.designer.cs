
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
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmd10Startbtn = new System.Windows.Forms.Button();
            this.cmd10Stopbtn = new System.Windows.Forms.Button();
            this.Cmd1btn = new System.Windows.Forms.Button();
            this.Cmd3btn = new System.Windows.Forms.Button();
            this.Cmd100btn = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.Mac_Sendbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DataNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loadbtn = new System.Windows.Forms.Button();
            this.Slave_Add_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "TCP Connection Check";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Serial Number Request";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(307, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "AT Setting Mode";
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(307, 198);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 15);
            this.label13.TabIndex = 11;
            this.label13.Text = "Get Joystick Value";
            // 
            // cmd10Startbtn
            // 
            this.cmd10Startbtn.Location = new System.Drawing.Point(488, 184);
            this.cmd10Startbtn.Name = "cmd10Startbtn";
            this.cmd10Startbtn.Size = new System.Drawing.Size(105, 42);
            this.cmd10Startbtn.TabIndex = 16;
            this.cmd10Startbtn.Text = "Start";
            this.cmd10Startbtn.UseVisualStyleBackColor = true;
            this.cmd10Startbtn.Click += new System.EventHandler(this.cmd10Startbtn_Click);
            // 
            // cmd10Stopbtn
            // 
            this.cmd10Stopbtn.Location = new System.Drawing.Point(599, 184);
            this.cmd10Stopbtn.Name = "cmd10Stopbtn";
            this.cmd10Stopbtn.Size = new System.Drawing.Size(105, 42);
            this.cmd10Stopbtn.TabIndex = 17;
            this.cmd10Stopbtn.Text = "Stop";
            this.cmd10Stopbtn.UseVisualStyleBackColor = true;
            this.cmd10Stopbtn.Click += new System.EventHandler(this.cmd10Stopbtn_Click);
            // 
            // Cmd1btn
            // 
            this.Cmd1btn.Location = new System.Drawing.Point(488, 88);
            this.Cmd1btn.Name = "Cmd1btn";
            this.Cmd1btn.Size = new System.Drawing.Size(105, 42);
            this.Cmd1btn.TabIndex = 18;
            this.Cmd1btn.Text = "Check";
            this.Cmd1btn.UseVisualStyleBackColor = true;
            this.Cmd1btn.Click += new System.EventHandler(this.Cmd1btn_Click);
            // 
            // Cmd3btn
            // 
            this.Cmd3btn.Location = new System.Drawing.Point(488, 136);
            this.Cmd3btn.Name = "Cmd3btn";
            this.Cmd3btn.Size = new System.Drawing.Size(105, 42);
            this.Cmd3btn.TabIndex = 19;
            this.Cmd3btn.Text = "Send";
            this.Cmd3btn.UseVisualStyleBackColor = true;
            this.Cmd3btn.Click += new System.EventHandler(this.Cmd3btn_Click);
            // 
            // Cmd100btn
            // 
            this.Cmd100btn.Location = new System.Drawing.Point(488, 232);
            this.Cmd100btn.Name = "Cmd100btn";
            this.Cmd100btn.Size = new System.Drawing.Size(105, 42);
            this.Cmd100btn.TabIndex = 20;
            this.Cmd100btn.Text = "Set";
            this.Cmd100btn.UseVisualStyleBackColor = true;
            this.Cmd100btn.Click += new System.EventHandler(this.Cmd100btn_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(611, 488);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 15);
            this.label14.TabIndex = 11;
            this.label14.Text = "Send Mac Address";
            // 
            // Mac_Sendbtn
            // 
            this.Mac_Sendbtn.Font = new System.Drawing.Font("굴림", 8F);
            this.Mac_Sendbtn.Location = new System.Drawing.Point(500, 475);
            this.Mac_Sendbtn.Name = "Mac_Sendbtn";
            this.Mac_Sendbtn.Size = new System.Drawing.Size(105, 42);
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
            this.DataIP.Width = 99;
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
            // Loadbtn
            // 
            this.Loadbtn.Location = new System.Drawing.Point(500, 427);
            this.Loadbtn.Name = "Loadbtn";
            this.Loadbtn.Size = new System.Drawing.Size(105, 42);
            this.Loadbtn.TabIndex = 24;
            this.Loadbtn.Text = "Load";
            this.Loadbtn.UseVisualStyleBackColor = true;
            this.Loadbtn.Click += new System.EventHandler(this.Loadbtn_Click);
            // 
            // Slave_Add_btn
            // 
            this.Slave_Add_btn.Location = new System.Drawing.Point(691, 11);
            this.Slave_Add_btn.Name = "Slave_Add_btn";
            this.Slave_Add_btn.Size = new System.Drawing.Size(134, 58);
            this.Slave_Add_btn.TabIndex = 25;
            this.Slave_Add_btn.Text = "Add";
            this.Slave_Add_btn.UseVisualStyleBackColor = true;
            this.Slave_Add_btn.Click += new System.EventHandler(this.Slave_Add_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(582, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Slave Info Add";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 597);
            this.Controls.Add(this.Slave_Add_btn);
            this.Controls.Add(this.Loadbtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Mac_Sendbtn);
            this.Controls.Add(this.Cmd100btn);
            this.Controls.Add(this.Cmd3btn);
            this.Controls.Add(this.Cmd1btn);
            this.Controls.Add(this.cmd10Stopbtn);
            this.Controls.Add(this.cmd10Startbtn);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button cmd10Startbtn;
        private System.Windows.Forms.Button cmd10Stopbtn;
        private System.Windows.Forms.Button Cmd1btn;
        private System.Windows.Forms.Button Cmd3btn;
        private System.Windows.Forms.Button Cmd100btn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button Mac_Sendbtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataMac;
        private System.Windows.Forms.Button Loadbtn;
        private System.Windows.Forms.Button Slave_Add_btn;
        private System.Windows.Forms.Label label3;
    }
}

