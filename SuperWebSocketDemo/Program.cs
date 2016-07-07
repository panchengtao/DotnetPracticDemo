using System;
using SuperSocket.SocketBase;
using SuperWebSocket;
using static System.Console;

namespace SuperWebSocketDemo
{
    class Program
    {
        private static void Main(string[] args)
        {
            var server = new WebSocketServer();

            server.NewSessionConnected += ServerNewSessionConnected;
            server.NewMessageReceived += ServerNewMessageRecevied;
            server.SessionClosed += ServerSessionClosed;

            try
            {
                server.Setup("127.0.0.1", 4141); 
                server.Start(); 
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            ReadKey();
        }

        private static void ServerSessionClosed(WebSocketSession session, CloseReason value)
        {
            WriteLine(session.Origin);
        }

        public static void ServerNewMessageRecevied(WebSocketSession session, string value)
        {
            WriteLine(value);
            session.Send("已收到："+value);
        }

        /// <param name="session"></param>
        public static void ServerNewSessionConnected(WebSocketSession session)
        {
            WriteLine(session.Origin);
        }
    }
}