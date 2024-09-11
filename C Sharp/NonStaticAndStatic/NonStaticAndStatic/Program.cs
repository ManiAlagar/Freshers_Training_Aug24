using System;
using System.Dynamic;
using NonStaticAndStatic;
public class Program 
{
    void display()
    {    
        Console.WriteLine("\n1.Calling Non-Static method from Static of Same class");
        //Calling Non-Static method from Non-Static of other class
        Arithmetic ObjectTwo = new();
        ObjectTwo.AddNumbers(8, 12);
    }
    public  static void Main(string[] args)
    {
        Console.WriteLine("Welcome to C sharp...");
        //Calling Non-Static method from Static of Same class
        Program objectOne = new();
        objectOne.display();
        //Calling Static method from Static of other class
        ArithmeticTwo.Add(5, 55);
    }
}
