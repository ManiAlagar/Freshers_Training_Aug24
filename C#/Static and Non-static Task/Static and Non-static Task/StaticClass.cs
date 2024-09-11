using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static_and_Non_static_Task
{
    class StaticClass
    {
        public void NonstaticFromOtherClass()
        {
            Console.WriteLine("methodFour");
        }

        public static void Method()
        {
            Console.WriteLine("methodFive");
        }
    }
}