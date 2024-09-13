using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev
{
     class ExceptionHandling
    {
         public void IndexOutOfRangeException()
        {
            try
            {
                int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Console.WriteLine(ints[30]);
            } catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("\n IndexOutOfRangeException: "+e.Message);//Index was outside the bounds of the array.
            }
        }

        public void ArithmeticOverflowException()
        {
            try
            {
                checked
                {
                    int x =999;
                    int y = 7897678;
                    int multiply = x * y;
                    Console.WriteLine(multiply);
                }

            }
            catch (OverflowException e)
            {
                Console.WriteLine("\n ArithmeticOverflowException: "+e.Message);//Arithmetic operation resulted in an overflow.
            }
        }
        public void NullReferenceException()
        {
            try
            {
                string name = null;
                string value = name.ToLower();
                Console.WriteLine(value);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("\n NullReferenceException: "+e.Message);//Object reference not set to an instance of an object.
            }
        }
    }

}
