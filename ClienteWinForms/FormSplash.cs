using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace ClienteWinForms
{
    public partial class FormSplash : Form

    {
        bool fadeIn = true;
        bool fadeOut = false;

        public FormSplash()
        {
            InitializeComponent();

            this.Opacity = 0;

            timer1.Interval = 50;
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            
            if (fadeIn)
            {
                this.Opacity += 0.05;

                if (this.Opacity >= 1)
                {
                    fadeIn = false;

                    Task.Delay(1500).ContinueWith(t =>
                    {
                        fadeOut = true;
                    });
                }
            }

            
            else if (fadeOut)
            {
                this.Invoke(new Action(() =>
                {
                    this.Opacity -= 0.05;

                    if (this.Opacity <= 0)
                    {
                        timer1.Stop();

                        FormIp ip = new FormIp();

                        ip.Show();

                        this.Hide();
                    }
                }));
            }
        }
    }
}
