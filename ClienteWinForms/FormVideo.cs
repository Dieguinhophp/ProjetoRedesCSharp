using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ClienteWinForms
{
    public partial class FormVideo : Form
    {
        VideoCapture camera;
        bool cameraLigada = false;

        TcpClient webcamCliente;
        StreamReader webcamReader;
        StreamWriter webcamWriter;
        string ipServidor;
        string usuario;
        


        public FormVideo(string ip)
        {
            InitializeComponent();
            ipServidor = ip;
            
            try
            {
                webcamCliente = new TcpClient(ipServidor, 6000);
                MessageBox.Show("Conectado servidor webcam!");
                NetworkStream stream = webcamCliente.GetStream();

                webcamReader = new StreamReader(stream, Encoding.UTF8);

                webcamWriter = new StreamWriter(stream, Encoding.UTF8)
                    {
                        AutoFlush = true
                    };

                Thread t = new Thread(ReceberMensagens);

                t.IsBackground = true;

                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void ReceberMensagens()
        {
            try
            {
                while (true)
                {
                    string msg = webcamReader.ReadLine();

                    if (msg == null)
                        break;

                    if (!msg.StartsWith("FRAME|"))
                        continue;

                    string base64 =
                        msg.Substring(6);

                    byte[] imagemBytes =
                        Convert.FromBase64String(base64);

                    using (MemoryStream ms =
                        new MemoryStream(imagemBytes))
                    {
                        using (Bitmap temp =
                                new Bitmap(ms))
                        {
                            Bitmap bitmap =
                                new Bitmap(temp);

                            BeginInvoke(new Action(() =>
                            {
                                Image imagemAnterior =
                                    picRemoto.Image;

                                picRemoto.Image = bitmap;

                                if (imagemAnterior != null)
                                    imagemAnterior.Dispose();
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja desligar chamada de video?", "Encerrar Ligação",
            MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private async void btnIniciarCamera_Click(object sender, EventArgs e)
        {
            try
            {
                camera = new VideoCapture(0, VideoCaptureAPIs.DSHOW);

                if (!camera.IsOpened())
                {
                    MessageBox.Show("Não foi possível abrir a câmera.");
                    return;
                }

                cameraLigada = true;

                await Task.Run(async () =>
                {
                    Mat frame = new Mat();

                    while (cameraLigada)
                    {
                        camera.Read(frame);

                        if (!frame.Empty())
                        {
                            Bitmap original = BitmapConverter.ToBitmap(frame);

                            Bitmap bitmap =
                                new Bitmap(original, new System.Drawing.Size(360, 320));

                            Invoke(new Action(() =>
                            {
                                Image imagemAnterior = picLocal.Image;

                                picLocal.Image = (Bitmap)bitmap.Clone();

                                if (imagemAnterior != null)
                                    imagemAnterior.Dispose();

                                
                            }));

                            EnviarFrame(bitmap);
                            original.Dispose();
                            bitmap.Dispose();
                        }

                        await Task.Delay(200);
                    }

                    frame.Dispose();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void EnviarFrame(Bitmap bitmap)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ImageCodecInfo jpgEncoder =
                    ImageCodecInfo.GetImageDecoders()
                    .First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                    EncoderParameters encoderParams =
                        new EncoderParameters(1);

                    encoderParams.Param[0] =
                        new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 15L);

                    bitmap.Save(ms, jpgEncoder, encoderParams);

                    byte[] imagemBytes = ms.ToArray();

                    string base64 = Convert.ToBase64String(imagemBytes);

                    webcamWriter.WriteLine("FRAME|" + base64);  
                }
            }
            catch
            {

            }
        }


        private void picRemoto_Click(object sender, EventArgs e)
        {

        }

        private void picLocal_Click(object sender, EventArgs e)
        {

        }

        private void FormVideo_FormClosed(object sender, FormClosedEventArgs e)
        {
            cameraLigada = false;

            if (camera != null)
            {
                
                camera.Release();
                camera.Dispose();
            }
            if (webcamCliente != null)
            {
                webcamCliente.Close();
            }
        }
    }
}
