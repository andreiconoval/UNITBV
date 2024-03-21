using System;
using System.Text;

namespace DoFactory.HeadFirst.State.Gumball
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
            gumballMachine.EjectQuarter();
            gumballMachine.TurnCrank();

            Console.WriteLine(gumballMachine);

            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            gumballMachine.EjectQuarter();

            Console.WriteLine(gumballMachine);

            gumballMachine.InsertQuarter();
            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();

            Console.WriteLine(gumballMachine);

            // Wait for user
            Console.Read();
        }
    }

    #region GumballMachine

    public class GumballMachine 
    {
        GumballMachineState state = GumballMachineState.SoldOut;
        int count = 0;
  
        public GumballMachine(int count) 
        {
            this.count = count;
            if (count > 0) 
            {
                state = GumballMachineState.NoQuarter;
            }
        }
  
        public void InsertQuarter() 
        {
            if (state == GumballMachineState.HasQuarter) 
            {
                Console.WriteLine("You can't insert another quarter");
            } 
            else if (state == GumballMachineState.NoQuarter) 
            {
                state = GumballMachineState.HasQuarter;
                Console.WriteLine("You inserted a quarter");
            } 
            else if (state == GumballMachineState.SoldOut) 
            {
                Console.WriteLine("You can't insert a quarter, the machine is sold out");
            } 
            else if (state == GumballMachineState.Sold) 
            {
                Console.WriteLine("Please wait, we're already giving you a gumball");
            }
        }

        public void EjectQuarter() 
        {
            if (state == GumballMachineState.HasQuarter) 
            {
                Console.WriteLine("Quarter returned");
                state = GumballMachineState.NoQuarter;
            } 
            else if (state == GumballMachineState.NoQuarter) 
            {
                Console.WriteLine("You haven't inserted a quarter");
            } 
            else if (state == GumballMachineState.Sold) 
            {
                Console.WriteLine("Sorry, you already turned the crank");
            } 
            else if (state == GumballMachineState.SoldOut) 
            {
                Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
            }
        }
 
        public void TurnCrank() 
        {
            if (state == GumballMachineState.Sold) 
            {
                Console.WriteLine("Turning twice doesn't get you another gumball!");
            } 
            else if (state == GumballMachineState.NoQuarter) 
            {
                Console.WriteLine("You turned but there's no quarter");
            } 
            else if (state == GumballMachineState.SoldOut) 
            {
                Console.WriteLine("You turned, but there are no gumballs");
            } 
            else if (state == GumballMachineState.HasQuarter) 
            {
                Console.WriteLine("You turned...");
                state = GumballMachineState.Sold;
                Dispense();
            }
        }
 
        public void Dispense() 
        {
            if (state == GumballMachineState.Sold) 
            {
                Console.WriteLine("A gumball comes rolling out the slot");
                count = count - 1;
                if (count == 0) 
                {
                    Console.WriteLine("Oops, out of gumballs!");
                    state = GumballMachineState.SoldOut;
                } 
                else 
                {
                    state = GumballMachineState.NoQuarter;
                }
            } 
            else if (state == GumballMachineState.NoQuarter) 
            {
                Console.WriteLine("You need to pay first");
            } 
            else if (state == GumballMachineState.SoldOut) 
            {
                Console.WriteLine("No gumball dispensed");
            } 
            else if (state == GumballMachineState.HasQuarter) 
            {
                Console.WriteLine("No gumball dispensed");
            }
        }
 
        public void refill(int numGumBalls) 
        {
            this.count = numGumBalls;
            state = GumballMachineState.NoQuarter;
        }

        public override string ToString() 
        {
            StringBuilder result = new StringBuilder();
            result.Append("\nMighty Gumball, Inc.");
            result.Append("\nJava-enabled Standing Gumball Model #2004\n");
            result.Append("Inventory: " + count + " gumball");
            
            if (count != 1) 
            {
                result.Append("s");
            }
            result.Append("\nMachine is ");
            if (state == GumballMachineState.SoldOut) 
            {
                result.Append("sold out");
            } 
            else if (state == GumballMachineState.NoQuarter) 
            {
                result.Append("waiting for quarter");
            } 
            else if (state == GumballMachineState.HasQuarter) 
            {
                result.Append("waiting for turn of crank");
            } 
            else if (state == GumballMachineState.Sold) 
            {
                result.Append("delivering a gumball");
            }
            result.Append("\n");
            return result.ToString();
        }
    }
    #endregion

    #region State

    // State enumercation

    public enum GumballMachineState
    {
        SoldOut,
        NoQuarter,
        HasQuarter,
        Sold
    }
    #endregion
}


