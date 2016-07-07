using Microsoft.AspNet.SignalR.Client;
using static System.Console;

namespace DotnetClientWithSignalRDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connection = new Connection("http://localhost:1508/Connections/ChatConnection");

            connection.Received += WriteLine;
            connection.Start().Wait();

            string line;
            while ((line = ReadLine()) != null)
            {
                connection.Send(line).Wait();
            }
        }
    }
}