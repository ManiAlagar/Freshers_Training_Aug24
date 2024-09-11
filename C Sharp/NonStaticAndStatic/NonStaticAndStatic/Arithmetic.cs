using System;
using System.Dynamic;

namespace NonStaticAndStatic
{
    internal class Arithmetic
    {
       public void AddNumbers(int a,int b)
       {
            Console.WriteLine("\n2.Calling Non-Static method from Non-Static of Other class");
            Console.WriteLine( a + " + " + b + " = "+ (a + b) );
            //Calling Non-Static method from Non-Static of Same class
            SubtractNumbers(22, 2);
       }
        public void SubtractNumbers(int a,int b)
        {
            Console.WriteLine ("\n3.Calling Non-Static method from Non-Static of Same class");
            Console.WriteLine(a + " - " + b + " = " + (a - b));            
        }
        public static void Multiply(int a,int b)
        {
            Console.WriteLine("\n6.Calling Static method from Non-Static of Other class");
            Console.WriteLine(a + " * " + b + " = " + (a * b));
           
        }
        public void DivideNumbers(int a,int b)
        {
            Console.WriteLine("\n7.Calling Non-Static method from Static of Other class");
            Console.WriteLine(a + " / " + b + " = " + (a / b));
            //Calling Static method from Non-Static of Same class
            Arithmetic.Modulo(155 , 2);

        }
        public static void Modulo(int a, int b)
        {
            Console.WriteLine("\n8.Calling Static method from Non-Static of Same class");
            Console.WriteLine(a + " % " + b + " = " + (a % b));
        }
    } 
}
