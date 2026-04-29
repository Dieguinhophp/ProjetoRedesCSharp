using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;



namespace ClienteWinForms
{
    public partial class FormLogin : Form

    {
        TcpClient cliente;
        StreamReader reader;
        StreamWriter writer;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cliente = new TcpClient("192.168.15.183", 5000);

                var stream = cliente.GetStream();

                reader = new StreamReader(stream, new UTF8Encoding(false));
                writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

                writer.WriteLine($"LOGIN|{txtUsuario.Text}|{txtSenha.Text}");

                string resposta = reader.ReadLine();

                if (resposta == "LOGIN_OK")
                {
  
                    FormChat chat = new FormChat(cliente, reader, writer, txtUsuario.Text);
                    chat.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Erro no login!");
                }
            }
            catch
            {
                MessageBox.Show("Erro ao conectar!");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            writer.WriteLine($"REGISTER|{txtUsuario.Text}|{txtSenha.Text}");

            string resposta = reader.ReadLine();
            MessageBox.Show(resposta);
        }
    }
}
