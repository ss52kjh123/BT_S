namespace My_Client
{
    partial class Slave_Set_Form
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
            this.Savebtn = new System.Windows.Forms.Button();
            this.IPTB = new System.Windows.Forms.MaskedTextBox();
            this.PortTB = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Savebtn
            // 
            this.Savebtn.Location = new System.Drawing.Point(260, 253);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Size = new System.Drawing.Size(110, 36);
            this.Savebtn.TabIndex = 0;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = true;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // IPTB
            // 
            this.IPTB.Location = new System.Drawing.Point(131, 155);
            this.IPTB.Mask = "###.###.###.###";
            this.IPTB.Name = "IPTB";
            this.IPTB.Size = new System.Drawing.Size(122, 25);
            this.IPTB.TabIndex = 1;
            // 
            // PortTB
            // 
            this.PortTB.Location = new System.Drawing.Point(262, 155);
            this.PortTB.Mask = "####";
            this.PortTB.Name = "PortTB";
            this.PortTB.Size = new System.Drawing.Size(42, 25);
            this.PortTB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = ":";
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(131, 80);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(173, 25);
            this.NameTB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "IP,Port";
            // 
            // Slave_Set_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 318);
            this.Controls.Add(this.NameTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortTB);
            this.Controls.Add(this.IPTB);
            this.Controls.Add(this.Savebtn);
            this.Name = "Slave_Set_Form";
            this.Text = "Slave_Set_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.MaskedTextBox IPTB;
        private System.Windows.Forms.MaskedTextBox PortTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}