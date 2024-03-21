using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary; 

namespace DoFactory.GangOfFour.Prototype.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Prototype Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            ColorManager colormanager = new ColorManager();

            // Initialize with standard colors
            colormanager["red"  ] = new Color(255,   0,   0);
            colormanager["green"] = new Color(  0, 255,   0);
            colormanager["blue" ] = new Color(  0,   0, 255);

            // User adds personalized colors
            colormanager["angry"] = new Color(255,  54,   0);
            colormanager["peace"] = new Color(128, 211, 128);
            colormanager["flame"] = new Color(211,  34,  20);

            // User uses selected colors
            Color color;

            string name = "red";
            color = colormanager[name].Clone() as Color;

            name = "peace";
            color = colormanager[name].Clone() as Color;

            // Create a "deep copy"
            name = "flame";
            color = colormanager[name].Clone(false) as Color;

            // Wait for user
            Console.Read();
        }
    }

    // "ConcretePrototype"

    [Serializable()]
    class Color : ICloneable
    {
        private byte red;
        private byte green;
        private byte blue;

        // Constructor
        public Color(byte red, byte green, byte blue)
        {
            this.red   = red;
            this.green = green;
            this.blue  = blue;
        }

        // Properties
        public byte Red
        {
            get{ return red; }
        }

        public byte Green
        {
            get{ return green; }
        }

        public byte Blue
        {
            get{ return blue; }
        }

        // Indicate shallow or deep copy
        public object Clone(bool shallow)
        {
            if (shallow)
            {
                return Clone();
            }
            else
            {
                return DeepCopy();
            }
        }

        // Create a shallow copy
        public object Clone()
        {
            Console.WriteLine(
                "Shallow copy of color RGB: {0,3},{1,3},{2,3}", 
                red, green, blue);

            return this.MemberwiseClone();
        }

        // Create a deep copy
        public object DeepCopy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
  
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);

            object copy = formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine(
                "*Deep*  copy of color RGB: {0,3},{1,3},{2,3}", 
                (copy as Color).Red, 
                (copy as Color).Green, 
                (copy as Color).Blue);

            return copy;
        }
    }

    // Type-safe prototype manager

    class ColorManager
    {
        Dictionary<string,Color> colors = new Dictionary<string,Color>();

        // Indexer
        public Color this[string name]
        {
            get{ return colors[name]; }
            set{ colors.Add(name, value); }
        }
    }
}
