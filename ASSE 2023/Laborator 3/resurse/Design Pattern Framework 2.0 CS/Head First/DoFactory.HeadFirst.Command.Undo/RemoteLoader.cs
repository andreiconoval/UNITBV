using System;
using System.Text;

namespace DoFactory.HeadFirst.Command.Undo
{
    class RemoteLoader
    {
        static void Main(string[] args)
        {
            RemoteControlWithUndo remoteControl = new RemoteControlWithUndo();
 
            Light livingRoomLight = new Light("Living Room");
 
            LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
 
            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
 
            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushed(0);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();
            remoteControl.OffButtonWasPushed(0);
            remoteControl.OnButtonWasPushed(0);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();

            CeilingFan ceilingFan = new CeilingFan("Living Room");
   
            CeilingFanMediumCommand ceilingFanMedium = new CeilingFanMediumCommand(ceilingFan);
            CeilingFanHighCommand ceilingFanHigh = new CeilingFanHighCommand(ceilingFan);
            CeilingFanOffCommand ceilingFanOff = new CeilingFanOffCommand(ceilingFan);
  
            remoteControl.SetCommand(0, ceilingFanMedium, ceilingFanOff);
            remoteControl.SetCommand(1, ceilingFanHigh, ceilingFanOff);
   
            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushed(0);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();
            remoteControl.OnButtonWasPushed(1);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();

            // Wait for user
            Console.Read();
        }    
    }

    #region Remote Control

    public class RemoteControlWithUndo 
    {
        ICommand[] onCommands;
        ICommand[] offCommands;
        ICommand undoCommand;
 
        // Constructor
        public RemoteControlWithUndo() 
        {
            onCommands = new ICommand[7];
            offCommands = new ICommand[7];
 
            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7 ;i++) 
            {
                onCommands [i] = noCommand;
                offCommands[i] = noCommand;
            }
            undoCommand = noCommand;
        }
  
        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) 
        {
            onCommands [slot] = onCommand;
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
            StringBuilder stringBuff = new StringBuilder();
            stringBuff.Append("\n------ Remote Control -------\n");
            for (int i = 0; i < onCommands.Length; i++) 
            {
                stringBuff.Append("[slot " + i + "] " + onCommands[i].GetType().Name
                    + "    " + offCommands[i].GetType().Name + "\n");
            }
            stringBuff.Append("[undo] " + undoCommand.GetType().Name + "\n");
            return stringBuff.ToString();
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

    public class DimmerLightOnCommand : ICommand 
    {
        Light light;
        int prevLevel;

        public DimmerLightOnCommand(Light light) 
        {
            this.light = light;
        }

        public void Execute() 
        {
            prevLevel = light.Level;
            light.Dim(75);
        }

        public void Undo() 
        {
            light.Dim(prevLevel);
        }
    }

    public class DimmerLightOffCommand : ICommand 
    {
        Light light;
        int prevLevel;

        public DimmerLightOffCommand(Light light) 
        {
            this.light = light;
            prevLevel = 100;
        }

        public void Execute() 
        {
            prevLevel = light.Level;
            light.Off();
        }

        public void Undo() 
        {
            light.Dim(prevLevel);
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
            switch(prevSpeed)
            {
                case CeilingFanSpeed.High:
                    ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    ceilingFan.Off();
                    break;
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
            ceilingFan.Medium();
        }
 
        public void Undo() 
        {
            switch(prevSpeed)
            {
                case CeilingFanSpeed.High:
                    ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    ceilingFan.Off();
                    break;
            }
        }
    }

    public class CeilingFanLowCommand : ICommand 
    {
        CeilingFan ceilingFan;
        CeilingFanSpeed prevSpeed;
  
        public CeilingFanLowCommand(CeilingFan ceilingFan) 
        {
            this.ceilingFan = ceilingFan;
        }
 
        public void Execute() 
        {
            prevSpeed = ceilingFan.Speed;
            ceilingFan.Low();
        }
 
        public void Undo() 
        {
            switch(prevSpeed)
            {
                case CeilingFanSpeed.High:
                    ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    ceilingFan.Off();
                    break;
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
            ceilingFan.High();
        }
 
        public void Undo() 
        {
            switch(prevSpeed)
            {
                case CeilingFanSpeed.High:
                    ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    ceilingFan.Off();
                    break;
            }
        }
    }

    #endregion

    #region Light and CeilingFan

    public class Light 
    {
        String location;
        int level;

        public Light(string location) 
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
            speed = CeilingFanSpeed.Off;
        }
  
        public void High() 
        {
            speed = CeilingFanSpeed.High;
            Console.WriteLine(location + " ceiling fan is on high");
        } 
 
        public void Medium() 
        {
            speed = CeilingFanSpeed.Medium;
            Console.WriteLine(location + " ceiling fan is on medium");
        }
 
        public void Low() 
        {
            speed = CeilingFanSpeed.Low;
            Console.WriteLine(location + " ceiling fan is on low");
        }
  
        public void Off() 
        {
            speed = CeilingFanSpeed.Off;
            Console.WriteLine(location + " ceiling fan is off");
        }
  
        public CeilingFanSpeed Speed
        {
            get{ return speed; }
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
