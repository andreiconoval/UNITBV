using System;

namespace DoFactory.HeadFirst.Facade.HomeTheater
{
    class HomeTheaterTestDrive
    {
        static void Main(string[] args)
        {
            Amplifier amp = new Amplifier("Top-O-Line Amplifier");
            Tuner tuner   = new Tuner("Top-O-Line AM/FM Tuner", amp);
            DvdPlayer dvd = new DvdPlayer("Top-O-Line DVD Player", amp);
            CdPlayer cd   = new CdPlayer("Top-O-Line CD Player", amp);

            Projector projector  = new Projector("Top-O-Line Projector", dvd);
            TheaterLights lights = new TheaterLights("Theater Ceiling Lights");
            Screen screen        = new Screen("Theater Screen");
            PopcornPopper popper = new PopcornPopper("Popcorn Popper");
 
            HomeTheaterFacade homeTheater = 
                new HomeTheaterFacade(amp, tuner, dvd, cd, 
                projector, screen, lights, popper);
 
            homeTheater.WatchMovie("Raiders of the Lost Ark");
            homeTheater.EndMovie();        

            // Wait for user
            Console.Read();
        }
    }

    #region Facade

    public class HomeTheaterFacade 
    {
        private Amplifier amp;
        private Tuner tuner;
        private DvdPlayer dvd;
        private CdPlayer cd;
        private Projector projector;
        private TheaterLights lights;
        private Screen screen;
        private PopcornPopper popper;
 
        public HomeTheaterFacade(Amplifier amp, 
            Tuner tuner, 
            DvdPlayer dvd, 
            CdPlayer cd, 
            Projector projector, 
            Screen screen,
            TheaterLights lights,
            PopcornPopper popper) 
        {
 
            this.amp = amp;
            this.tuner = tuner;
            this.dvd = dvd;
            this.cd = cd;
            this.projector = projector;
            this.screen = screen;
            this.lights = lights;
            this.popper = popper;
        }
 
        public void WatchMovie(string movie) 
        {
            Console.WriteLine("Get ready to watch a movie...");
            popper.On();
            popper.Pop();
            lights.Dim(10);
            screen.Down();
            projector.On();
            projector.WideScreenMode();
            amp.On();
            amp.SetDvd(dvd);
            amp.SetSurroundSound();
            amp.SetVolume(5);
            dvd.On();
            dvd.Play(movie);
        }
 
        public void EndMovie() 
        {
            Console.WriteLine("\nShutting movie theater down...");
            popper.Off();
            lights.On();
            screen.Up();
            projector.Off();
            amp.Off();
            dvd.Stop();
            dvd.Eject();
            dvd.Off();
        }

        public void ListenToCd(string cdTitle) 
        {
            Console.WriteLine("Get ready for an audiopile experence...");
            lights.On();
            amp.On();
            amp.SetVolume(5);
            amp.SetCd(cd);
            amp.SetStereoSound();
            cd.On();
            cd.Play(cdTitle);
        }

        public void EndCd() 
        {
            Console.WriteLine("Shutting down CD...");
            amp.Off();
            amp.SetCd(cd);
            cd.Eject();
            cd.Off();
        }

        public void ListenToRadio(double frequency) 
        {
            Console.WriteLine("Tuning in the airwaves...");
            tuner.On();
            tuner.SetFrequency(frequency);
            amp.On();
            amp.SetVolume(5);
            amp.SetTuner(tuner);
        }

        public void EndRadio() 
        {
            Console.WriteLine("Shutting down the tuner...");
            tuner.Off();
            amp.Off();
        }
    }

    #endregion

    #region Subsystem Components

    public class Tuner 
    {
        string description;
        Amplifier amplifier;
        double frequency;
    
        public Tuner(string description, Amplifier amplifier) 
        {
            this.description = description;
            this.amplifier = amplifier;
        }
 
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
 
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }
 
        public void SetFrequency(double frequency) 
        {
            Console.WriteLine(description + " setting frequency to " + frequency);
            this.frequency = frequency;
        }
 
        public void SetAm() 
        {
            Console.WriteLine(description + " setting AM mode");
        }
 
        public void SetFm() 
        {
            Console.WriteLine(description + " setting FM mode");
        }
  
