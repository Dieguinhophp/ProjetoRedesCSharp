namespace ClienteWinForms
{
    partial class FormChat
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
            rtbChat = new RichTextBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            panelTop = new Panel();
            pictureBox2 = new PictureBox();
            btnLogout = new Button();
            lblUsuario = new Label();
            panelBottom = new Panel();
            lstUsuarios = new ListBox();
            btnVideoCall = new Button();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.FromArgb(30, 55, 60);
            rtbChat.BorderStyle = BorderStyle.None;
            rtbChat.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbChat.ForeColor = Color.White;
            rtbChat.Location = new Point(14, 65);
            rtbChat.Margin = new Padding(4, 3, 4, 3);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbChat.Size = new Size(530, 305);
            rtbChat.TabIndex = 0;
            rtbChat.Text = "";
            rtbChat.TextChanged += rtbChat_TextChanged;
            // 
            // txtMensagem
            // 
            txtMensagem.BackColor = Color.FromArgb(30, 60, 60);
            txtMensagem.BorderStyle = BorderStyle.FixedSingle;
            txtMensagem.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMensagem.ForeColor = SystemColors.Window;
            txtMensagem.Location = new Point(14, 382);
            txtMensagem.Margin = new Padding(4, 3, 4, 3);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(392, 23);
            txtMensagem.TabIndex = 1;
            txtMensagem.TextChanged += txtMensagem_TextChanged;
            txtMensagem.KeyDown += txtMensagem_KeyDown;
            // 
            // btnEnviar
            // 
            btnEnviar.BackColor = Color.FromArgb(30, 60, 60);
            btnEnviar.FlatStyle = FlatStyle.Popup;
            btnEnviar.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEnviar.ForeColor = SystemColors.Window;
            btnEnviar.Location = new Point(456, 380);
            btnEnviar.Margin = new Padding(4, 3, 4, 3);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(88, 27);
            btnEnviar.TabIndex = 2;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(30, 60, 60);
            panelTop.Controls.Add(pictureBox2);
            panelTop.Controls.Add(btnLogout);
            panelTop.Controls.Add(lblUsuario);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(915, 58);
            panelTop.TabIndex = 3;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.ChatGPT_Image_21_de_mai__de_2026__17_28_39;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(-56, -26);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(245, 117);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(30, 55, 60);
            btnLogout.FlatStyle = FlatStyle.Popup;
            btnLogout.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(805, 14);
            btnLogout.Margin = new Padding(4, 3, 4, 3);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(88, 27);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Sair";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Constantia", 10.25F);
            lblUsuario.ForeColor = Color.White;
            lblUsuario.Location = new Point(196, 24);
            lblUsuario.Margin = new Padding(4, 0, 4, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(64, 17);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuário: ";
            lblUsuario.Click += lblUsuario_Click;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(30, 60, 60);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 503);
            panelBottom.Margin = new Padding(4, 3, 4, 3);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(915, 29);
            panelBottom.TabIndex = 4;
            // 
            // lstUsuarios
            // 
            lstUsuarios.BackColor = Color.FromArgb(30, 55, 60);
            lstUsuarios.BorderStyle = BorderStyle.None;
            lstUsuarios.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstUsuarios.ForeColor = Color.White;
            lstUsuarios.FormattingEnabled = true;
            lstUsuarios.Location = new Point(753, 65);
            lstUsuarios.Margin = new Padding(4, 3, 4, 3);
            lstUsuarios.Name = "lstUsuarios";
            lstUsuarios.Size = new Size(140, 300);
            lstUsuarios.TabIndex = 5;
            // 
            // btnVideoCall
            // 
            btnVideoCall.BackColor = Color.FromArgb(30, 60, 60);
            btnVideoCall.FlatStyle = FlatStyle.Popup;
            btnVideoCall.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVideoCall.ForeColor = SystemColors.Window;
            btnVideoCall.Location = new Point(753, 382);
            btnVideoCall.Margin = new Padding(4, 3, 4, 3);
            btnVideoCall.Name = "btnVideoCall";
            btnVideoCall.Size = new Size(140, 25);
            btnVideoCall.TabIndex = 6;
            btnVideoCall.Text = "Chamada de Video";
            btnVideoCall.UseVisualStyleBackColor = false;
            btnVideoCall.Click += btnVideoCall_Click;
            // 
            // FormChat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(915, 532);
            Controls.Add(btnVideoCall);
            Controls.Add(lstUsuarios);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(rtbChat);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormChat";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormChat";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.Button btnLogout;
        private Button btnVideoCall;
        private PictureBox pictureBox2;
    }
}