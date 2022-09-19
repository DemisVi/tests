using System;
using System.Collections.Generic;

public static class Matrix
{
    public static int[,] Multiply(int[,] matrixa, int[,] matrixb)
    {
        if (matrixa.GetLength(1) != matrixb.GetLength(0))
            throw new ArithmeticException("Multiplication not possible");

        var resultRowLength = matrixa.GetLength(0);
        var resultColumnLength = matrixb.GetLength(1);
        var initialBColumnLength = matrixb.GetLength(0);

        var result = new int[resultRowLength, resultColumnLength];

        // var temp = 0;

        for (int i = 0; i < resultRowLength; i++)
            for (int j = 0; j < resultColumnLength; j++)
            {
                // temp = 0;
                for (int k = 0; k < initialBColumnLength; k++)
                    result[i, j] += matrixa[i, k] * matrixb[k, j];
                // result[i, j] = temp;
            }

        return result;
    }

    public static void Print(int[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                System.Console.Write("{0,-10}", matrix[i, j]);
            System.Console.WriteLine();
        }
        System.Console.WriteLine();
    }

    public static int[,] FillRandom(int rows, int cols)
    {
        var rand = new Random(DateTime.Now.Millisecond);

        var temp = new int[rows, cols];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                temp[i, j] = rand.Next(0, 101);

        return temp;
    }

    public static List<int[,]> GetSegments(int[,] matrix, int count)
    {
        var matrixRows = matrix.GetLength(0);
        var matrixColumns = matrix.GetLength(1);
        var segmentRows = matrixRows % count != 0 ? matrixRows / count + 1 : matrixRows / count;
        var remainder = matrixRows % segmentRows;
        var result = new List<int[,]>(count);

        for (var i = 0; i < matrixRows; i += segmentRows)
        {
            if (i + remainder > matrixRows - 1) segmentRows = remainder;
            var temp = new int[segmentRows, matrixColumns];
            for (var j = 0; j < matrixColumns; j++)
                for (var k = 0; k < segmentRows; k++)
                {
                    temp[k, j] = matrix[i + k, j];
                }
            result.Add(temp);
        }

        return result;
    }
}