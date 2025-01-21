static int EuclideanAlgorithm(int a, int b)
{
    while (b != 0)
    {
        int temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

int x = EuclideanAlgorithm(500, 250);


Console.WriteLine(x);