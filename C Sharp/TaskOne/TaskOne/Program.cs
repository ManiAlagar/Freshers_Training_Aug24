using CsharpStruct;
using TaskOne;
public class Program
{
    public void demo()
    {
        Console.WriteLine("HI ");
    }
    public static void Main(string[] arg)
    {

        #region invoke constructor of struct
        Console.WriteLine("-------Struct Implement--------");
        
        Employee emp = new Employee(1, "Brian");

        Console.WriteLine("\nEmployee Name: " + emp.name);
        Console.WriteLine("Employee Id: " + emp.id);
       #endregion

        StringOperation stringObject = new();
        string Value = "Hi Hello";

        //Console.WriteLine("\nString Manipulation:");
        Console.WriteLine("\n\n------String Manipulation-------");

        #region String Manipulation 
        int StringLength = stringObject.StringLength(Value);
        Console.WriteLine($"\nLength of the {Value} : {StringLength} ");

        Console.Write("\nString Traverse : ");
        Console.WriteLine(stringObject.TraverseString(Value));

        Console.Write("\nString Reverse : ");
        Console.WriteLine(stringObject.ReverseString(Value));
        #endregion

        //MatrixOperation Class:
        Console.Write("\n\n-------Matrix Operations---------");

        MatrixOperation MatrixObject = new();
        int[,] matrix = MatrixObject.MatrixGeneration();

        Console.WriteLine("Opertions in Matrix:"); 
        Console.WriteLine("\n1.Display the Matrix: \n2.Get element using index \n3.Add and modify the exisiting element in a index:");
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

            //Error code here to check exception handling...
            int RandomNUmber = 10;
            Console.WriteLine(RandomNUmber / 0);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("\nAccessing invalid indices in arrays");
            return;
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine("\n" + e.Message);
        }

        Console.WriteLine("\nExcecuting message after the handling the Message");
    }    
}
