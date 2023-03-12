using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    class OurServer
    {
        TcpListener _listener;

        public OurServer()
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            _listener.Start();

            LoopClients();
        }

        void LoopClients()
        {
            while (true)
            {
                TcpClient client = _listener.AcceptTcpClient();

                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }

        void HandleClient(TcpClient client)
        {
            StreamReader _sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter _sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            
            while (true)
            {
                string message = _sReader.ReadLine();
                Console.WriteLine($"Client: {message}");

                Console.Write("> ");
                string answer = Console.ReadLine();
                _sWriter.WriteLine(answer);
                _sWriter.Flush();
            }
        }
    }
}