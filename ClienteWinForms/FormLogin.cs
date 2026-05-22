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
        string ipServidor;
        public FormLogin(TcpClient c, StreamReader r, StreamWriter w, string ip)
        {
            InitializeComponent();
            cliente = c;
            reader = r;
            writer = w;
            ipServidor = ip;

            cmbCargo.Items.Add("Brigadista");
            cmbCargo.Items.Add("Bombeiro");
            cmbCargo.Items.Add("Capitão");
            cmbCargo.Items.Add("Supervisor");
            cmbCargo.Items.Add("Coordenador");

            cmbCargo.SelectedIndex = 0;
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
                writer.WriteLine($"LOGIN|{txtUsuario.Text}|{txtSenha.Text}");

                string resposta = reader.ReadLine();

                if (resposta == "LOGIN_OK")
                {

                    FormChat chat = new FormChat(cliente, reader, writer, txtUsuario.Text, ipServidor);
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
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
            string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha usuário e senha!");
                return;
            }

            try
            {
                writer.WriteLine($"REGISTER|{txtUsuario.Text}|{txtSenha.Text}|{cmbCargo.Text}");

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
