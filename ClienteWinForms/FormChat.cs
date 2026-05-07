using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ClienteWinForms
{
    public partial class FormChat: Form
    {
        TcpClient cliente;
        StreamReader reader;
        StreamWriter writer;
        string usuario;

        public FormChat(TcpClient c, StreamReader r, StreamWriter w, string user)
        {

            InitializeComponent();

            cliente = c;
            reader = r;
            writer = w;
            usuario = user;
            
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

                    if (msg != null)
                    {
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
                                        lstUsuarios.Items.Add(u);
                                }
                            }
                            else
                            {
                                
                                if (msg.StartsWith(usuario + ":"))
                                    rtbChat.SelectionColor = Color.White;
                                else
                                    rtbChat.SelectionColor = Color.ForestGreen;

                                rtbChat.AppendText(msg + Environment.NewLine);
                                rtbChat.ScrollToCaret();
                            }
                        }));
                    }
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

                
                

                this.Close();}
        }
    }
}
