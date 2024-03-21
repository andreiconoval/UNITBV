using System;

// Replacement for the Java 'Swing example'
namespace DoFactory.HeadFirst.Observer.DotNet
{
    class DotNetObserverExample
    {
        static void Main()
        {
            // Create listeners
            ActionListener angel = new ActionListener("Angel");
            ActionListener devil = new ActionListener("Devil");

            // Create Button and attach listeners
            Button button = new Button("Click Me");
            button.Attach(angel);
            button.Attach(devil);

            // Simulate clicks on button
            button.Push(1,3);
            button.Push(5,4);
            button.Push(8,5);

            // Wait for user
            Console.Read();
        }
    }

    #region EventHandler and EventArgs

    // Delegate type for hooking up click requests
    public delegate void ClickEventHandler
                            (object sender, ClickEventArgs e);

    // Custom event arguments
    public class ClickEventArgs : EventArgs 
    {
        private int x;
        private int y;

        // Constructor
        public ClickEventArgs(int x, int y) 
        {
            this.x = x; 
            this.y = y;
        }

        // Properties
        public int X
        {
            get{ return x; }
            set{ x = value; }
        }

        public int Y
        {
            get{ return y; }
            set{ y = value; }
        }
    }  

    #endregion

    #region Controls

    // Base class for UI controls

    abstract class Control
    {
        protected string text;

        // Constructor
        public Control(string text)
        {
            this.text = text;
        }

        // Event
        public event ClickEventHandler Click;
        
        // Invoke the Click  event
        public virtual void OnClick(ClickEventArgs e) 
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        public void Attach(ActionListener listener)
        {
            Click += new ClickEventHandler(listener.Update);
        }

        public void Detach(ActionListener listener)
        {
            Click -= new ClickEventHandler(listener.Update);
        }

        // Use this method to simulate push (click) events
        public void Push(int x, int y)
        {
            OnClick(new ClickEventArgs(x,y));
            Console.WriteLine("");
        }
    }

    // Button control

    class Button : Control
    {
        // Constructor
        public Button(string text) : base(text)
        {
        }
    }
    #endregion

    #region ActionListener

    interface IActionListener
    {
        void Update(object sender, ClickEventArgs e);
    }

    class ActionListener : IActionListener
    {
        private string name;

        // Constructor
        public ActionListener(string name)
        {
            this.name = name;
        }

        public void Update(object sender, ClickEventArgs e)
        {
            Console.WriteLine("Notified {0} of click at ({1},{2})", 
                name, e.X, e.Y);
        }
    }
    #endregion
}
