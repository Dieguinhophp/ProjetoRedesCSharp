using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Linq;
using System.Drawing.Imaging;

namespace ClienteWinForms
{
    public partial class FormVideo : Form
    {
        VideoCapture camera;
        bool cameraLigada = false;

        TcpClient cliente;
        StreamReader reader;
        StreamWriter writer;
        string usuario;



        public FormVideo(TcpClient c, StreamReader r, StreamWriter w, string user)
        {

            InitializeComponent();


            cliente = c;
            reader = r;
            writer = w;
            usuario = user;

            Thread t = new Thread(ReceberMensagens);

            t.IsBackground = true;

            t.Start();

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

                    if (msg.StartsWith("WEBCAM|"))
                    {
                        string base64 =
                            msg.Substring(8);

                        byte[] imagemBytes =
                            Convert.FromBase64String(base64);

                        using (MemoryStream ms =
                            new MemoryStream(imagemBytes))
                        {
                            Bitmap bitmap =
                                new Bitmap(ms);

                            Invoke(new Action(() =>
                            {
                                if (picRemoto.Image != null)
                                    picRemoto.Image.Dispose();

                                picRemoto.Image =
                                    (Bitmap)bitmap.Clone();
                            }));
                        }
                    }
                }
            }
            catch
            {

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
                                new Bitmap(original, new System.Drawing.Size(320, 240));

                            Invoke(new Action(() =>
                            {
                                if (picLocal.Image != null)
                                    picLocal.Image.Dispose();

                                picLocal.Image = (Bitmap)bitmap.Clone();
                            }));

                            EnviarFrame(bitmap);
                        }

                        await Task.Delay(300);
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
                        new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 30L);

                    bitmap.Save(ms, jpgEncoder, encoderParams);

                    byte[] imagemBytes = ms.ToArray();

                    string base64 =
                        Convert.ToBase64String(imagemBytes);

                    writer.WriteLine("WEBCAM|" + base64);
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
        
        }
    }
}
