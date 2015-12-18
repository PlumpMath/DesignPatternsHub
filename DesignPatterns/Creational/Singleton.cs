using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    /*
        Ensure a class has only one instance and provide a global point of access to it.
    */

    public class Singleton
    {
        private static Singleton _instance;

        // Constructor is 'protected'
        private Singleton()
        {
        }

        public static Singleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new Singleton();
            }

            return _instance;
        }
    }
}
