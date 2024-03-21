using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Flyweight.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Flyweight Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Build a document with text
            string document = "AAZZBBZB";
            char[] chars = document.ToCharArray();

            CharacterFactory factory = new CharacterFactory();

            // extrinsic state
            int pointSize = 10;

            // For each character use a flyweight object
            foreach (char c in chars)
            {
                Character character = factory[c];
                character.Display(++pointSize);
            }

            // Wait for user
            Console.Read();
        }
    }

    // "FlyweightFactory"

    class CharacterFactory
    {
        private Dictionary<char,Character> characters = 
                       new Dictionary<char,Character>();

        // Indexer
        public Character this[char key]
        {
            get
            {
                // Uses "lazy initialization" -- i.e. only create when needed.
                Character character = null;
                if (characters.ContainsKey(key))
                {
                    character = characters[key];
                }
                else
                {
                    // Instead of a case statement with 26 cases (characters).
                    // First, get qualified class name, then dynamically create instance 
                    string name = this.GetType().Namespace + "." +
                                     "Character" + key.ToString();
                    character = (Character)Activator.CreateInstance
                                     (Type.GetType(name));
                }

                return character;
            }
        }
    }

    // "Flyweight" 

    class Character
    {
        protected char symbol;
        protected int width;
        protected int height;
        protected int ascent;
        protected int descent;

        public void Display(int pointSize)
        {
            Console.WriteLine(this.symbol +
                      " (pointsize " + pointSize + ")");
        }

    }

    // "ConcreteFlyweight"

    class CharacterA : Character
    {
        // Constructor
        public CharacterA()
        {
            this.symbol  = 'A';
            this.height  = 100;
            this.width   = 120;
            this.ascent  = 70;
            this.descent = 0;
        }
    }

    // "ConcreteFlyweight"

    class CharacterB : Character
    {
        // Constructor
        public CharacterB()
        {
            this.symbol  = 'B';
            this.height  = 100;
            this.width   = 140;
            this.ascent  = 72;
            this.descent = 0;
        }
    }

    // ... C, D, E, etc.

    // "ConcreteFlyweight"

    class CharacterZ : Character
    {
        // Constructor
        public CharacterZ()
        {
            this.symbol  = 'Z';
            this.height  = 100;
            this.width   = 100;
            this.ascent  = 68;
            this.descent = 0;
        }
    }
}
