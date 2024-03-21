using System;
using System.Text;

namespace DoFactory.HeadFirst.Command.Party
{
    class RemoteLoader
    {
        static void Main(string[] args)
        {
            RemoteControl remoteControl = new RemoteControl();

            Light light = new Light("Living Room");
            TV tv = new TV("Living Room");
            Stereo stereo = new Stereo("Living Room");
            Hottub hottub = new Hottub();
 
            LightOnCommand lightOn = new LightOnCommand(light);
            StereoOnCommand stereoOn = new StereoOnCommand(stereo);
            TVOnCommand tvOn = new TVOnCommand(tv);
            HottubOnCommand hottubOn = new HottubOnCommand(hottub);
            LightOffCommand lightOff = new LightOffCommand(light);
            StereoOffCommand stereoOff = new StereoOffCommand(stereo);
            TVOffCommand tvOff = new TVOffCommand(tv);
            HottubOffCommand hottubOff = new HottubOffCommand(hottub);

            ICommand[] partyOn = { lightOn, stereoOn, tvOn, hottubOn};
            ICommand[] partyOff = { lightOff, stereoOff, tvOff, hottubOff};
  
            MacroCommand partyOnMacro = new MacroCommand(partyOn);
            MacroCommand partyOffMacro = new MacroCommand(partyOff);
 
            remoteControl.SetCommand(0, partyOnMacro, partyOffMacro);
  
            Console.WriteLine(remoteControl);
            Console.WriteLine("--- Pushing Macro On---");
            remoteControl.OnButtonWasPushed(0);
            Console.WriteLine("\n--- Pushing Macro Off---");
            remoteControl.OffButtonWasPushed(0);

            // Wait for user
            Console.Read();
        }    
    }

    #region Remote Control

    public class RemoteControl 
    {
        ICommand[] onCommands;
        ICommand[] offCommands;
        ICommand undoCommand;
 
