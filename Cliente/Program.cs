using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Cliente
{
    static TcpClient cliente;
    static NetworkStream stream;

    static void Main()
    {
        cliente = new TcpClient("127.0.0.1", 5000);
        stream = cliente.GetStream();

        Console.WriteLine("Conectado ao servidor!");

        Thread t = new Thread(ReceberMensagens);
        t.Start();

        while (true)
        {
            string msg = Console.ReadLine();
            byte[] dados = Encoding.UTF8.GetBytes(msg);
            stream.Write(dados, 0, dados.Length);
        }
    }

    static void ReceberMensagens()
    {
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytes = stream.Read(buffer, 0, buffer.Length);
            string mensagem = Encoding.UTF8.GetString(buffer, 0, bytes);

            Console.WriteLine("Mensagem: " + mensagem);
        }
    }
}