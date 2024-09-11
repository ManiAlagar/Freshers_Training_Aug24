using System;

namespace Static_and_Non_static_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("static within static of same class");
            StaticMethod();


            Console.WriteLine("Non static within static of same class");
            Program obj = new Program();
            obj.NonStaticMethod();


            Console.WriteLine("Non static within static of other class");
            StaticClass myObj = new StaticClass();
            myObj.NonstaticFromOtherClass();
        }
        static void StaticMethod()
        {
            Console.WriteLine("method one");

            Console.WriteLine("static within static of other class");
              StaticClass.Method();
        }
        void NonStaticMethod()
        {
            Console.WriteLine("method two");


            Console.WriteLine("Non static within non static of same class");
            NonStaticMethods();


            Console.WriteLine("static within non static of same class");
            StaticMethod();
        }
         void NonStaticMethods()
        {
            Console.WriteLine("method three");


            Console.WriteLine("Non static within non static of other class");
            StaticClass myObject = new StaticClass();
            myObject.NonstaticFromOtherClass();


            Console.WriteLine("static within non static of other class");
            StaticClass.Method();
        }
    }

}
