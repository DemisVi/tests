int MAX = int.Parse(args[0]);

for (int i = 0; i < MAX; i++)
{
    for (int j = 0; j < MAX; j++)
    {
        System.Console.Write(GetSymbol(i, j, MAX));
    }
    System.Console.WriteLine();
}

char GetSymbol(int i, int j, int mAX)
{
    return i == j || i == mAX - 1 - j ||
           i == 0 || i == mAX - 1 ||
           j == 0 || j == mAX - 1 ? '#' : ' ';
}