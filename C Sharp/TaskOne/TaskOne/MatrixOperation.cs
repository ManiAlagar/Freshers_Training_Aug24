namespace TaskOne
{
    public class MatrixOperation
    {
        public void Display(int[,] matrix)
        {
           
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }
        public int[,] MatrixGeneration()
        {
            Console.WriteLine("\n\nEnter the Dimension of the Matrix: ");
            int x = Convert.ToInt32(Console.ReadLine());

            //Array declaration
            int[,] matrix = new int[x,x];

            Random randomNumber = new();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {                    
                    matrix[i, j] = randomNumber.Next(10,20); // Store user input in the 2D array
                }
            }
            Console.WriteLine("\nRandomly Generated Matrix:");
            Display(matrix);
            return matrix;
        }

        public int GetMatrixElement(int rowIndex, int columnIndex, int[,] matrix)
        {     
            int matrixElement = matrix[rowIndex, columnIndex];
            return matrixElement;
        }

        public void ModifyMatrixElement(int rowIndex, int columnIndex, int[,] matrix)
        {
            Console.Write("\nEnter the element to add: ");
            int addValue = Convert.ToInt32(Console.ReadLine());

            matrix[rowIndex, columnIndex] += addValue;
            Console.WriteLine("Modified Successfully......");
            Display(matrix);
        }      
    }
}
