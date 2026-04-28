using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

class Cliente
{
    static TcpClient cliente;
    static NetworkStream stream;
    static StreamReader reader;
    static StreamWriter writer;

    static void Main()
    {
        Console.Write("Digite o IP do servidor: ");
        string ip = Console.ReadLine();

        cliente = new TcpClient(ip, 5000);
        stream = cliente.GetStream();

        reader = new StreamReader(stream, Encoding.UTF8);
        writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

        Console.WriteLine("1 - Login");
        Console.WriteLine("2 - Registrar");
        string opcao = Console.ReadLine();

        Console.Write("Usuário: ");
        string user = Console.ReadLine();

        Console.Write("Senha: ");
        string pass = Console.ReadLine();

        string mensagem = opcao == "1"
            ? $"LOGIN|{user}|{pass}"
            : $"REGISTER|{user}|{pass}";

        writer.WriteLine(mensagem);

        string resposta = reader.ReadLine();

        Console.WriteLine("Servidor: " + resposta);

        if (resposta != "LOGIN_OK")
        {
            Console.WriteLine("Falha no login!");
            return;
        }
       

        Console.WriteLine("Conectado ao servidor!");

        Thread t = new Thread(ReceberMensagens);
        t.Start();

        while (true)
        {
            string msg = Console.ReadLine();
            writer.WriteLine("MSG|" + msg);
        }
    }

    static void ReceberMensagens()
    {
        while (true)
        {
            string mensagem = reader.ReadLine();

            if (mensagem != null)
            {
                Console.WriteLine(mensagem);
            }
        }
    }
}