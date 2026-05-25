using System.Net.Sockets;

namespace ClienteWinForms
{
    public partial class FormChat : Form
    {


        TcpClient cliente;
        StreamReader reader;
        StreamWriter writer;
        string usuario;
        string ipServidor;


        public FormChat(TcpClient c, StreamReader r, StreamWriter w, string user, string ip)
        {

            InitializeComponent();


            cliente = c;
            reader = r;
            writer = w;
            usuario = user;
            ipServidor = ip;

            lblUsuario.Text = "Usuário: " + usuario;


            Thread t = new Thread(ReceberMensagens);
            t.IsBackground = true;
            t.Start();



        }

        private void rtbChat_TextChanged(object sender, EventArgs e)
        {


        }

        private void txtMensagem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMensagem.Text))
            {
                writer.WriteLine("MSG|" + txtMensagem.Text);
                txtMensagem.Clear();
            }
        }

        void ReceberMensagens()
        {
            try
            {
                while (true)
                {
                    string msg = reader.ReadLine();

                    if (msg == null)
                        break;

                    Invoke(new Action(() =>
                    {
                        if (msg.StartsWith("USERS|"))
                        {
                            string lista = msg.Replace("USERS|", "");
                            string[] usuarios = lista.Split(',');

                            lstUsuarios.Items.Clear();

                            foreach (var u in usuarios)
                            {
                                if (!string.IsNullOrWhiteSpace(u))
                                {
                                    string[] dados = u.Split(':');

                                    if (dados.Length >= 2)
                                    {
                                        string nome = dados[0];
                                        string cargo = dados[1];

                                        lstUsuarios.Items.Add(
                                            "🟢 " + nome + " [" + cargo + "]"
                                        );
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (msg.Contains("[Capitão]"))
                            {
                                rtbChat.SelectionColor = Color.Orange;
                            }
                            else if (msg.Contains("[Supervisor]"))
                            {
                                rtbChat.SelectionColor = Color.Red;
                            }
                            else if (msg.Contains("[Coordenador]"))
                            {
                                rtbChat.SelectionColor = Color.DeepSkyBlue;
                            }
                            else
                            {
                                rtbChat.SelectionColor = Color.White;
                            }

                            rtbChat.AppendText(msg + Environment.NewLine);
                            rtbChat.ScrollToCaret();
                        }
                    }));
                }
            }
            catch
            {
                MessageBox.Show("Conexão com o servidor foi encerrada.");
                Application.Exit();
            }
        }



        private void txtMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnviar.PerformClick();
                e.SuppressKeyPress = true;
            }

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair?", "Logout",
            MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    writer.WriteLine("LOGOUT");
                }
                catch { }

                try
                {
                    cliente.Close();
                }
                catch { }

                this.Close();
            }
        }



        private void btnVideoCall_Click(object sender, EventArgs e)
        {
            FormVideo video = new FormVideo(ipServidor);

            video.Show();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}


