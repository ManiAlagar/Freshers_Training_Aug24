using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppDev
{
    class Program
    {
        static void Main(string[] args)
        {
            int myInt = 24;
            long myLong = 5000000L;
            string myString = "harini";
            byte[] bytes = Encoding.UTF8.GetBytes(myString);
            string encoded = Convert.ToBase64String(bytes);
            Console.WriteLine("Int to double: "+Convert.ToDouble(myInt));
            Console.WriteLine("Long to string: "+Convert.ToString(myLong));
            Console.WriteLine("Original string: " + myString + ", Base64 encoded: " + encoded);

            Console.WriteLine("\n String length: "+myString.Length);
            Console.WriteLine("String uppercase: "+myString.ToUpper());
            Console.WriteLine("String lowercase: " + myString.ToLower());

            Value value = new Value(1, 2);
            Console.WriteLine("\n accessing value through struct:" + value.X);

            Console.WriteLine("\n For Loop");
            ForLoop();
            Console.WriteLine("\n While Loop");
            WhileLoop();
            Console.WriteLine("\n Foreach Loop");
            ForEachLoop();
            Console.WriteLine("\n Do while Loop");
            DoWhileLoop();
            //math();

            ExceptionHandling exceptionHandling = new ExceptionHandling();
            exceptionHandling.IndexOutOfRangeException();  //Index was outside the bounds of the array.

            ExceptionHandling ArithmeticOverflow = new ExceptionHandling();
            ArithmeticOverflow.ArithmeticOverflowException();  //Arithmetic operation resulted in an overflow.

            ExceptionHandling NullReference = new ExceptionHandling();
            NullReference.NullReferenceException();  //Object reference not set to an instance of an object.

            Matrix matrix = new Matrix();
            matrix.MatrixCal();
        }

        static void ForLoop()
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void WhileLoop()
        {
            int i = 6;
            while (i <= 10) 
            { 
                Console.WriteLine(i);
                i++;
            }
        }

        static void DoWhileLoop()
        {
            int i = 11;
            do
            {
                Console.WriteLine(i);
                i++;
            } while (i <= 15);
        }

        static void ForEachLoop()
        {
            string[] things = { "pen", "pencil" };
            foreach (string thing in things)
            {
                Console.WriteLine(thing);
            }
           
        }
        static void Math()
        {
            int x, y, mod;
            Console.Write("Enter a number: ");
            x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter another number: ");
            y = Convert.ToInt32(Console.ReadLine());
            mod = x % y;
            Console.WriteLine("Output for x/y is: " + mod);
        }

        public struct Value
        {
            public int X;
            public int Y;

            public Value(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

    }
    
}
