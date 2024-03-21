using System;
using System.Text;

namespace DoFactory.HeadFirst.State.GumballStateWinner
{
    class GumballMachineTestDrive
    {
        static void Main(string[] args)
        {
            GumballMachine machine = new GumballMachine(10);

            Console.WriteLine(machine);

            machine.InsertQuarter();
            machine.TurnCrank();
            machine.InsertQuarter();
            machine.TurnCrank();

            Console.WriteLine(machine);

            machine.InsertQuarter();
            machine.TurnCrank();
            machine.InsertQuarter();
            machine.TurnCrank();

            Console.WriteLine(machine);

            machine.InsertQuarter();
            machine.TurnCrank();
            machine.InsertQuarter();
            machine.TurnCrank();

            Console.WriteLine(machine);

            machine.InsertQuarter();
            machine.TurnCrank();
            machine.InsertQuarter();
            machine.TurnCrank();

            Console.WriteLine(machine);

            machine.InsertQuarter();
            machine.TurnCrank();
            machine.InsertQuarter();
            machine.TurnCrank();

            Console.WriteLine(machine);

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
        IState winnerState;
 
        IState state;
        private int count = 0;
 
        public GumballMachine(int count) 
        {
            soldOutState = new SoldOutState(this);
            noQuarterState = new NoQuarterState(this);
            hasQuarterState = new HasQuarterState(this);
            soldState = new SoldState(this);
            winnerState = new WinnerState(this);

            this.count = count;
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

        public IState State
        {
            set{ state = value; }
            get{ return state; }
        }
 
        public void ReleaseBall() 
        {
            if (count > 0) 
            {
                Console.WriteLine("A gumball comes rolling out the slot...");
                count--;
            }
        }
 
        public int Count
        {
            get{ return count; }
        }
 
        void Refill(int count) 
        {
            this.count = count;
            state = noQuarterState;
        }

        public IState GetSoldOutState() 
        {
            return soldOutState;
        }

        public IState GetNoQuarterState() 
        {
            return noQuarterState;
        }

        public IState GetHasQuarterState() 
        {
            return hasQuarterState;
        }

        public IState GetSoldState() 
        {
            return soldState;
        }

        public IState GetWinnerState() 
        {
            return winnerState;
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

    public class SoldState : IState 
    {
        GumballMachine machine;
 
        public SoldState(GumballMachine machine) 
        {
            this.machine = machine;
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
            machine.ReleaseBall();
            if (machine.Count > 0) 
            {
                machine.State = machine.GetNoQuarterState();
            } 
            else 
            {
                Console.WriteLine("Oops, out of gumballs!");
                machine.State = machine.GetSoldOutState();
            }
        }
 
        public override string ToString() 
        {
            return "dispensing a gumball";
        }
    }

    public class SoldOutState : IState 
    {
        GumballMachine machine;
 
        public SoldOutState(GumballMachine machine) 
        {
            this.machine = machine;
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

    public class NoQuarterState : IState 
    {
        GumballMachine machine;
 
        public NoQuarterState(GumballMachine machine) 
        {
            this.machine = machine;
        }
 
        public void InsertQuarter() 
        {
            Console.WriteLine("You inserted a quarter");
            machine.State = machine.GetHasQuarterState();
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

    public class HasQuarterState : IState 
    {
        GumballMachine machine;
 
        public HasQuarterState(GumballMachine machine) 
        {
            this.machine = machine;
        }
  
        public void InsertQuarter() 
        {
            Console.WriteLine("You can't insert another quarter");
        }
 
        public void EjectQuarter() 
        {
            Console.WriteLine("Quarter returned");
            machine.State = machine.GetNoQuarterState();
        }
 
        public void TurnCrank() 
        {
            Console.WriteLine("You turned...");
            Random random = new Random();
            
            int winner = random.Next(11); 
            if ((winner == 0) && (machine.Count > 1)) 
            {
                machine.State = machine.GetWinnerState();
            } 
            else 
            {
                machine.State = machine.GetSoldState();
            }
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

    public class WinnerState : IState 
    {
        GumballMachine machine;
 
        public WinnerState(GumballMachine machine) 
        {
            this.machine = machine;
        }
 
        public void InsertQuarter() 
        {
            Console.WriteLine("Please wait, we're already giving you a Gumball");
        }
 
        public void EjectQuarter() 
        {
            Console.WriteLine("Please wait, we're already giving you a Gumball");
        }
 
        public void TurnCrank() 
        {
            Console.WriteLine("Turning again doesn't get you another gumball!");
        }
 
        public void Dispense() 
        {
            Console.WriteLine("YOU'RE A WINNER! You get two gumballs for your quarter");
            machine.ReleaseBall();
            if (machine.Count == 0) 
            {
                machine.State = machine.GetSoldOutState();
            } 
            else 
            {
                machine.ReleaseBall();
                if (machine.Count > 0) 
                {
                    machine.State = machine.GetNoQuarterState();
                } 
                else 
                {
                    Console.WriteLine("Oops, out of gumballs!");
                    machine.State = machine.GetSoldOutState();
                }
            }
        }
 
        public override string ToString() 
        {
            return "despensing two gumballs for your quarter, because YOU'RE A WINNER!";
        }
    }
    #endregion
}
