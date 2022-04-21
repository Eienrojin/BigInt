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
            BigInteger num1 = BigInteger.Parse("-73545");
            BigInteger num2 = BigInteger.Parse("9188");
            BigInteger resultI = num2 + num1;

            bigInt a = new bigInt(num1.ToString());
            bigInt b = new bigInt(num2.ToString());
            bigInt c = new bigInt("0");

            Console.WriteLine("До действий");
            Console.WriteLine(a);
            Console.WriteLine(b);

            //Действие тут
            c = a + b;

            string resultEtalon = resultI.ToString();
            string resultMyBigInt = c.ToString();

            Console.WriteLine("\nПосле сложения");
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