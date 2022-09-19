using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

var a = Matrix.FillRandom(5, 3);
var b = Matrix.FillRandom(1, 5);

/* ThreadedSolve(a, b, 3); */
Matrix.Print(a);
foreach (var item in Matrix.GetSegments(a, 3))
    Matrix.Print(item);


void ThreadedSolve(int[,] matrixa, int[,] matrixb, int numberOfThreads = 1)
{
    var matrixc = new int[matrixa.GetLength(0), matrixb.GetLength(1)];
    var actualThreadCount = numberOfThreads < matrixa.GetLength(0) ? numberOfThreads : matrixa.GetLength(0);

    System.Console.WriteLine(matrixc.GetLength(0) + " " + matrixc.GetLength(1) + " " + actualThreadCount);
}

