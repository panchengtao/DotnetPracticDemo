using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace SocketDemo
{
    internal class Program
    {
        public static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            hostEntry = Dns.GetHostEntry(server);
            foreach (var address in hostEntry.AddressList)
            {
                var ipe = new IPEndPoint(address, port);
                var tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    tempSocket.Connect(ipe);
                }
                catch (Exception)
                {
                    tempSocket.Connect(ipe);
                }


                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
            }
            return s;
        }

        public static string SocketSendReceive(string server, int port)
        {
            var request = "GET / HTTP/1.1\r\nHost: " + server +
                          "\r\nConnection: Close\r\n\r\n";
            var bytesSent = Encoding.ASCII.GetBytes(request);
            var bytesReceived = new byte[256];
            var s = ConnectSocket(server, port);

            if (s == null)
            {
                return "Connection Failed";
            }
            s.Send(bytesSent, bytesSent.Length, 0);

            int bytes;
            var page = "Default HTML page on " + server + ":\r\n";

            do
            {
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            } while (bytes > 0);

            return page;
        }


        private static void Main(string[] args)
        {
            var port = 80;
            var host = Dns.GetHostName();

            var result = SocketSendReceive(host, port);
            WriteLine(result);

            ReadKey();
        }
    }
}