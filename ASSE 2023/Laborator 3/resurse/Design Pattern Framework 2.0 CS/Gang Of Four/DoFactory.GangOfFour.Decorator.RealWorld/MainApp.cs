using System;
using System.Collections;

namespace DoFactory.GangOfFour.Decorator.RealWorld
{
    /// <summary>
    /// MainApp startup class for Real-World 
    /// Decorator Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create book
            Book book = new Book ("Worley", "Inside ASP.NET", 10);
            book.Display();

            // Create video
            Video video = new Video ("Spielberg", "Jaws", 23, 92);
            video.Display();

            // Make video borrowable, then borrow and display
            Console.WriteLine("\nMaking video borrowable:");

            Borrowable borrowvideo = new Borrowable(video);
            borrowvideo.BorrowItem("Customer #1");
            borrowvideo.BorrowItem("Customer #2");

            borrowvideo.Display();

            // Wait for user
            Console.Read();
        }
    }

    // "Component" 

    abstract class LibraryItem
    {
        private int numCopies;

        // Property
        public int NumCopies
        {
            get{ return numCopies; }
            set{ numCopies = value; }
        }

        public abstract void Display();
    }

    // "ConcreteComponent" 

    class Book : LibraryItem
    {
        private string author;
        private string title;

        // Constructor
        public Book(string author,string title,int numCopies)
        {
            this.author = author;
            this.title = title;
            this.NumCopies = numCopies;
        }

        public override void Display()
        {
            Console.WriteLine("\nBook ------ ");
            Console.WriteLine(" Author: {0}", author);
            Console.WriteLine(" Title: {0}", title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
        }
    }

    // "ConcreteComponent"

    class Video : LibraryItem
    {
        private string director;
        private string title;
        private int playTime;

        // Constructor
        public Video(string director, string title, 
            int numCopies, int playTime)
        {
            this.director = director;
            this.title = title;
            this.NumCopies = numCopies;
            this.playTime = playTime;
        }

        public override void Display()
        {
            Console.WriteLine("\nVideo ----- ");
            Console.WriteLine(" Director: {0}", director);
            Console.WriteLine(" Title: {0}", title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
            Console.WriteLine(" Playtime: {0}\n", playTime);
        }
    }

    // "Decorator"

    abstract class Decorator : LibraryItem
    {
        protected LibraryItem libraryItem;

        // Constructor
        public Decorator(LibraryItem libraryItem)
        {
            this.libraryItem = libraryItem;
        }

        public override void Display()
        {
            libraryItem.Display();
        }
    }

    // "ConcreteDecorator"

    class Borrowable : Decorator
    {
        protected ArrayList borrowers = new ArrayList();

        // Constructor
        public Borrowable(LibraryItem libraryItem) 
            : base(libraryItem) 
        {
        }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumCopies--;
        }

        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumCopies++;
        }

        public override void Display()
        {
            base.Display();
            
            foreach (string borrower in borrowers)
            {
                Console.WriteLine(" borrower: " + borrower);
            }
        }
    }
}
