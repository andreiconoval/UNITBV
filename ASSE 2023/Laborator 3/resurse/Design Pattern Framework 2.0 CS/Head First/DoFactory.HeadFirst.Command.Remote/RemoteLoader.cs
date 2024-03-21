using System;
using System.Text;

namespace DoFactory.HeadFirst.Command.Remote
{
    class RemoteLoader
    {
        static void Main(string[] args)
        {
            RemoteControl remoteControl = new RemoteControl();
 
            Light livingRoomLight = new Light("Living Room");
            Light kitchenLight    = new Light("Kitchen");
            CeilingFan ceilingFan = new CeilingFan("Living Room");
            GarageDoor garageDoor = new GarageDoor("");
            Stereo stereo         = new Stereo("Living Room");
  
            LightOnCommand livingRoomLightOn = 
                new LightOnCommand(livingRoomLight);

            LightOffCommand livingRoomLightOff = 
                new LightOffCommand(livingRoomLight);

            LightOnCommand kitchenLightOn = 
                new LightOnCommand(kitchenLight);

            LightOffCommand kitchenLightOff = 
                new LightOffCommand(kitchenLight);
  
            CeilingFanOnCommand ceilingFanOn = 
                new CeilingFanOnCommand(ceilingFan);

            CeilingFanOffCommand ceilingFanOff = 
                new CeilingFanOffCommand(ceilingFan);
 
            GarageDoorUpCommand garageDoorUp =
                new GarageDoorUpCommand(garageDoor);

            GarageDoorDownCommand garageDoorDown =
                new GarageDoorDownCommand(garageDoor);
 
            StereoOnWithCDCommand stereoOnWithCD =
                new StereoOnWithCDCommand(stereo);

            StereoOffCommand  stereoOff =
                new StereoOffCommand(stereo);
 
            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
            remoteControl.SetCommand(1, kitchenLightOn, kitchenLightOff);
            remoteControl.SetCommand(2, ceilingFanOn, ceilingFanOff);
            remoteControl.SetCommand(3, stereoOnWithCD, stereoOff);
  
            Console.WriteLine(remoteControl);
 
            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushed(0);
            remoteControl.OnButtonWasPushed(1);
            remoteControl.OffButtonWasPushed(1);
            remoteControl.OnButtonWasPushed(2);
            remoteControl.OffButtonWasPushed(2);
            remoteControl.OnButtonWasPushed(3);
            remoteControl.OffButtonWasPushed(3);

            // Wait for user
            Console.Read();
        }    
    }

    #region RemoteControl

    // This is the invoker

    public class RemoteControl 
    {
        ICommand[] onCommands;
        ICommand[] offCommands;
 
