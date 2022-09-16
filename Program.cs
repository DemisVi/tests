using System;
using System.Numerics;

var rnd = new Random(DateTime.Now.Millisecond);

int sizeX = int.Parse(args[0]);
int sizeY = int.Parse(args[0]);


var matrixC = new int[sizeX, sizeY];

var time = new TimeSpan(0);
for (int i = 0; i < 10; i++)
{
    var matrixA = Fill(rnd, sizeX, sizeY);
    var matrixB = Fill(rnd, sizeX, sizeY);
    var start = DateTime.Now;
    matrixC = Solve(matrixA, matrixB);
    time += DateTime.Now - start;
}
System.Console.WriteLine(time / 10);

time = new TimeSpan(0);
for (int i = 0; i < 10; i++)
{
    var matrixA = Fill(rnd, sizeX, sizeY);
    var matrixB = Transpose(Fill(rnd, sizeX, sizeY));
    var start = DateTime.Now;
    matrixC = SolveTrans(matrixA, matrixB);
    time += DateTime.Now - start;
}
System.Console.WriteLine(time / 10);

int[,] Fill(Random rand, int sizeX, int sizeY)
{
    var temp = new int[sizeX, sizeY];

    for (int i = 0; i < sizeX; i++)
    {
        for (int j = 0; j < sizeY; j++)
        {
            temp[i, j] = rand.Next(0, 101);
        }
    }

    return temp;
}

int[,] Solve(int[,] matrixa, int[,] matrixb)
{
    var res = new int[matrixa.GetLength(0), matrixa.GetLength(0)];

    for (int i = 0; i < sizeX; i++)
        for (int j = 0; j < sizeY; j++)
        {
            var temp = 0;
            for (int k = 0; k < sizeY; k++)
                temp += matrixa[i, k] * matrixb[k, j];
            res[i, j] = temp;
        }

    return res;
}

int[,] Transpose(int[,] matrix)
{
    var transp = new int[matrix.GetLength(0), matrix.GetLength(0)];

    for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(0); j++)
            transp[i, j] = matrix[j, i];

    return transp;
}

int[,] SolveTrans(int[,] matrixa, int[,] matrixb)
{
    var res = new int[matrixa.GetLength(0), matrixa.GetLength(0)];

    for (int i = 0; i < sizeX; i++)
        for (int j = 0; j < sizeY; j++)
        {
            var temp = 0;
            for (int k = 0; k < sizeY; k++)
                temp += matrixa[i, k] * matrixb[j, k];
            res[i, j] = temp;
        }

    return res;
}