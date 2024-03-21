using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Observer.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Observer Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create IBM stock and attach investors
            IBM ibm = new IBM(120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            // Fluctuating prices will notify investors
            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;

            // Wait for user
            Console.Read();
        }
    }

    // Generic delegate type for hooking up stock change requests
    public delegate void ChangeEventHandler<T,U>
        (T sender, U eventArgs);

    // Custom event arguments
    public class ChangeEventArgs : EventArgs 
    {
        private string symbol;
        private double price;

        // Constructor
        public ChangeEventArgs(string symbol, double price) 
        {
            this.symbol = symbol;
            this.price = price;
        }

        // Properties
        public double Price
        {
            get{ return price; }
            set{ price = value; }
        }

        public string Symbol
        {
            get{ return symbol; }
            set{ symbol = value; }
        }
    }  

    // "Subject" 

    abstract class Stock
    {
        protected string symbol;
        protected double price;

        // Constructor
        public Stock(string symbol, double price)
        {
            this.symbol = symbol;
            this.price = price;
        }

        // Event
        public event ChangeEventHandler<Stock,ChangeEventArgs> Change;
        
        // Invoke the Change event
        public virtual void OnChange(ChangeEventArgs e) 
        {
            if (Change != null)
            {
                Change(this, e);
            }
        }

        public void Attach(Investor investor)
        {
            Change += new ChangeEventHandler<Stock,ChangeEventArgs>(investor.Update);
        }

        public void Detach(Investor investor)
        {
            Change -= new ChangeEventHandler<Stock, ChangeEventArgs>(investor.Update);
        }

        // Properties
        public double Price
        {
            get{ return price; }
            set
            {
                price = value;
                OnChange(new ChangeEventArgs(symbol,price));
                Console.WriteLine("");
            }
        }

        public string Symbol
        {
            get{ return symbol; }
            set{ symbol = value; }
        }
    }

    // "ConcreteSubject"

    class IBM : Stock
    {
        // Constructor - symbol for IBM is always same
        public IBM(double price) : base("IBM", price)
        {
        }
    }

    // "Observer"

    interface IInvestor
    {
        void Update(Stock sender, ChangeEventArgs e);
    }

    // "ConcreteObserver"

    class Investor : IInvestor
    {
        private string name;
        private Stock stock;

        // Constructor
        public Investor(string name)
        {
            this.name = name;
        }

        public void Update(Stock sender, ChangeEventArgs e)
        {
            Console.WriteLine("Notified {0} of {1}'s " +
                "change to {2:C}", name, e.Symbol, e.Price);
        }

        // Property
        public Stock Stock
        {
            get{ return stock; }
            set{ stock = value; }
        }
    }
}
