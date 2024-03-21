using System;
using System.Windows.Forms;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// The Controller class in the MVC pattern
    /// </summary>
    public class Controller
    {
        private FormMain view;
        private Model model;

        // Constructor
        public Controller( FormMain view )
        {
            this.view = view;
            this.model = new Model();
        }

        public void Start()
        {
            model.Start();
        }

        public void Stop()
        {
            model.Stop();

            // Disable stop button on View
            view.ButtonStop.Enabled = false;
        }

        public int BeatsPerMinute
        {
            set
            { 
                model.BeatsPerMinute = value; 

                // Enable stop button on View
                view.ButtonStop.Enabled = true;
            }
            get
            { 
                return model.BeatsPerMinute; 
            }
        }

        public void Attach(IBeatable beatable)
        {
            model.Attach(beatable);
        }

        public void Detach(IBeatable beatable)
        {
            model.Detach(beatable);
        }
    }
}
