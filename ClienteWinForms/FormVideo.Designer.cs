namespace ClienteWinForms
{
    partial class FormVideo
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
            btnEncerrar = new Button();
            picRemoto = new PictureBox();
            picLocal = new PictureBox();
            btnIniciarCamera = new Button();
            panelBottom = new Panel();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picRemoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLocal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 60, 60);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(btnEncerrar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(625, 51);
            panel1.TabIndex = 7;
            // 
            // btnEncerrar
            // 
            btnEncerrar.BackColor = Color.FromArgb(30, 55, 60);
            btnEncerrar.FlatStyle = FlatStyle.Popup;
            btnEncerrar.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEncerrar.ForeColor = Color.White;
            btnEncerrar.Location = new Point(525, 12);
            btnEncerrar.Margin = new Padding(4, 3, 4, 3);
            btnEncerrar.Name = "btnEncerrar";
            btnEncerrar.Size = new Size(88, 27);
            btnEncerrar.TabIndex = 10;
            btnEncerrar.Text = "Sair";
            btnEncerrar.UseVisualStyleBackColor = false;
            btnEncerrar.Click += btnEncerrar_Click;
            // 
            // picRemoto
            // 
            picRemoto.BackColor = Color.FromArgb(30, 55, 60);
            picRemoto.Location = new Point(12, 68);
            picRemoto.Name = "picRemoto";
            picRemoto.Size = new Size(307, 282);
            picRemoto.SizeMode = PictureBoxSizeMode.StretchImage;
            picRemoto.TabIndex = 8;
            picRemoto.TabStop = false;
            picRemoto.Click += picRemoto_Click;
            // 
            // picLocal
            // 
            picLocal.BackColor = Color.FromArgb(30, 55, 60);
            picLocal.Location = new Point(427, 164);
            picLocal.Name = "picLocal";
            picLocal.Size = new Size(186, 186);
            picLocal.SizeMode = PictureBoxSizeMode.StretchImage;
            picLocal.TabIndex = 9;
            picLocal.TabStop = false;
            picLocal.Click += picLocal_Click;
            // 
            // btnIniciarCamera
            // 
            btnIniciarCamera.BackColor = Color.FromArgb(30, 55, 60);
            btnIniciarCamera.FlatStyle = FlatStyle.Popup;
            btnIniciarCamera.Font = new Font("Constantia", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIniciarCamera.ForeColor = Color.White;
            btnIniciarCamera.Location = new Point(457, 356);
            btnIniciarCamera.Margin = new Padding(4, 3, 4, 3);
            btnIniciarCamera.Name = "btnIniciarCamera";
            btnIniciarCamera.Size = new Size(133, 27);
            btnIniciarCamera.TabIndex = 11;
            btnIniciarCamera.Text = "Ligar Camera";
            btnIniciarCamera.UseVisualStyleBackColor = false;
            btnIniciarCamera.Click += btnIniciarCamera_Click;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(30, 60, 60);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 433);
            panelBottom.Margin = new Padding(4, 3, 4, 3);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(625, 29);
            panelBottom.TabIndex = 12;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.ChatGPT_Image_21_de_mai__de_2026__17_28_39;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(-57, -31);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(245, 117);
            pictureBox2.TabIndex = 13;
            pictureBox2.TabStop = false;
            // 
            // FormVideo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(625, 462);
            Controls.Add(panelBottom);
            Controls.Add(btnIniciarCamera);
            Controls.Add(picLocal);
            Controls.Add(picRemoto);
            Controls.Add(panel1);
            Name = "FormVideo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormVideo";
            FormClosed += FormVideo_FormClosed;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picRemoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLocal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox picRemoto;
        private PictureBox picLocal;
        private Button btnEncerrar;
        private Button btnIniciarCamera;
        private Panel panelBottom;
        private PictureBox pictureBox2;
    }
}