using System;
using System.ComponentModel.DataAnnotations;
using TaskOne;
public class Program
{
    public static void Main(string[] arg)
    {

        StringOperation stringObject = new();
        string Value = "Hello";
        
        int StringLength = stringObject.StringLength(Value);
        Console.WriteLine($"Length of the {Value} : " + StringLength);

        Console.Write("\nString Traverse : ");
        Console.WriteLine(stringObject.TraverseString(Value));

        Console.Write("\nString Reverse : ");
        Console.WriteLine(stringObject.ReverseString(Value));

        //MatrixOperation Class:
        MatrixOperation MatrixObject = new();
        int[,] matrix = MatrixObject.MatrixGeneration();

        Console.WriteLine("\nOpertions in Matrix:"); 
        Console.WriteLine("1.Display the Matrix: \n2.Get element using index \n3.Add and modify the exisiting element in a index:");
        try
        {
            
            bool flag = false;
            do
            {
                Console.WriteLine("\nEnter Your Choice:");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {             
                            Console.WriteLine("\nYour choice ----> \"Display the Matrix:\" ");
                            MatrixObject.Display(matrix);
                            break;
                        }

                    case 2:
                        {
                            Console.WriteLine("\nYour choice ----> Get element using index:");

                            Console.WriteLine("\nEnter the row index");
                            int rowIndex = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the column index");
                            int columnIndex = Convert.ToInt32(Console.ReadLine());

                            Console.Write($"The element in the index [{rowIndex}][{columnIndex}] : ");
                            Console.WriteLine("\"" + MatrixObject.GetMatrixElement(rowIndex, columnIndex, matrix) + "\" ");
                            flag = false;
                            break;
                        }

                    case 3:
                        {
                            Console.WriteLine("\nYour choice ----> \"Add and modify the exisiting element in a index:\" ");
                            Console.WriteLine("Enter the row index");
                            int rowIndex = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the column index");
                            int columnIndex = Convert.ToInt32(Console.ReadLine());
                            MatrixObject.ModifyMatrixElement(rowIndex, columnIndex, matrix);
                            flag = false;
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("\nInvalid Choice");
                            Console.WriteLine("Available choice 1 or 2");
                            flag = true;
                            break;
                        }
                }
            } while (flag);
            int RandomNUmber = 10;
            Console.WriteLine(RandomNUmber / 0);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("\nAccessing invalid indices in arrays");
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine("\n"+e.Message);
        }
    }    
}
