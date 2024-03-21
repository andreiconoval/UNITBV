namespace DemoProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ISubject subject = new ProxyForSubject();
            subject.Request();
        }
    }
}
