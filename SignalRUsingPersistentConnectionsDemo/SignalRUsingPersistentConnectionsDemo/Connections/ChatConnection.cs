using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRUsingPersistentConnectionsDemo.Connections
{
    public class ChatConnection : PersistentConnection
    {
        /// <summary>
        ///     代表连接时传回Welcome
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "Welcome!");
        }

        /// <summary>
        ///     广播消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(data);
        }
        
    }
}