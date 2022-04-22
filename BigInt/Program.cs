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
            BigInteger num1 = BigInteger.Parse("35657576");
            BigInteger num2 = BigInteger.Parse("-545463");
            BigInteger resultI;

            BigInt a = new BigInt(num1.ToString());
            BigInt b = new BigInt(num2.ToString());
            BigInt c = new BigInt("0");

            Console.WriteLine("До действий");
            Console.WriteLine(a);
            Console.WriteLine(b);

            //Действие тут
            resultI = num1 - num2;
            c = a - b;

            string resultEtalon = resultI.ToString();
            string resultMyBigInt = c.ToString();

            Console.WriteLine("\nПосле вычитания");
            Console.WriteLine($"{a}" +
                            $"\n{b}" +
                            $"\n-------" +
                            $"\n{c}");

            Console.WriteLine("\nЭталон");
            Console.WriteLine(resultI);

            Console.WriteLine(resultMyBigInt == resultEtalon);
        }
    }
}