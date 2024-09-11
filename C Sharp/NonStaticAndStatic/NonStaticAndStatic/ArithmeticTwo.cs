using System;

namespace NonStaticAndStatic
{
    public static class ArithmeticTwo
    {
        public static void Add(int a, int b)
        {
            Console.WriteLine("\n4.Calling Static method from Static of Other class");
            Console.WriteLine(a + " + " + b + " = " + (a + b));
            //Calling Static method from Static of Same class
            Subtract(5, 2);
            
        }
        public static void Subtract(int a, int b)
        {
            Console.WriteLine("\n5.Calling Static method from Static of Same class");
            Console.WriteLine(a + " - " + b + " = " + (a - b));

            //Calling Static method from Non-Static of Other class
            Arithmetic.Multiply(15,6);

            //Calling Non-Static method from Static of Other class
            Arithmetic objectThree = new();
            objectThree.DivideNumbers(55, 5);
        }
    }
}
