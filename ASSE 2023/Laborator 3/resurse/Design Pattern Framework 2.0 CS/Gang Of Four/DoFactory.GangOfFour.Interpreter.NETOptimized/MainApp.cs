using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Interpreter.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Interpreter Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            string roman = "MCMXXVIII";
            Context context = new Context(roman);

            // Build the 'parse tree'
            List<Expression> tree = new List<Expression>();
            //ArrayList tree = new ArrayList();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            // Interpret
            foreach (Expression expression in tree)
            {
                expression.Interpret(context);
            }

            Console.WriteLine("{0} = {1}", roman, context.Output);

            // Wait for user
            Console.Read();
        }
    }

    // "Context"

    class Context
    {
        private string input;
        private int output;

        // Constructor
        public Context(string input)
        {
            this.input = input;
        }

        // Properties
        public string Input
        {
            get{ return input; }
            set{ input = value; }
        }

        public int Output
        {
            get{ return output; }
            set{ output = value; }
        }
    }

    // "AbstractExpression"

    abstract class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0) 
                return;

            if (context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }

            while (context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }

        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();
    }

    // Thousand checks for the Roman Numeral M
    // "TerminalExpression"

    class ThousandExpression : Expression
    {
        public override string One() { return "M"; }
        public override string Four(){ return " "; }
        public override string Five(){ return " "; }
        public override string Nine(){ return " "; }
        public override int Multiplier() { return 1000; }
    }

    // Hundred checks C, CD, D or CM
    // "TerminalExpression"

    class HundredExpression : Expression
    {
        public override string One() { return "C"; }
        public override string Four(){ return "CD"; }
        public override string Five(){ return "D"; }
        public override string Nine(){ return "CM"; }
        public override int Multiplier() { return 100; }
    }

    // Ten checks for X, XL, L and XC
    // "TerminalExpression"

    class TenExpression : Expression
    {
        public override string One() { return "X"; }
        public override string Four(){ return "XL"; }
        public override string Five(){ return "L"; }
        public override string Nine(){ return "XC"; }
        public override int Multiplier() { return 10; }
    }

    // One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX
    // "TerminalExpression"

    class OneExpression : Expression
    {
        public override string One() { return "I"; }
        public override string Four(){ return "IV"; }
        public override string Five(){ return "V"; }
        public override string Nine(){ return "IX"; }
        public override int Multiplier() { return 1; }
    }
}
