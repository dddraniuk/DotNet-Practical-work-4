using System;
using System.Linq;

class MatrixAndPascalTriangle
{
    static void Main()
    {
       
        Console.WriteLine("--- 1. Множення матриць ---");
        int[,] matrixA = { { 1, 2, 3 }, { 4, 5, 6 } };
        int[,] matrixB = { { 7, 8 }, { 9, 10 }, { 11, 12 } };
        int[,] matrixC = MultiplyMatrices(matrixA, matrixB);
        PrintMatrix(matrixC);

  
        Console.WriteLine("\n--- 2. Макс. суми у матрицi 3x3 ---");
        int[,] matrixN = { { 1, 9, 2 }, { 4, 5, 6 }, { 7, 3, 8 } };
        FindMaxSum(matrixN);

        
        Console.WriteLine("\n--- 3. Змiйкове заповнення ---");
        int n = 4;
        int[,] snakeMatrix = FillSnake(n);
        PrintMatrix(snakeMatrix);

        
        Console.WriteLine("\n--- 4. Трикутник Паскаля (N=8) ---");
        PrintPascalTriangle(8);
    }

    static int[,] MultiplyMatrices(int[,] A, int[,] B)
    {
        int rA = A.GetLength(0), cA = A.GetLength(1), cB = B.GetLength(1);
        int[,] res = new int[rA, cB];
        for (int i = 0; i < rA; i++)
            for (int j = 0; j < cB; j++)
                for (int k = 0; k < cA; k++)
                    res[i, j] += A[i, k] * B[k, j];
        return res;
    }

    static void FindMaxSum(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        int maxRowSum = int.MinValue, rowIndex = 0;
        int maxColSum = int.MinValue, colIndex = 0;

        for (int i = 0; i < size; i++)
        {
            int rowSum = 0, colSum = 0;
            for (int j = 0; j < size; j++)
            {
                rowSum += matrix[i, j];
                colSum += matrix[j, i];
            }
            if (rowSum > maxRowSum) { maxRowSum = rowSum; rowIndex = i; }
            if (colSum > maxColSum) { maxColSum = colSum; colIndex = i; }
        }
        Console.WriteLine($"Рядок {rowIndex} має макс. суму: {maxRowSum}");
        Console.WriteLine($"Стовпець {colIndex} має макс. суму: {maxColSum}");
    }

    static int[,] FillSnake(int n)
    {
        int[,] matrix = new int[n, n];
        int val = 1;
        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0)
                for (int j = 0; j < n; j++) matrix[i, j] = val++;
            else
                for (int j = n - 1; j >= 0; j--) matrix[i, j] = val++;
        }
        return matrix;
    }

    static void PrintPascalTriangle(int n)
    {
        int[][] triangle = new int[n][];
        for (int i = 0; i < n; i++)
        {
            triangle[i] = new int[i + 1];
            triangle[i][0] = triangle[i][i] = 1;
            for (int j = 1; j < i; j++)
                triangle[i][j] = triangle[i - 1][j - 1] + triangle[i - 1][j];

           
            Console.Write(new string(' ', (n - i) * 2));
            foreach (var item in triangle[i]) Console.Write($"{item,4}");
            Console.WriteLine();
        }
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write($"{matrix[i, j],4}");
            Console.WriteLine();
        }
    }
}