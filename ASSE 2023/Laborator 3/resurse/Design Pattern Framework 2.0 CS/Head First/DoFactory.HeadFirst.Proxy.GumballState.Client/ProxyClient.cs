using System;
using System.Runtime.Remoting;

using DoFactory.HeadFirst.Proxy.GumballState.Machine;

namespace DoFactory.HeadFirst.Proxy.GumballState.Client
{
    class ProxyClient
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("ProxyClient.exe.config",false);

            GumballMachine machine = new GumballMachine();
            GumballMonitor monitor = new GumballMonitor(machine);
            monitor.Report();

            // Wait for user
            Console.Read();
        }
    }

    #region GumballMonitor

    public class GumballMonitor 
    {
        GumballMachine machine;
 
        public GumballMonitor(GumballMachine machine) 
        {
            this.machine = machine;
        }
 
        public void Report() 
        {
            Console.WriteLine("Gumball Machine: " + machine.Location);
            Console.WriteLine("Current inventory: " + machine.Count + " gumballs");
            Console.WriteLine("Current state: " + machine.State);
        }
    }
    #endregion
}
