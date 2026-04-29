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
                            rtbChat.AppendText(msg + Environment.NewLine);
                            rtbChat.ScrollToCaret();
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

        
    }
}
