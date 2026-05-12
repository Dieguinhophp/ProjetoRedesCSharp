using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using System.Net.Sockets;


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
        }



        private void btnEncerrar_Click(object sender, EventArgs e)
        {

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
                            Bitmap bitmap = BitmapConverter.ToBitmap(frame);

                            Invoke(new Action(() =>
                            {
                                if (picLocal.Image != null)
                                    picLocal.Image.Dispose();

                                picLocal.Image = (Bitmap)bitmap.Clone();
                            }));

                            EnviarFrame(bitmap);
                        }

                        await Task.Delay(100);
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
                    bitmap.Save(ms, ImageFormat.Jpeg);

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
