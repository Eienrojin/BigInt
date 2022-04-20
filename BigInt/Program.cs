using System.Numerics;

namespace BigInt
{
    class Program
    {
        static void Main()
        {
            Test();
        }

        static void Test()
        {
            BigInteger num1 = 151;
            BigInteger num2 = -38;
            BigInteger resultI = num2 + num1;

            bigInt a = new bigInt(num1.ToString());
            bigInt b = new bigInt(num2.ToString());
            bigInt c = new bigInt("0");

            Console.WriteLine("До действий");
            Console.WriteLine(a);
            Console.WriteLine(b);

            //Действие тут
            c = a + b;

            Console.WriteLine("\nПосле сложения");
            Console.WriteLine($"{a}" +
                            $"\n{b}" +
                            $"\n-------" +
                            $"\n{c}");

            Console.WriteLine("\nЭталон");
            Console.WriteLine(resultI);
        }
    }
}