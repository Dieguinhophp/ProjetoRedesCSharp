using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClienteWinForms
{
    public partial class FormIp : Form
    {
        TcpClient cliente;
        StreamReader reader;
        StreamWriter writer;
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
                cliente = new TcpClient(txtIp.Text, 5000);

                var stream = cliente.GetStream();

                reader = new StreamReader(stream, new UTF8Encoding(false));
                writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

                MessageBox.Show("Conectado ao servidor!");

                
                FormLogin login = new FormLogin(cliente, reader, writer);
                login.Show();

                this.Hide();
            }
            catch
            {
                MessageBox.Show("Erro ao conectar ao servidor!");
            }
        }
    }
}
