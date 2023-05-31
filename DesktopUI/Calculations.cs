using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI
{
    /**
     * The initial purpose of this class is to demonstrate the dependancy injection functionality under DIC of Caliburn Simple Container.
     * It is simple class with a constructor, a property, and a method which increments the property and writes it in the console.
     * It is going to be registered in the config method of simple container and be used (injected) in the shell view model.
     */
    public class Calculations : ICalculations
    {
        private int _count;
        public Calculations()
        {
            this._count = 0;
        }

        public void increment()
        {
            this._count += 1;
            Console.WriteLine(this._count.ToString());
        }
    }
}
