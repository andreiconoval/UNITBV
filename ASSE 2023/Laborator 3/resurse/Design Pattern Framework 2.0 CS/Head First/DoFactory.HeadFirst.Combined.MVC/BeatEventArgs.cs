using System;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// BeatEventArgs carry info about BPM (Beats Per Minute).
    /// </summary>
    public class BeatEventArgs
    {
        private int beatsPerMinute;

        public BeatEventArgs(int beats) : base()
        {
            this.beatsPerMinute = beats;
        }

        public int BeatsPerMinute
        {
            get{ return beatsPerMinute; }
        }
    }
}
