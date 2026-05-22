namespace ClienteWinForms
{
    partial class FormIp
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
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            txtIp = new TextBox();
            btnLogin = new Button();
            panelBottom = new Panel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 60, 60);
            panel1.Controls.Add(pictureBox2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(584, 51);
            panel1.TabIndex = 7;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.ChatGPT_Image_21_de_mai__de_2026__17_28_39;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(-55, -30);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(245, 117);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(98, 259);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 8;
            label1.Text = "Digite o IP do Servidor:";
            label1.Click += label1_Click;
            // 
            // txtIp
            // 
            txtIp.BackColor = Color.FromArgb(30, 55, 60);
            txtIp.BorderStyle = BorderStyle.None;
            txtIp.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIp.ForeColor = Color.White;
            txtIp.Location = new Point(244, 259);
            txtIp.Margin = new Padding(4, 3, 4, 3);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(222, 16);
            txtIp.TabIndex = 9;
            txtIp.TextChanged += txtIp_TextChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(30, 60, 60);
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(224, 296);
            btnLogin.Margin = new Padding(4, 3, 4, 3);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(103, 44);
            btnLogin.TabIndex = 10;
            btnLogin.Text = "Conectar";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(30, 60, 60);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 382);
            panelBottom.Margin = new Padding(4, 3, 4, 3);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(584, 29);
            panelBottom.TabIndex = 11;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.ChatGPT_Image_21_de_mai__de_2026__12_42_39;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(165, 73);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(233, 180);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // FormIp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(584, 411);
            Controls.Add(pictureBox1);
            Controls.Add(panelBottom);
            Controls.Add(btnLogin);
            Controls.Add(txtIp);
            Controls.Add(label1);
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormIp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormIp";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button btnLogin;
        private Panel panelBottom;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}