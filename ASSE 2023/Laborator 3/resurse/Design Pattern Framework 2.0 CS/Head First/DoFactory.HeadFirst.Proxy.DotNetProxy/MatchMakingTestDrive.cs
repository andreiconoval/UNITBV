using System;
using System.Reflection;


namespace DoFactory.HeadFirst.Proxy.DotNetProxy
{
    class MatchMakingTestDrive
    {
        static void Main(string[] args)
        {
            Person joe = new Person("Joe Javabean");

            // ---- Use the Owner Invocation Handler -----

            ClassGenerator generator = new ClassGenerator( typeof(IPerson), 
                new OwnerInvocationHandler(joe));
            IPerson personProxy = (IPerson)generator.Generate(); 

            Console.WriteLine("Name is " + personProxy.Name);

            personProxy.Interest = "bowling, Go";
            Console.WriteLine("Interests set from owner proxy");

            personProxy.HotOrNot = 7;

            int hotOrNot = personProxy.HotOrNot;

            Console.WriteLine("Rating is " + hotOrNot + "\n");
        
            // ---- Use the Non Owner Invocation Handler ----

            generator = new ClassGenerator( typeof(IPerson), 
                new NonOwnerInvocationHandler(joe));
            personProxy = (IPerson)generator.Generate(); 

            Console.WriteLine("Name is " + personProxy.Name);

            personProxy.Interest = "bowling, Go";

            personProxy.HotOrNot = 5;
            Console.WriteLine("Rating set from non-owner proxy");

            hotOrNot = personProxy.HotOrNot;
            Console.WriteLine("Rating is " + hotOrNot + "\n");
        
            // Wait for user
            Console.Read();
        }
    }

    #region Person

    public interface IPerson
    {
        string Name{get; set;}
        string Gender{get; set;}
        string Interest{get; set;}
        int HotOrNot{get; set;}
    }

    public class Person : IPerson
    {
        private string name;
        private string gender;
        private string interest;
        private int rating;
        private int ratingCount = 0;

        public Person(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get{ return name; }
            set{name = value; }
        }

        public string Gender
        {
            get{ return gender;    }
            set{ gender = value; }
        }

        public string Interest
        {
            get{ return interest;    }
            set{ interest = value; }
        }

        public int HotOrNot
        {
            get
            { 
                return (ratingCount == 0)    ? 0 : rating / ratingCount;
            }
            set
            {
                rating += value;
                ratingCount++;
            }
        }
    }

    #endregion

    #region Owner InvocationHandler

    public class OwnerInvocationHandler : IInvocationHandler 
    {
        private Person p;
        public OwnerInvocationHandler(Person p)
        {
            this.p = p;
        }
        public object Invoke(string methodName, object[] args) 
        {
            if (args.Length > 0)
            {
                if (methodName == "HotOrNot")
                {
                    Console.WriteLine("Can't set rating from owner proxy");
                    return null;
                }
                return p.GetType().InvokeMember (methodName, BindingFlags.SetProperty, null, p, args);
            }
            else 
            {
                return p.GetType().InvokeMember (methodName, BindingFlags.GetProperty, null, p, args);
            }
        }
    }

    #endregion

    #region NonOwner InvocationHandler

    public class NonOwnerInvocationHandler : IInvocationHandler 
    {
        private Person p;
        public NonOwnerInvocationHandler(Person p)
        {
            this.p = p;
        }
        public object Invoke(string methodName, object[] args) 
        {
            if (args.Length > 0)
            {
                if (methodName == "Interest")
                {
                    Console.WriteLine("Can't set interests from non owner proxy");
                    return null;
                }
                return p.GetType().InvokeMember (methodName, BindingFlags.SetProperty, null, p, args);
            }
            else 
            {
                return p.GetType().InvokeMember (methodName, BindingFlags.GetProperty, null, p, args);
            }
        }
    }
    #endregion
}