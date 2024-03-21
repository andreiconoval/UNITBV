using System;

namespace DoFactory.HeadFirst.Command.SimpleRemote
{
    class RemoteControlTest
    {
        static void Main(string[] args)
        {
            SimpleRemoteControl remote = new SimpleRemoteControl();

            Light light = new Light();
            LightOnCommand lightOn = new LightOnCommand(light);

            GarageDoor garageDoor = new GarageDoor();
            GarageDoorOpenCommand garageOpen = new GarageDoorOpenCommand(garageDoor);
 
            remote.Command = lightOn;
            remote.ButtonWasPressed();

            remote.Command = garageOpen;
            remote.ButtonWasPressed();

            // Wait for user
            Console.Read();
        }    
    }

    #region SimpleRemoteControl
      
    // The invoker

    public class SimpleRemoteControl 
    {
        private ICommand slot;
 
        public SimpleRemoteControl() {}
 
        public ICommand Command 
        {
            set{ slot = value; }
        }
 
        public void ButtonWasPressed() 
        {
            slot.Execute();
        }
    }

    #endregion

    #region Command

    public interface ICommand 
    {
        void Execute();
    }

    public class LightOnCommand : ICommand 
    {
        private Light light;
  
        public LightOnCommand(Light light) 
        {
            this.light = light;
        }
 
        public void Execute() 
        {
            light.On();
        }
    }

    public class GarageDoorOpenCommand : ICommand 
    {
        private GarageDoor garageDoor;

        public GarageDoorOpenCommand(GarageDoor garageDoor) 
        {
            this.garageDoor = garageDoor;
        }

        public void Execute() 
        {
            garageDoor.Up();
        }
    }

    public class LightOffCommand : ICommand 
    {
        private Light light;
 
        public LightOffCommand(Light light) 
        {
            this.light = light;
        }
 
        public void Execute() 
        {
            light.Off();
        }
    }

    #endregion

    #region Light, GarageDoor

    public class Light 
    {
        public void On() 
        {
            Console.WriteLine("Light is on");
        }

        public void Off() 
        {
            Console.WriteLine("Light is off");
        }
    }

    public class GarageDoor 
    {
        public void Up() 
        {
            Console.WriteLine("Garage Door is Open");
        }

        public void Down() 
        {
            Console.WriteLine("Garage Door is Closed");
        }

        public void Stop() 
        {
            Console.WriteLine("Garage Door is Stopped");
        }

        public void LightOn() 
        {
            Console.WriteLine("Garage light is on");
        }

        public void LightOff() 
        {
            Console.WriteLine("Garage light is off");
        }
    }
    #endregion
}
