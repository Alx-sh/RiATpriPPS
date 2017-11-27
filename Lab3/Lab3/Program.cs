using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = "127.0.0.1";
            var port = int.Parse(Console.ReadLine());
            var httpServer = new Methods();
            httpServer.Start(host, port);
        }
    }
}
