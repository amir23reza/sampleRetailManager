using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels
{
    public class ShellViewModel
    {
        /**
         * Below is a sample demonstration of dependancy injection where we are using Calculations class as a dependancy which is injected through a DIC
         */
        public ShellViewModel(ICalculations calculations) {
            calculations.increment();
        }
    }
}
