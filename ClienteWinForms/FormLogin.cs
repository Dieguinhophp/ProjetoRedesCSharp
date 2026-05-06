using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;



namespace ClienteWinForms
{
    public partial class FormLogin : Form

    {
        TcpClient cliente;
        StreamReader reader;
        StreamWriter writer;
        public FormLogin(TcpClient c, StreamReader r, StreamWriter w)
        {
            InitializeComponent();
            cliente = c;
            reader = r;
            writer = w;
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
            try
            {
                writer.WriteLine($"REGISTER|{txtUsuario.Text}|{txtSenha.Text}");

                string resposta = reader.ReadLine();

                if (resposta == "REGISTER_OK")
                {
                    MessageBox.Show("Usuário registrado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro ao registrar! Usuário pode já existir.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Erro ao comunicar com o servidor!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
