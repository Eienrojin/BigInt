using System.Numerics;

namespace BigInt
{
    class Program
    {
        static void Main()
        {
            bigInt a = new bigInt("324", 1);
            bigInt b = new bigInt("32", 1);

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(a > b);

            if (a <= b)
            {
                Console.WriteLine("Выполнилось");
            }
        }
    }
}