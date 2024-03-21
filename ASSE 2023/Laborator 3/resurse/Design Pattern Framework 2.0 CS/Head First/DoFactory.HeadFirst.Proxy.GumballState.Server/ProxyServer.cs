using System;
using System.Runtime.Remoting;

namespace DoFactory.HeadFirst.Proxy.GumballState.Server
{
    class ProxyServer
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("ProxyServer.exe.config",false);
            Console.WriteLine("Wait for request. Press any key to exit...");

            // Wait for user
            Console.Read();
        }
    }
}
