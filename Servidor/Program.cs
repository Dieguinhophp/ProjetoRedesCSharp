
using System.Net;
using System.Net.Sockets;
using System.Text;


class Servidor
{
    static TcpListener servidor;
    static List<TcpClient> clientes = new List<TcpClient>();
    static Dictionary<TcpClient, string> nomes = new Dictionary<TcpClient, string>();

    static void Main()
    {
        Banco.Inicializar();

        servidor = new TcpListener(IPAddress.Any, 5000);
        servidor.Start();

        Console.WriteLine("Servidor iniciado...");

        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine("IP: " + ip.ToString());
            }
        }

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

                        if (partes.Length < 3)
                        {
                            writer.WriteLine("REGISTER_ERRO");
                            continue;
                        }

                        bool registro =
                            Banco.Registrar(partes[1], partes[2]);

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

                        string msgFinal =
                            nome + ": " + partes[1];

                        Console.WriteLine(msgFinal);

                        EnviarParaTodos(msgFinal);

                        break;

                    case "WEBCAM":

                        if (partes.Length < 2)
                            continue;

                        string frameBase64 = partes[1];

                        EnviarWebcamParaTodos(frameBase64, cliente);

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

    static void EnviarWebcamParaTodos(string frame, TcpClient remetente)
    {
        foreach (var cliente in clientes)
        {
            try
            {
                if (cliente == remetente)
                    continue;

                NetworkStream stream = cliente.GetStream();

                StreamWriter writer =
                    new StreamWriter(stream, Encoding.UTF8)
                    {
                        AutoFlush = true
                    };

                writer.WriteLine("WEBCAM|" + frame);
            }
            catch
            {

            }
        }
    }



    static void EnviarListaUsuarios()
    {
        string lista = "USERS|";

        foreach (var nome in nomes.Values)
        {
            lista += nome + ",";
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