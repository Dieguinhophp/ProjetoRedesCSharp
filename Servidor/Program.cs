using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


class Servidor
{
    static TcpListener servidor;
    static List<TcpClient> clientes = new List<TcpClient>();
    static Dictionary<TcpClient, string> nomes = new Dictionary<TcpClient, string>();

    static void Main()
    {
        servidor = new TcpListener(IPAddress.Any, 5000);
        servidor.Start();

        Console.WriteLine("Servidor iniciado...");

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
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytes = stream.Read(buffer, 0, buffer.Length);
            string mensagem = Encoding.UTF8.GetString(buffer, 0, bytes);

            string[] partes = mensagem.Split('|');

            if (partes[0] == "NOME")
            {
                nomes[cliente] = partes[1];
                Console.WriteLine($"Cliente identificado como: {partes[1]}");
            }
            else if (partes[0] == "MSG")
            {
                string nome = nomes.ContainsKey(cliente) ? nomes[cliente] : "Desconhecido";
                string msgFinal = nome + ": " + partes[1];

                Console.WriteLine(msgFinal);
                EnviarParaTodos(msgFinal);
            }
        }
    }

    static void EnviarParaTodos(string mensagem)
    {
        
        byte[] dados = Encoding.UTF8.GetBytes(mensagem);

        foreach (var cliente in clientes)
        {
            NetworkStream stream = cliente.GetStream();
            stream.Write(dados, 0, + dados.Length);
        }
    }
}