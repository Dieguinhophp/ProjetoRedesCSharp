using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClienteWinForms
{


    public partial class FormIp : Form
    {
        
        public FormIp()
        {
            InitializeComponent();
            
        }

        private void txtIp_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient cliente = new TcpClient(txtIp.Text, 5000);

                NetworkStream stream = cliente.GetStream();

                StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8)
                {
                    AutoFlush = true
                };
                

                FormLogin login = new FormLogin(cliente, reader, writer, txtIp.Text);

                login.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
