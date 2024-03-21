using System;
using System.Text;

namespace DoFactory.HeadFirst.State.GumballState
{
    class GumballMachineTestDrive
    {
        static void Main(string[] args)
        {
            GumballMachine gumballMachine = new GumballMachine(5);

            Console.WriteLine(gumballMachine);

            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            Console.WriteLine(gumballMachine);

            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();

            Console.WriteLine(gumballMachine);

            // Wait for user
            Console.Read();
        }
    }

    #region Gumball Machine

    public class GumballMachine 
    {
        IState soldOutState;
        IState noQuarterState;
        IState hasQuarterState;
        IState soldState;
 
        IState state;
        int count = 0;
 
        // Constructor
        public GumballMachine(int count) 
        {
            this.count = count;

            soldOutState = new SoldOutState(this);
            noQuarterState = new NoQuarterState(this);
            hasQuarterState = new HasQuarterState(this);
            soldState = new SoldState(this);

            if (count > 0) 
            {
                state = noQuarterState;
            } 
            else
            {
                state = soldOutState;
            }
        }
 
        public void InsertQuarter() 
        {
            state.InsertQuarter();
        }
 
        public void EjectQuarter() 
        {
            state.EjectQuarter();
        }
 
        public void TurnCrank() 
        {
            state.TurnCrank();
            state.Dispense();
        }

        public void ReleaseBall() 
        {
            Console.WriteLine("A gumball comes rolling out the slot...");
            if (count != 0) 
            {
                count = count - 1;
            }
        }

        public void Refill(int count) 
        {
            this.count = count;
            state = noQuarterState;
        }

        // Properties
        public IState State 
        {
            set{ this.state = value; }
        }
 
        public int Count
        {
            get{ return count; }
        }
 
        public IState SoldOutState
        {
            get{ return soldOutState; }
        }

        public IState NoQuarterState
        {
            get{ return noQuarterState; }
        }

        public IState HasQuarterState
        {
            get{ return hasQuarterState; }
        }

        public IState SoldState 
        {
            get{ return soldState; }
        }
 
        public override string ToString() 
        {
            StringBuilder result = new StringBuilder();
            result.Append("\nMighty Gumball, Inc.");
            result.Append("\n.NET-enabled Standing Gumball Model #2004");
            result.Append("\nInventory: " + count + " gumball");
            if (count != 1) 
            {
                result.Append("s");
            }
            result.Append("\n");
            result.Append("Machine is " + state + "\n");
            return result.ToString();
        }
    }

    #endregion

    #region State

    public interface IState 
    {
         void InsertQuarter();
        void EjectQuarter();
        void TurnCrank();
        void Dispense();
    }

    public class HasQuarterState : IState 
    {
        GumballMachine gumballMachine;

        // Constructor
        public HasQuarterState(GumballMachine gumballMachine) 
        {
            this.gumballMachine = gumballMachine;
        }
  
        public void InsertQuarter() 
        {
            Console.WriteLine("You can't insert another quarter");
        }
 
        public void EjectQuarter() 
        {
            Console.WriteLine("Quarter returned");
            gumballMachine.State = gumballMachine.NoQuarterState;
        }
 
        public void TurnCrank() 
        {
            Console.WriteLine("You turned...");
            gumballMachine.State = gumballMachine.SoldState;
        }

        public void Dispense() 
        {
            Console.WriteLine("No gumball dispensed");
        }
 
        public override string ToString() 
        {
            return "waiting for turn of crank";
        }
    }

    public class NoQuarterState : IState 
    {
        GumballMachine gumballMachine;
 
        // Constructor
        public NoQuarterState(GumballMachine gumballMachine) 
        {
            this.gumballMachine = gumballMachine;
        }
 
        public void InsertQuarter() 
        {
            Console.WriteLine("You inserted a quarter");
            gumballMachine.State = gumballMachine.HasQuarterState;
        }
 
        public void EjectQuarter() 
        {
            Console.WriteLine("You haven't inserted a quarter");
        }
 
        public void TurnCrank() 
        {
            Console.WriteLine("You turned, but there's no quarter");
        }
 
        public void Dispense() 
        {
            Console.WriteLine("You need to pay first");
        } 
 
        public override string ToString() 
        {
            return "waiting for quarter";
        }
    }

    public class SoldOutState : IState 
    {
        GumballMachine gumballMachine;
 
        // Constructor
        public SoldOutState(GumballMachine gumballMachine) 
        {
            this.gumballMachine = gumballMachine;
        }
 
        public void InsertQuarter() 
        {
            Console.WriteLine("You can't insert a quarter, the machine is sold out");
        }
 
        public void EjectQuarter() 
        {
            Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
        }
 
        public void TurnCrank() 
        {
            Console.WriteLine("You turned, but there are no gumballs");
        }
 
        public void Dispense() 
        {
            Console.WriteLine("No gumball dispensed");
        }
 
        public override string ToString() 
        {
            return "sold out";
        }
    }

    public class SoldState : IState 
    {
 
        GumballMachine gumballMachine;
 
        // Constructor
        public SoldState(GumballMachine gumballMachine) 
        {
            this.gumballMachine = gumballMachine;
        }
       
        public void InsertQuarter() 
        {
            Console.WriteLine("Please wait, we're already giving you a gumball");
        }
 
        public void EjectQuarter() 
        {
            Console.WriteLine("Sorry, you already turned the crank");
        }
 
        public void TurnCrank() 
        {
            Console.WriteLine("Turning twice doesn't get you another gumball!");
        }
 
        public void Dispense() 
        {
            gumballMachine.ReleaseBall();
            if (gumballMachine.Count > 0) 
            {
                gumballMachine.State = gumballMachine.NoQuarterState;
            } 
            else 
            {
                Console.WriteLine("Oops, out of gumballs!");
                gumballMachine.State = gumballMachine.SoldOutState;
            }
        }
 
        public override string ToString() 
        {
            return "dispensing a gumball";
        }
    }
    #endregion
}