        public RemoteControl() 
        {
            onCommands = new ICommand[7];
            offCommands = new ICommand[7];
 
            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7; i++) 
            {
                onCommands[i] = noCommand;
                offCommands[i] = noCommand;
            }
        }
  
        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) 
        {
            onCommands[slot] = onCommand;
            offCommands[slot] = offCommand;
        }
 
        public void OnButtonWasPushed(int slot) 
        {
            onCommands[slot].Execute();
        }
 
        public void OffButtonWasPushed(int slot) 
        {
            offCommands[slot].Execute();
        }
  
        public override string ToString() 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n------ Remote Control -------\n");
            for (int i = 0; i < onCommands.Length; i++) 
            {
                sb.Append("[slot " + i + "] " + onCommands[i].GetType().Name
                    + "    " + offCommands[i].GetType().Name + "\n");
            }

            return sb.ToString();
        }
    }
    #endregion

    #region Commands

    public interface ICommand 
    {
        void Execute();
    }

    public class NoCommand : ICommand 
    {
        public void Execute() { }
    }

    public class LivingroomLightOnCommand : ICommand 
    {
        Light light;

        public LivingroomLightOnCommand(Light light) 
        {
            this.light = light;
        }

        public void Execute() 
        {
            light.On();
        }
    }

    public class LightOnCommand : ICommand 
    {
        Light light;

        public LightOnCommand(Light light) 
        {
            this.light = light;
        }

        public void Execute() 
        {
            light.On();
        }
    }

    public class LightOffCommand : ICommand 
    {
        Light light;
 
        public LightOffCommand(Light light) 
        {
            this.light = light;
        }
 
        public void Execute() 
        {
            light.Off();
        }
    }
    public class GarageDoorUpCommand : ICommand 
    {
        GarageDoor garageDoor;

        public GarageDoorUpCommand(GarageDoor garageDoor) 
        {
            this.garageDoor = garageDoor;
        }

        public void Execute() 
        {
            garageDoor.up();
        }
    }

    public class GarageDoorDownCommand : ICommand 
    {
        GarageDoor garageDoor;

        public GarageDoorDownCommand(GarageDoor garageDoor) 
        {
            this.garageDoor = garageDoor;
        }

        public void Execute() 
        {
            garageDoor.up();
        }
    }

    public class CeilingFanOnCommand : ICommand 
    {
        CeilingFan ceilingFan;

        public CeilingFanOnCommand(CeilingFan ceilingFan) 
        {
            this.ceilingFan = ceilingFan;
        }
        public void Execute() 
        {
            ceilingFan.High();
        }
    }

    public class CeilingFanOffCommand : ICommand 
    {
        CeilingFan ceilingFan;

        public CeilingFanOffCommand(CeilingFan ceilingFan) 
        {
            this.ceilingFan = ceilingFan;
        }
        public void Execute() 
        {
            ceilingFan.Off();
        }
    }

    public class LivingroomLightOffCommand : ICommand 
    {
        Light light;

        public LivingroomLightOffCommand(Light light) 
        {
            this.light = light;
        }

        public void Execute() 
        {
            light.Off();
        }
    }

    public class HottubOnCommand : ICommand 
    {
        Hottub hottub;

        public HottubOnCommand(Hottub hottub) 
        {
            this.hottub = hottub;
        }

        public void Execute() 
        {
            hottub.On();
            hottub.Heat();
            hottub.BubblesOn();
        }
    }

    public class HottubOffCommand : ICommand 
    {
        Hottub hottub;

        public HottubOffCommand(Hottub hottub) 
        {
            this.hottub = hottub;
        }

        public void Execute() 
        {
            hottub.Cool();
            hottub.Off();
        }
    }

    public class StereoOnWithCDCommand : ICommand 
    {
        Stereo stereo;
 
        public StereoOnWithCDCommand(Stereo stereo) 
        {
            this.stereo = stereo;
        }
 
        public void Execute() 
        {
            stereo.On();
            stereo.SetCD();
            stereo.SetVolume(11);
        }
    }

    public class StereoOffCommand : ICommand 
    {
        Stereo stereo;
 
        public StereoOffCommand(Stereo stereo) 
        {
            this.stereo = stereo;
        }
 
        public void Execute() 
        {
            stereo.Off();
        }
    }

    #endregion

    #region Light, TV, GarageDoor, CeilingFan, etc

    public class Light 
    {
        string location = "";

        public Light(string location) 
        {
            this.location = location;
        }

        public void On() 
        {
            Console.WriteLine(location + " light is on");
        }

        public void Off() 
        {
            Console.WriteLine(location + " light is off");
        }
    }

    public class GarageDoor 
    {
        string location;

        public GarageDoor(string location) 
        {
            this.location = location;
        }

        public void up() 
        {
            Console.WriteLine(location + " garage Door is Up");
        }

        public void down() 
        {
            Console.WriteLine(location + " garage Door is Down");
        }

        public void stop() 
        {
            Console.WriteLine(location + " garage Door is Stopped");
        }

        public void lightOn() 
        {
            Console.WriteLine(location + " garage light is on");
        }

        public void lightOff() 
        {
            Console.WriteLine(location + " garage light is off");
        }
    }

    public class CeilingFan 
    {
        string location = "";
        int level;
        public static readonly int HIGH = 2;
        public static readonly int MEDIUM = 1;
        public static readonly int LOW = 0;
 
        public CeilingFan(string location) 
        {
            this.location = location;
        }
  
        public void High() 
        {
            // turns the ceiling fan on to high
            level = HIGH;
            Console.WriteLine(location + " ceiling fan is on high");
        } 

        public void Medium() 
        {
            // turns the ceiling fan on to medium
            level = MEDIUM;
            Console.WriteLine(location + " ceiling fan is on medium");
        }

        public void Low() 
        {
            // turns the ceiling fan on to low
            level = LOW;
            Console.WriteLine(location + " ceiling fan is on low");
        }
 
        public void Off() 
        {
            // turns the ceiling fan off
            level = 0;
            Console.WriteLine(location + " ceiling fan is off");
        }
 
        public int getSpeed() 
        {
            return level;
        }
    }

    public class Hottub 
    {
        bool on;
        int temperature;

        public Hottub() 
        {
        }

        public void On() 
        {
            on = true;
        }

        public void Off() 
        {
            on = false;
        }

        public void BubblesOn() 
        {
            if (on) 
            {
                Console.WriteLine("Hottub is bubbling!");
            }
        }

        public void BubblesOff() 
        {
            if (on) 
            {
                Console.WriteLine("Hottub is not bubbling");
            }
        }

        public void JetsOn() 
        {
            if (on) 
            {
                Console.WriteLine("Hottub jets are on");
            }
        }

        public void JetsOff() 
        {
            if (on) 
            {
                Console.WriteLine("Hottub jets are off");
            }
        }

        public void SetTemperature(int temperature) 
        {
            this.temperature = temperature;
        }

        public void Heat() 
        {
            temperature = 105;
            Console.WriteLine("Hottub is heating to a steaming 105 degrees");
        }

        public void Cool() 
        {
            temperature = 98;
            Console.WriteLine("Hottub is cooling to 98 degrees");
        }
    }

    public class TV 
    {
        string location;
        int channel;

        public TV(string location) 
        {
            this.location = location;
        }

        public void On() 
        {
            Console.WriteLine("TV is on");
        }

        public void Off() 
        {
            Console.WriteLine("TV is off");
        }

        public void SetInputChannel() 
        {
            this.channel = 3;
            Console.WriteLine("Channel " + channel + " is set for VCR");
        }
    }

    public class Stereo 
    {
        string location;

        public Stereo(string location) 
        {
            this.location = location;
        }

        public void On() 
        {
            Console.WriteLine(location + " stereo is on");
        }

        public void Off() 
        {
            Console.WriteLine(location + " stereo is off");
        }

        public void SetCD() 
        {
            Console.WriteLine(location + " stereo is set for CD input");
        }

        public void SetDVD() 
        {
            Console.WriteLine(location + " stereo is set for DVD input");
        }

        public void SetRadio() 
        {
            Console.WriteLine(location + " stereo is set for Radio");
        }

        public void SetVolume(int volume) 
        {
            // Code to set the volume
            // Valid range: 1-11 (after all 11 is better than 10, right?)
            Console.WriteLine(location + " Stereo volume set to " + volume);
        }
    }
    #endregion
}
