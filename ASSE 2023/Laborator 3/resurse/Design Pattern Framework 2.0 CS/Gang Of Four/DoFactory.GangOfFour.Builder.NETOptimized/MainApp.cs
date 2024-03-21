using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Builder.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Builder Design Pattern.
    /// </summary>
    public class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        public static void Main()
        {
            VehicleBuilder builder;

            // Create shop with vehicle builders
            Shop shop = new Shop();
            
            // Create builders and display vehicle
            builder = new ScooterBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new CarBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new MotorCycleBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            // Wait for user
            Console.Read();
        }
    }

    // "Director"

    class Shop
    {
        // Builder uses a complex series of steps
        public void Construct(VehicleBuilder vehicleBuilder)
        {
            vehicleBuilder.BuildFrame();
            vehicleBuilder.BuildEngine();
            vehicleBuilder.BuildWheels();
            vehicleBuilder.BuildDoors();
        }
    }

    // "Builder"

    abstract class VehicleBuilder
    {
        protected Vehicle vehicle;

        protected readonly string frame  = Part.Frame.ToString();
        protected readonly string engine = Part.Engine.ToString();
        protected readonly string wheels = Part.Wheels.ToString();
        protected readonly string doors = Part.Doors.ToString();

        // Constructor
        public VehicleBuilder(string name)
        {
            vehicle = new Vehicle(name);
        }

        // Property
        public Vehicle Vehicle
        {
            get{ return vehicle; }
        }

        public abstract void BuildFrame();
        public abstract void BuildEngine();
        public abstract void BuildWheels();
        public abstract void BuildDoors();
    }

    // "ConcreteBuilder1"

    class MotorCycleBuilder : VehicleBuilder
    {
        // Invoke base class constructor
        public MotorCycleBuilder() : base("MotorCycle")
        {
        }

        public override void BuildFrame()
        {
            vehicle[frame] = "MotorCycle Frame";
        }

        public override void BuildEngine()
        {
            vehicle[engine] = "500 cc";
        }

        public override void BuildWheels()
        {
            vehicle[wheels] = "2";
        }

        public override void BuildDoors()
        {
            vehicle[doors] = "0";
        }
    }

    // "ConcreteBuilder2"

    class CarBuilder : VehicleBuilder
    {
        // Invoke base class constructor
        public CarBuilder() : base("Car")
        {
        }

        public override void BuildFrame()
        {
            vehicle[frame] = "Car Frame";
        }

        public override void BuildEngine()
        {
            vehicle[engine] = "2500 cc";
        }

        public override void BuildWheels()
        {
            vehicle[wheels] = "4";
        }

        public override void BuildDoors()
        {
            vehicle[doors] = "4";
        }
    }

    // "ConcreteBuilder3"

    class ScooterBuilder : VehicleBuilder
    {
        // Invoke base class constructor
        public ScooterBuilder() : base("Scooter")
        {
        }

        public override void BuildFrame()
        {
            vehicle[frame] = "Scooter Frame";
        }

        public override void BuildEngine()
        {
            vehicle[engine] = "50 cc";
        }

        public override void BuildWheels()
        {
            vehicle[wheels] = "2";
        }

        public override void BuildDoors()
        {
            vehicle[doors] = "0";
        }
    }

    // "Product" 

    class Vehicle
    {
        private string type;
        private Dictionary<string, string> parts = new Dictionary<string, string>();

        // Constructor
        public Vehicle(string type)
        {
            this.type = type;
        }

        // Indexer 
        public string this[string key]
        {
            get{ return parts[key]; }
            set{ parts[key] = value; }
        }

        public void Show()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Vehicle Type: {0}", type);
            Console.WriteLine(" Frame  : {0}", 
                this[Part.Frame.ToString()]);
            Console.WriteLine(" Engine : {0}", 
                this[Part.Engine.ToString()]);
            Console.WriteLine(" #Wheels: {0}", 
                this[Part.Wheels.ToString()]);
            Console.WriteLine(" #Doors : {0}", 
                this[Part.Doors.ToString()]);
        }
    }

    // Part enumeration

    public enum Part
    {
        Frame,
        Engine,
        Wheels,
        Doors
    }
}