        public override string ToString() 
        {
            return description;
        }
    }

    public class Amplifier 
    {
        string description;
        Tuner tuner;
        DvdPlayer dvd;
        CdPlayer cd;
    
        public Amplifier(string description) 
        {
            this.description = description;
        }
 
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
 
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }
 
        public void SetStereoSound() 
        {
            Console.WriteLine(description + " stereo mode on");
        }
 
        public void SetSurroundSound() 
        {
            Console.WriteLine(description + " surround sound on (5 speakers, 1 subwoofer)");
        }
 
        public void SetVolume(int level) 
        {
            Console.WriteLine(description + " setting volume to " + level);
        }

        public void SetTuner(Tuner tuner) 
        {
            Console.WriteLine(description + " setting tuner to " + dvd);
            this.tuner = tuner;
        }
  
        public void SetDvd(DvdPlayer dvd) 
        {
            Console.WriteLine(description + " setting DVD player to " + dvd);
            this.dvd = dvd;
        }
 
        public void SetCd(CdPlayer cd) 
        {
            Console.WriteLine(description + " setting CD player to " + cd);
            this.cd = cd;
        }
 
        public override string ToString() 
        {
            return description;
        }
    }
    public class TheaterLights 
    {
        string description;
    
        public TheaterLights(string description) 
        {
            this.description = description;
        }
  
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
  
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }
  
        public void Dim(int level) 
        {
            Console.WriteLine(description + " dimming to " + level  + "%");
        }
   
        public override string ToString() 
        {
            return description;
        }
    }
    public class Screen 
    {
        string description;
    
        public Screen(string description) 
        {
            this.description = description;
        }
 
        public void Up() 
        {
            Console.WriteLine(description + " going up");
        }
 
        public void Down() 
        {
            Console.WriteLine(description + " going down");
        }
 
  
        public override string ToString() 
        {
            return description;
        }
    }

    public class Projector 
    {
        string description;
        DvdPlayer dvdPlayer;
    
        public Projector(string description, DvdPlayer dvdPlayer) 
        {
            this.description = description;
            this.dvdPlayer = dvdPlayer;
        }
 
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
 
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }

        public void WideScreenMode() 
        {
            Console.WriteLine(description + " in widescreen mode (16x9 aspect ratio)");
        }

        public void TvMode() 
        {
            Console.WriteLine(description + " in tv mode (4x3 aspect ratio)");
        }
  
        public override string ToString() 
        {
            return description;
        }
    }

    public class PopcornPopper 
    {
        string description;
    
        public PopcornPopper(string description) 
        {
            this.description = description;
        }
 
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
 
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }

        public void Pop() 
        {
            Console.WriteLine(description + " popping popcorn!");
        }
        public override string ToString() 
        {
            return description;
        }
    }
    public class DvdPlayer 
    {
        string description;
        int currentTrack;
        Amplifier amplifier;
        string movie;
    
        public DvdPlayer(string description, Amplifier amplifier) 
        {
            this.description = description;
            this.amplifier = amplifier;
        }
 
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
 
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }

        public void Eject() 
        {
            movie = null;
            Console.WriteLine(description + " eject");
        }
 
        public void Play(string movie) 
        {
            this.movie = movie;
            currentTrack = 0;
            Console.WriteLine(description + " playing \"" + movie + "\"");
        }

        public void Play(int track) 
        {
            if (movie == null) 
            {
                Console.WriteLine(description + " can't play track " + track + " no dvd inserted");
            } 
            else 
            {
                currentTrack = track;
                Console.WriteLine(description + " playing track " + currentTrack + " of \"" + movie + "\"");
            }
        }

        public void Stop() 
        {
            currentTrack = 0;
            Console.WriteLine(description + " stopped \"" + movie + "\"");
        }
 
        public void Pause() 
        {
            Console.WriteLine(description + " paused \"" + movie + "\"");
        }

        public void SetTwoChannelAudio() 
        {
            Console.WriteLine(description + " set two channel audio");
        }
 
        public void SetSurroundAudio() 
        {
            Console.WriteLine(description + " set surround audio");
        }
 
        public override string ToString() 
        {
            return description;
        }
    }

    public class CdPlayer 
    {
        string description;
        int currentTrack;
        Amplifier amplifier;
        string title;
    
        public CdPlayer(string description, Amplifier amplifier) 
        {
            this.description = description;
            this.amplifier = amplifier;
        }
 
        public void On() 
        {
            Console.WriteLine(description + " on");
        }
 
        public void Off() 
        {
            Console.WriteLine(description + " off");
        }

        public void Eject() 
        {
            title = null;
            Console.WriteLine(description + " eject");
        }
 
        public void Play(string title) 
        {
            this.title = title;
            currentTrack = 0;
            Console.WriteLine(description + " playing \"" + title + "\"");
        }

        public void Play(int track) 
        {
            if (title == null) 
            {
                Console.WriteLine(description + " can't play track " + currentTrack + 
                    ", no cd inserted");
            } 
            else 
            {
                currentTrack = track;
                Console.WriteLine(description + " playing track " + currentTrack);
            }
        }

        public void Stop() 
        {
            currentTrack = 0;
            Console.WriteLine(description + " stopped");
        }
 
        public void Pause() 
        {
            Console.WriteLine(description + " paused \"" + title + "\"");
        }
 
        public override string ToString() 
        {
            return description;
        }
    }
    #endregion
}
