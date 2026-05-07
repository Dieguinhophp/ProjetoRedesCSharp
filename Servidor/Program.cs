
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

        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

        try
        {
            while (true)
            {
                string mensagem = reader.ReadLine();

                if (mensagem == null)
                    break;

                string[] partes = mensagem.Split('|');

                if (partes[0] == "LOGIN")
                {
                    bool sucesso = Banco.Login(partes[1], partes[2]);

                    if (sucesso)
                        nomes[cliente] = partes[1];

                    writer.WriteLine(sucesso ? "LOGIN_OK" : "LOGIN_ERRO");
                    EnviarListaUsuarios();
                }
                else if (partes[0] == "REGISTER")
                {
                    bool sucesso = Banco.Registrar(partes[1], partes[2]);
                    writer.WriteLine(sucesso ? "REGISTER_OK" : "REGISTER_ERRO");
                }
                else if (partes[0] == "MSG")
                {
                    string nome = nomes.ContainsKey(cliente) ? nomes[cliente] : "Desconhecido";
                    string msgFinal = nome + ": " + partes[1];

                    Console.WriteLine(msgFinal);
                    EnviarParaTodos(msgFinal);

                }
                else if (partes[0] == "LOGOUT")
                {
                    Console.WriteLine("Cliente saiu.");

                    clientes.Remove(cliente);
                    nomes.Remove(cliente);

                    EnviarListaUsuarios();

                    cliente.Close();
                    break;
                }
            }
        }
        catch
        {
            Console.WriteLine("Cliente desconectado.");
        }
        finally
        {
            clientes.Remove(cliente);
            nomes.Remove(cliente);
            EnviarListaUsuarios();
            cliente.Close();
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

    static void EnviarListaUsuarios()
    {
        string lista = "USERS|";

        foreach (var nome in nomes.Values)
        {
            lista += nome + ",";
        }
        lista = lista.TrimEnd(',');

        foreach (var cliente in clientes)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                writer.WriteLine(lista);
            }
            catch
            {
                clientes.Remove(cliente);
                nomes.Remove(cliente);

                EnviarListaUsuarios();
            }

        }
    }
}