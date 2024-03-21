using System;
using System.Runtime.Remoting;

namespace DoFactory.GangOfFour.Proxy.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Proxy Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create math proxy
            MathProxy proxy = new MathProxy();

            // Do the math
            Console.WriteLine("4 + 2 = " + proxy.Add(4, 2));
            Console.WriteLine("4 - 2 = " + proxy.Sub(4, 2));
            Console.WriteLine("4 * 2 = " + proxy.Mul(4, 2));
            Console.WriteLine("4 / 2 = " + proxy.Div(4, 2));

            // Wait for user
            Console.Read();
        }
    }

    // "Subject" 

    public interface IMath
    {
        double Add(double x, double y);
        double Sub(double x, double y);
        double Mul(double x, double y);
        double Div(double x, double y);
    }

    // "RealSubject" 

    class Math : MarshalByRefObject, IMath
    {
        public double Add(double x, double y){return x + y;}
        public double Sub(double x, double y){return x - y;}
        public double Mul(double x, double y){return x * y;}
        public double Div(double x, double y){return x / y;}
    }

    // Remote "Proxy Object" 

    class MathProxy : IMath
    {
        private Math math;

        // Constructor
        public MathProxy()
        {
            // Create Math instance in a different AppDomain
            AppDomain ad = 
                AppDomain.CreateDomain("MathDomain",null, null);

            ObjectHandle o = ad.CreateInstance(
                "DoFactory.GangOfFour.Proxy.NETOptimized", 
                "DoFactory.GangOfFour.Proxy.NETOptimized.Math");
            math = (Math)o.Unwrap();
        }

        public double Add(double x, double y)
        { 
            return math.Add(x,y); 
        }

        public double Sub(double x, double y)
        { 
            return math.Sub(x,y); 
        }

        public double Mul(double x, double y)
        { 
            return math.Mul(x,y); 
        }

        public double Div(double x, double y)
        { 
            return math.Div(x,y); 
        }
    }
}
