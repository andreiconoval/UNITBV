using System;
using System.Timers;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// The Model in the MVC pattern
    /// </summary>
    public class Model
    {
        private int beatsPerMinute = 120;
        private int interval = 500;
        private Timer timer;

        public delegate void BeatHandler<T>(T sender, BeatEventArgs e);
        public event BeatHandler<Model> Beat;

        public Model()
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void OnTimedEvent(object o, ElapsedEventArgs e)
        {
            Beeper.Beep(2000,10);
            
            // Let the observers know about this beat
            OnBeat(new BeatEventArgs(beatsPerMinute));
        }

        public int BeatsPerMinute
        {
            set
            { 
                beatsPerMinute = value; 

                // Interval is in milliseconds
                interval = (int)((60D / ((double)beatsPerMinute )) * 1000D);
                timer.Interval = interval;

                // Update display of observers
                OnBeat(new BeatEventArgs(beatsPerMinute));
            }
            get
            { 
                return beatsPerMinute; 
            }
        }

        public void Attach(IBeatable beatable)
        {
            Beat += new BeatHandler<Model>(beatable.Beat);
        }

        public void Detach(IBeatable beatable)
        {
            Beat -= new BeatHandler<Model>(beatable.Beat);
        }

        // Invoke the Beat event
        public virtual void OnBeat(BeatEventArgs e) 
        {
            if (Beat != null)
            {
                Beat(this, e);
            }
        }
    }
}
