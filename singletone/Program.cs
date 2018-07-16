using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace singletone
{
    class Program
    {
        //thread save realisation
        class Singleton
        {
            private static Singleton instance;
            private static object locker = new object();

            private Singleton() { }

            public static Singleton GetSingleton()
            {
                if (instance==null)
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new Singleton();                 
                    }
                return instance;
            }
        }


        //lazy realisation


        static void Main(string[] args)
        {
            Singleton theOne = Singleton.GetSingleton();
            Console.WriteLine(theOne.GetHashCode());
            Singleton secondOne = Singleton.GetSingleton();
            Console.WriteLine(secondOne.GetHashCode());
    
        }
    }
}
