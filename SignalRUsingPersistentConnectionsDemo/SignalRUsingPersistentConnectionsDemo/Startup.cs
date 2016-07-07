using Microsoft.Owin;
using Owin;
using SignalRUsingPersistentConnectionsDemo;
using SignalRUsingPersistentConnectionsDemo.Connections;

[assembly: OwinStartup(typeof (Startup))]

namespace SignalRUsingPersistentConnectionsDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR<ChatConnection>("/Connections/ChatConnection");
        }
    }
}