        // Constructor
        public RemoteControl() 
        {
            onCommands  = new ICommand[7];
            offCommands = new ICommand[7];
 
            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7 ;i++) 
            {
                onCommands[i]  = noCommand;
                offCommands[i] = noCommand;
            }
            undoCommand = noCommand;
        }
  
        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) 
        {
            onCommands[slot]  = onCommand;
            offCommands[slot] = offCommand;
        }
 
        public void OnButtonWasPushed(int slot) 
        {
            onCommands[slot].Execute();
            undoCommand = onCommands[slot];
        }
 
        public void OffButtonWasPushed(int slot) 
        {
            offCommands[slot].Execute();
            undoCommand = offCommands[slot];
        }

        public void UndoButtonWasPushed() 
        {
            undoCommand.Undo();
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
            sb.Append("[undo] " + undoCommand.GetType().Name + "\n");
            return sb.ToString();
        }
    }

    #endregion

    #region Commands

    public interface ICommand 
    {
        void Execute();
        void Undo();
    }

    public class NoCommand : ICommand 
    {
        public void Execute() { }
        public void Undo() { }
    }

    public class MacroCommand : ICommand 
    {
        ICommand[] commands;
 
        public MacroCommand(ICommand[] commands) 
        {
            this.commands = commands;
        }
 
        public void Execute() 
        {
            for (int i = 0; i < commands.Length; i++) 
            {
                commands[i].Execute();
            }
        }
 
        public void Undo() 
        {
            for (int i = 0; i < commands.Length; i++) 
            {
                commands[i].Undo();
            }
        }
    }

    public class TVOnCommand : ICommand 
    {
        TV tv;

        public TVOnCommand(TV tv) 
        {
            this.tv = tv;
        }

        public void Execute() 
        {
            tv.On();
            tv.SetInputChannel();
        }

        public void Undo() 
        {
            tv.Off();
        }
    }

    public class TVOffCommand : ICommand 
    {
        TV tv;

        public TVOffCommand(TV tv) 
        {
            this.tv= tv;
        }

        public void Execute() 
        {
            tv.Off();
        }

        public void Undo() 
        {
            tv.On();
        }
    }

    public class StereoOnCommand : ICommand 
    {
        Stereo stereo;

        public StereoOnCommand(Stereo stereo) 
        {
            this.stereo = stereo;
        }

        public void Execute() 
        {
            stereo.On();
        }

        public void Undo() 
        {
            stereo.Off();
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

        public void Undo() 
        {
            stereo.On();
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

        public void Undo() 
        {
            stereo.Off();
        }
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

        public void Undo() 
        {
            light.Off();
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

        public void Undo() 
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

        public void Undo() 
        {
            light.Off();
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

        public void Undo() 
        {
            light.On();
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
            hottub.SetTemperature(98);
            hottub.Off();
        }

        public void Undo() 
        {
            hottub.On();
        }
    }

    public class CeilingFanOffCommand : ICommand 
    {
        CeilingFan ceilingFan;
        CeilingFanSpeed prevSpeed;

        public CeilingFanOffCommand(CeilingFan ceilingFan) 
        {
            this.ceilingFan = ceilingFan;
        }

        public void Execute() 
        {
            prevSpeed = ceilingFan.Speed;
            ceilingFan.Off();
        }

        public void Undo() 
        {
            switch (prevSpeed) 
            {
                case CeilingFanSpeed.High: ceilingFan.high(); break;
                case CeilingFanSpeed.Medium: ceilingFan.medium(); break;
                case CeilingFanSpeed.Low: ceilingFan.low(); break;
                case CeilingFanSpeed.Off: ceilingFan.Off(); break;
            }
        }
    }

    public class CeilingFanMediumCommand : ICommand 
    {
        CeilingFan ceilingFan;
        CeilingFanSpeed prevSpeed;

        public CeilingFanMediumCommand(CeilingFan ceilingFan) 
        {
            this.ceilingFan = ceilingFan;
        }

        public void Execute() 
        {
            prevSpeed = ceilingFan.Speed;
            ceilingFan.medium();
        }

        public void Undo() 
        {
            switch (prevSpeed) 
            {
                case CeilingFanSpeed.High: ceilingFan.high(); break;
                case CeilingFanSpeed.Medium: ceilingFan.medium(); break;
                case CeilingFanSpeed.Low: ceilingFan.low(); break;
                case CeilingFanSpeed.Off: ceilingFan.Off(); break;
            }
        }
    }

    public class CeilingFanHighCommand : ICommand 
    {
        CeilingFan ceilingFan;
        CeilingFanSpeed prevSpeed;

        public CeilingFanHighCommand(CeilingFan ceilingFan) 
        {
            this.ceilingFan = ceilingFan;
        }

        public void Execute() 
        {
            prevSpeed = ceilingFan.Speed;
            ceilingFan.high();
        }

        public void Undo() 
        {
            switch (prevSpeed) 
            {
                case CeilingFanSpeed.High: ceilingFan.high(); break;
                case CeilingFanSpeed.Medium: ceilingFan.medium(); break;
                case CeilingFanSpeed.Low: ceilingFan.low(); break;
                case CeilingFanSpeed.Off: ceilingFan.Off(); break;
            }
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
            hottub.SetTemperature(104);
            hottub.Circulate();
        }

        public void Undo() 
        {
            hottub.Off();
        }
    }

    #endregion

    #region TV, Tub, CeilingFan, etc

    public class Hottub 
    {
        bool on;
        int temperature;

        public void On() 
        {
            on = true;
        }

        public void Off() 
        {
            on = false;
        }

        public void Circulate() 
        {
            if (on) 
            {
                Console.WriteLine("Hottub is bubbling!");
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
            if (temperature > this.temperature) 
            {
                Console.WriteLine("Hottub is heating to a steaming " + temperature + " degrees");
            }
            else 
            {
                Console.WriteLine("Hottub is cooling to " + temperature + " degrees");
            }
            this.temperature = temperature;
        }
    }


    public class TV 
    {
        String location;
        int channel;

        public TV(String location) 
        {
            this.location = location;
        }

        public void On() 
        {
            Console.WriteLine(location + " TV is on");
        }

        public void Off() 
        {
            Console.WriteLine(location + " TV is off");
        }

        public void SetInputChannel() 
        {
            this.channel = 3;
            Console.WriteLine(location + " TV channel " + channel + " is set for DVD");
        }
    }

    public class Stereo 
    {
        String location;

        public Stereo(String location) 
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

        public void setDVD() 
        {
            Console.WriteLine(location + " stereo is set for DVD input");
        }

        public void SetRadio() 
        {
            Console.WriteLine(location + " stereo is set for Radio");
        }

        public void SetVolume(int volume) 
        {
            // code to set the volume
            // valid range: 1-11 (after all 11 is better than 10, right?)
            Console.WriteLine(location + " Stereo volume set to " + volume);
        }
    }

    public class Light 
    {
        String location;
        int level;

        public Light(String location) 
        {
            this.location = location;
        }

        public void On() 
        {
            level = 100;
            Console.WriteLine("Light is on");
        }

        public void Off() 
        {
            level = 0;
            Console.WriteLine("Light is off");
        }

        public void Dim(int level) 
        {
            this.level = level;
            if (level == 0) 
            {
                Off();
            }
            else 
            {
                Console.WriteLine("Light is dimmed to " + level + "%");
            }
        }

        public int Level
        {
            get{ return level; }
        }
    }

    public class CeilingFan 
    {
        string location;
        CeilingFanSpeed speed;
 
        public CeilingFan(string location) 
        {
            this.location = location;
        }
  
        public void high() 
        {
            // turns the ceiling fan on to high
            speed = CeilingFanSpeed.High;
            Console.WriteLine(location + " ceiling fan is on high");
        } 

        public void medium() 
        {
            // turns the ceiling fan on to medium
            speed = CeilingFanSpeed.Medium;
            Console.WriteLine(location + " ceiling fan is on medium");
        }

        public void low() 
        {
            // turns the ceiling fan on to low
            speed = CeilingFanSpeed.Low;
            Console.WriteLine(location + " ceiling fan is on low");
        }
 
        public void Off() 
        {
            // turns the ceiling fan off
            speed = CeilingFanSpeed.Off;
            Console.WriteLine(location + " ceiling fan is off");
        }
 
        public CeilingFanSpeed Speed
        {
            get { return speed; }
        }
    }

    public enum CeilingFanSpeed
    {
        High,
        Medium,
        Low,
        Off
    }
    #endregion
}
