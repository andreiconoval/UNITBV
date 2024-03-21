using System;
using System.Runtime.InteropServices;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// Beeper implements 'missing' C# Beep function
    /// </summary>
    public class Beeper
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq,int duration);
    }
}
