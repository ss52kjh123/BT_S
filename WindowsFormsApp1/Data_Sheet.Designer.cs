namespace My_Client
{
    partial class Data_Sheet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PREVbtn = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Loadbtn = new System.Windows.Forms.Button();
            this.DataNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // PREVbtn
            // 
            this.PREVbtn.Location = new System.Drawing.Point(419, 24);
            this.PREVbtn.Name = "PREVbtn";
            this.PREVbtn.Size = new System.Drawing.Size(91, 35);
            this.PREVbtn.TabIndex = 0;
            this.PREVbtn.Text = "<<PREV";
            this.PREVbtn.UseVisualStyleBackColor = true;
            this.PREVbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataNo,
            this.DataName,
            this.DataIP,
            this.DataPort,
            this.DataMac});
            this.DataGridView1.Location = new System.Drawing.Point(44, 84);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersWidth = 51;
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.Size = new System.Drawing.Size(474, 325);
            this.DataGridView1.TabIndex = 1;
            // 
            // Loadbtn
            // 
            this.Loadbtn.Location = new System.Drawing.Point(313, 24);
            this.Loadbtn.Name = "Loadbtn";
            this.Loadbtn.Size = new System.Drawing.Size(100, 35);
            this.Loadbtn.TabIndex = 2;
            this.Loadbtn.Text = "Load";
            this.Loadbtn.UseVisualStyleBackColor = true;
            this.Loadbtn.Click += new System.EventHandler(this.Loadbtn_Click);
            // 
            // DataNo
            // 
            this.DataNo.HeaderText = "No.";
            this.DataNo.MinimumWidth = 6;
            this.DataNo.Name = "DataNo";
            this.DataNo.Width = 59;
            // 
            // DataName
            // 
            this.DataName.HeaderText = "Name";
            this.DataName.MinimumWidth = 6;
            this.DataName.Name = "DataName";
            this.DataName.Width = 72;
            // 
            // DataIP
            // 
            this.DataIP.HeaderText = "IP Address";
            this.DataIP.MinimumWidth = 6;
            this.DataIP.Name = "DataIP";
            this.DataIP.Width = 107;
            // 
            // DataPort
            // 
            this.DataPort.HeaderText = "Port";
            this.DataPort.MinimumWidth = 6;
            this.DataPort.Name = "DataPort";
            this.DataPort.Width = 63;
            // 
            // DataMac
            // 
            this.DataMac.HeaderText = "Mac Address";
            this.DataMac.MinimumWidth = 6;
            this.DataMac.Name = "DataMac";
            this.DataMac.Width = 112;
            // 
            // Data_Sheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 450);
            this.Controls.Add(this.Loadbtn);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.PREVbtn);
            this.Name = "Data_Sheet";
            this.Text = "Data_Sheet";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PREVbtn;
        private System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.Button Loadbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataMac;
    }
}