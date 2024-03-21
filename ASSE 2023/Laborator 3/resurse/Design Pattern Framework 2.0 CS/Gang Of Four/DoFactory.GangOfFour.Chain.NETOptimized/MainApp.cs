using System;

namespace DoFactory.GangOfFour.Chain.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Chain of Responsibility Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            Purchase purchase;

            // Setup Chain of Responsibility
            Approver Larry = new Director();
            Approver Sam   = new VicePresident();
            Approver Tammy = new President();

            Larry.Successor   = Sam;
            Sam.Successor     = Tammy;

            // Generate and process purchase requests
            purchase = new Purchase(2034, 350.00, "Supplies");
            Larry.ProcessRequest(purchase);

            purchase = new Purchase(2035, 32590.10, "Project X");
            Larry.ProcessRequest(purchase);

            purchase = new Purchase(2036, 122100.00, "Project Y");
            Larry.ProcessRequest(purchase);

            // Wait for user
            Console.Read();
        }
    }

    public class PurchaseEventArgs : EventArgs 
    {
        private int number;
        private double amount;
        private string purpose;

        // Constructor
        public PurchaseEventArgs(int number, double amount, 
            string purpose) 
        {
            this.number = number;
            this.amount = amount;
            this.purpose = purpose;
        }

        // Properties
        public double Amount
        {
            get{ return amount; }
            set{ amount = value; }
        }

        public string Purpose
        {
            get{ return purpose; }
            set{ purpose = value; }
        }

        public int Number
        {
            get{ return number; }
            set{ number = value; }
        }
    }  

    // Generic delegate for hooking up purchase requests
    public delegate void PurchaseEventHandler<T,U>(T sender, U eventArgs);

    // "Handler"

    abstract class Approver
    {
        protected Approver successor;

        // Event
        public event PurchaseEventHandler<Approver,PurchaseEventArgs> Purchase;

        // Invoke the Purchase event
        public virtual void OnPurchase(PurchaseEventArgs e) 
        {
            if (Purchase != null)
            {
                Purchase(this, e);
            }
        }

        public void ProcessRequest(Purchase purchase)
        {
            OnPurchase(new PurchaseEventArgs(
                purchase.Number, purchase.Amount, purchase.Purpose));
        }

        // Property
        public Approver Successor
        {
            set
            {
                this.successor = value;
            }
        }
    }

    // "ConcreteHandler"

    class Director : Approver
    {
        // Constructor
        public Director()
        {
            // Hook up delegate to event
            this.Purchase += 
                new PurchaseEventHandler<Approver,PurchaseEventArgs>(DirectorRequest);
        }

        public void DirectorRequest(Approver approver, PurchaseEventArgs e)
        {
            if (e.Amount < 10000.0)
            {
                Console.WriteLine("{0} approved request# {1}", 
                    this.GetType().Name, e.Number);
            }
            else if (successor != null)
            {
                successor.OnPurchase(e);
            }
        }
    }

    // "ConcreteHandler"

    class VicePresident : Approver
    {
        // Constructor
        public VicePresident()
        {
            // Hook up delegate to event
            this.Purchase +=
                new PurchaseEventHandler<Approver, PurchaseEventArgs>(VicePresidentRequest);
        }

        public void VicePresidentRequest(Approver approver, 
            PurchaseEventArgs e)
        {
            if (e.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}", 
                    this.GetType().Name, e.Number);
            }
            else if (successor != null)
            {
                successor.OnPurchase(e);
            }
        }
    }

    // "ConcreteHandler"

    class President : Approver
    {
        // Constructor
        public President()
        {
            // Hook up delegate to event
            this.Purchase +=
                new PurchaseEventHandler<Approver, PurchaseEventArgs>(PresidentRequest);
        }

        public void PresidentRequest(Approver approver, PurchaseEventArgs e)
        {
            if (e.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}", 
                    this.GetType().Name, e.Number);
            }
            else
            {
                Console.WriteLine(
                    "Request# {0} requires an executive meeting!", 
                    e.Number);
            }
        }
    }

    // Request details

    class Purchase
    {
        private int number;
        private double amount;
        private string purpose;

        // Constructor
        public Purchase(int number, double amount, string purpose)
        {
            this.number = number;
            this.amount = amount;
            this.purpose = purpose;
        }

        // Properties
        public double Amount
        {
            get{ return amount; }
            set{ amount = value; }
        }

        public string Purpose
        {
            get{ return purpose; }
            set{ purpose = value; }
        }

        public int Number
        {
            get{ return number; }
            set{ number = value; }
        }
    }
}
