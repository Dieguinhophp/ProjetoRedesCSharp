
using System.Net;
using System.Net.Sockets;
using System.Text;


class Servidor
{
    static TcpListener servidor;
    static List<TcpClient> clientes = new List<TcpClient>();
    static Dictionary<TcpClient, string> nomes = new Dictionary<TcpClient, string>();

    static Dictionary<TcpClient, StreamWriter>
    webcamWriters =
    new Dictionary<TcpClient, StreamWriter>();


    static TcpListener webcamServidor;
    static List<TcpClient> webcamClientes = new List<TcpClient>();
    static void Main()
    {
        Banco.Inicializar();

        servidor = new TcpListener(IPAddress.Any, 5000);
        servidor.Start();

        webcamServidor = new TcpListener(IPAddress.Any, 6000);
        webcamServidor.Start();

        Console.WriteLine("Servidor webcam iniciado na porta 6000");

        Console.WriteLine("Servidor iniciado...");

        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine("IP: " + ip.ToString());
            }
        }
        Thread webcamThread = new Thread(AceitarWebcams);

        webcamThread.IsBackground = true;

        webcamThread.Start();



        while (true)
        {
            TcpClient cliente = servidor.AcceptTcpClient();
            clientes.Add(cliente);

            Console.WriteLine("Cliente conectado!");

            Thread t = new Thread(() => AtenderCliente(cliente));
            t.Start();
        }
    }

    static void AtenderCliente(TcpClient cliente)
    {
        NetworkStream stream = cliente.GetStream();

        StreamReader reader =
            new StreamReader(stream, Encoding.UTF8);

        StreamWriter writer =
            new StreamWriter(stream, new UTF8Encoding(false))
            {
                AutoFlush = true
            };

        try
        {
            while (true)
            {
                string mensagem = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(mensagem))
                    break;


                string[] partes = mensagem.Split('|');

                if (partes.Length == 0)
                    continue;

                switch (partes[0])
                {
                    case "LOGIN":

                        if (partes.Length < 3)
                        {
                            writer.WriteLine("LOGIN_ERRO");
                            continue;
                        }

                        bool login =
                            Banco.Login(partes[1], partes[2]);

                        if (login)
                        {
                            nomes[cliente] = partes[1];

                            writer.WriteLine("LOGIN_OK");

                            Thread.Sleep(100);

                            EnviarListaUsuarios();

                            Console.WriteLine(partes[1] + " entrou.");
                        }
                        else
                        {
                            writer.WriteLine("LOGIN_ERRO");
                        }

                        break;

                    case "REGISTER":

                        if (partes.Length < 4)
                        {
                            writer.WriteLine("REGISTER_ERRO");
                            continue;
                        }

                        bool registro =
                            Banco.Registrar(partes[1], partes[2], partes[3]);

                        writer.WriteLine(
                            registro
                            ? "REGISTER_OK"
                            : "REGISTER_ERRO"
                        );

                        break;

                    case "MSG":

                        if (partes.Length < 2)
                            continue;

                        string nome =
                            nomes.ContainsKey(cliente)
                            ? nomes[cliente]
                            : "Desconhecido";

                        string cargo =
                            Banco.ObterCargo(nome);

                        string msgFinal =
                            "[" + cargo + "] " + nome + ": " + partes[1];

                        Console.WriteLine(msgFinal);

                        EnviarParaTodos(msgFinal);

                        break;

                    


                    case "LOGOUT":

                        Console.WriteLine("Cliente saiu.");

                        clientes.Remove(cliente);

                        if (nomes.ContainsKey(cliente))
                            nomes.Remove(cliente);

                        EnviarListaUsuarios();

                        cliente.Close();

                        return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro cliente: " + ex.Message);
        }
        finally
        {
            clientes.Remove(cliente);

            if (nomes.ContainsKey(cliente))
                nomes.Remove(cliente);

            EnviarListaUsuarios();

            try
            {
                cliente.Close();
            }
            catch { }

            Console.WriteLine("Cliente desconectado.");
        }
    }

    static void EnviarParaTodos(string mensagem)
    {
        foreach (var cliente in clientes)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                writer.WriteLine(mensagem);
            }
            catch
            {
                // ignora erro
            }
        }
    }

    static void ReceberWebcam(TcpClient cliente)
    {
        try
        {
            NetworkStream stream =
                cliente.GetStream();

            StreamReader reader =
                new StreamReader(stream, Encoding.UTF8);

            while (true)
            {
                string frame = reader.ReadLine();

                if (frame == null)
                    break;

                foreach (var c in webcamClientes)
                {
                    try
                    {
                        if (c == cliente)
                            continue;

                        webcamWriters[c]
                            .WriteLine(frame);
                    }
                    catch
                    {

                    }
                }
            }
        }
        catch
        {

        }
        finally
        {
            webcamClientes.Remove(cliente);
            if (webcamWriters.ContainsKey(cliente))
            {
                webcamWriters.Remove(cliente);
            }


            try
            {
                cliente.Close();
            }
            catch
            {

            }

            Console.WriteLine("Cliente webcam desconectado.");
        }
    }


    static void AceitarWebcams()
    {
        while (true)
        {
            TcpClient cliente =
                webcamServidor.AcceptTcpClient();

            webcamClientes.Add(cliente);
            webcamWriters[cliente] =
            new StreamWriter(
        cliente.GetStream(),
        Encoding.UTF8)
    {
        AutoFlush = true
    };

            Console.WriteLine("Cliente webcam conectado!");

            Thread t = new Thread(() => ReceberWebcam(cliente));

            t.IsBackground = true;

            t.Start();
        }
    }


    


    static void EnviarListaUsuarios()
    {
        string lista = "USERS|";

        foreach (var nome in nomes.Values)
        {
            string cargo = Banco.ObterCargo(nome);

            lista += nome + ":" + cargo + ",";
        }

        lista = lista.TrimEnd(',');

        List<TcpClient> desconectados = new List<TcpClient>();

        foreach (var cliente in clientes)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();

                StreamWriter writer =
                    new StreamWriter(stream, Encoding.UTF8)
                    {
                        AutoFlush = true
                    };

                writer.WriteLine(lista);
            }
            catch
            {
                desconectados.Add(cliente);
            }
        }

        foreach (var c in desconectados)
        {
            clientes.Remove(c);

            if (nomes.ContainsKey(c))
                nomes.Remove(c);

            try
            {
                c.Close();
            }
            catch { }
        }
    }
}