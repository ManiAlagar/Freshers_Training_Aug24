using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev
{
     class Matrix
    {
        public int[,] MatrixCal()
        {
           
                int userInput;
                Console.Write("\n Enter a number: ");
                userInput = Convert.ToInt32(Console.ReadLine());

                int[,] numbers = new int[userInput, userInput];
                Random random = new Random();

                for (int m = 0; m < userInput; m++)
                {
                    for (int n = 0; n < userInput; n++)
                    {
                        numbers[m, n] = random.Next(10);
                        Console.Write(numbers[m, n] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Options \n 1.Get element in the Matrix \n 2.Manipulate the existing element");
            try
            {
                bool isImplement = true;
                do
                {
                    Console.Write("\n Enter your option here => ");
                    int option = Convert.ToInt32(Console.ReadLine());


                    switch (option)
                    {
                        case 1:
                            Console.Write("\n Enter an Row index: ");
                            int getRowIndex = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\n Enter an Column index: ");
                            int getColumnIndex = Convert.ToInt32(Console.ReadLine());
                            int matrixElement = numbers[getRowIndex, getColumnIndex];
                            Console.WriteLine("\n Fetched Matrix element :" + matrixElement);
                            isImplement = false;
                            break;
                        case 2:
                            Console.Write("\n Enter an Row index: ");
                            int rowIndex = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\n Enter an Column index: ");
                            int columnIndex = Convert.ToInt32(Console.ReadLine());
                            int matrixNumber = numbers[rowIndex, columnIndex];
                            Console.WriteLine(matrixNumber);
                            Console.Write("\n Enter the Value to Add: ");
                            int userValue = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\n Existing Value: " + numbers[rowIndex, columnIndex]);
                            numbers[rowIndex, columnIndex] = matrixNumber + userValue;
                            Console.WriteLine("\n Updated Value: " + numbers[rowIndex, columnIndex]);
                            Console.WriteLine("\n Printing Matrix: ");
                            Display(numbers);
                            Console.ReadLine();
                            isImplement = false;
                            break;
                        default:
                            Console.WriteLine("\n Please enter a valid option");
                            isImplement = true;
                            break;

                    }
                } while (isImplement);

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("\n IndexOutOfRangeException: " + e.Message);//Index was outside the bounds of the array.
            }
            catch (OverflowException e)
            {
                Console.WriteLine("\n ArithmeticOverflowException: " + e.Message);//Arithmetic operation resulted in an overflow.
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Exception: " + e.Message);
            }
            return numbers;
            
            
        }
       
        static void Display(int[,] numbers)
        {
            for (int k = 0; k < numbers.GetLength(0); k++)
            { 
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write(numbers[k, j] + "\t");
                }
                Console.WriteLine();
            }
        }
       

    }
}
