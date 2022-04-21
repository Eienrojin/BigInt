/*TODO*/
/*
* 
* СЛОЖЕНИЕ ПОЛОЖИТЕЛЬНОГО И ОТРИЦАТЕЛЬНОГО ЧИСЛА, ГДЕ |-А| > B
* ДОБАВИТЬ ПРОВЕРКУ ЧИСЛА НА ВВОД
* РЕШИТЬ ПРОБЛЕМУ С OVERFLOW (-4465453 + 5765486)
*/

/*СДЕЛАНО*/
/*
 * ПОФИКСИТЬ СЛОЖЕНИЕ ТИПА 9000+1000 = 0000 
 *
 * ПАРСИНГ ЗНАКА ЧИСЛА ПО ПЕРВОМУ ЗНАКУ
 * 0     - НУЛЕВОЕ ЗНАЧЕНИЕ
 * -432  - ОТРИЦАТЕЛЬНОЕ
 * 432   - ПОЛОЖИТЕЛЬНОЕ
 * 
 * ИММУТАЦИЯ МЕТОДА ADD
 */

/*
 * ПРОБЛЕМНЫЕ ЧИСЛА
 * 5563 + (-4466)
 * (-4465453 + 5765486)
 */

namespace BigInt
{
    internal class bigInt : IComparable<bigInt>
    {
        public bigInt(string values)
        {
            Values = new();

            if (values[0] == '0') Positive = 0;
            if (values[0] != '0' && values[0] != '-') Positive = 1;

            if (values[0] == '-')
            {
                Positive = -1;
                values = values.Remove(0, 1);
            }

            for (int i = 0; i < values.Length; i++)
            {
                string numbers = values[i].ToString();

                Values.Add(Convert.ToSByte(numbers));
            }
        }

        public bigInt(bigInt other)
        {
            Values = other.Values;
            Positive = other.Positive;
        }

        private List<sbyte> Values { get; set; }

        private int Positive { get; set; } = 0;

        /*------------CALCULATING------------*/

        /*------------ADD------------*/
        public static bigInt operator +(bigInt left, bigInt right)
        {
            return Add(left, right);
        }

        //Note: it work only with positive values
        private static bigInt Add(bigInt a, bigInt b)
        {
            int x, y, tempResult, result;
            bool differentSign = false;
            bool needNegativeSign = false;
            bigInt left, right;

            int overflow = 0;
            string tempString = "";
            string tempString2 = "";
            bigInt bigResult = new("0");

            List<sbyte> tempList = new List<sbyte>();

            //Bigger number will be on top, smaller on bottom
            if (GetBiggerNum(a, b) == 1)
            {
                tempString = a.ToString();
                tempString2 = b.ToString();
            }
            else if (GetBiggerNum(a, b) == 0)
            {
                tempString = a.ToString();
                tempString2 = a.ToString();
            }
            else if (GetBiggerNum(a,b) == -1)
            {
                tempString = b.ToString();
                tempString2 = a.ToString();
            }

            left = new bigInt(tempString);
            right = new bigInt(tempString2);

            MakeEqualLength(left, right);

            for (int i = left.Values.Count - 1; i >= 0; i--)
            {
                x = left.Values[i];
                y = right.Values[i];

                if (left.Positive != right.Positive)
                {
                    differentSign = true;

                    x += overflow;
                    overflow = 0;

                    if (Math.Abs(x) == Math.Abs(y))
                    {
                        x += 10;
                    }
                    if (Math.Abs(x) < Math.Abs(y))
                    {
                        x += 10;
                        needNegativeSign = true;
                    }
                }

                tempResult = (x * left.Positive) + (y * right.Positive) + overflow;

                if (differentSign && needNegativeSign)
                {
                    overflow = -1;
                    needNegativeSign = false;
                }
                if (!differentSign)
                {
                    overflow = tempResult / 10;
                }

                result = tempResult % 10;

                tempList.Add((sbyte)result);

                if (i == 0 && overflow == 1)
                {
                    tempList.Insert(tempList.Count, 1);
                }
            }

            GetAbsList(tempList);
            tempList.ToString();

            for (int i = 0; i <= tempList.Count - 1; i++)
            {
                bigResult.Values.Add(tempList[i]);
            }

            //preparing stage
            //delete zeroes from the front and reverse
            bigResult.Values.RemoveAt(0);
            bigResult.Values.Reverse();

            DeleteUnusualZeroes(bigResult);

            bigResult.Positive = SetSignForAdd(left, right);

            return bigResult;
        }

        //ДОРАБОТАТЬ
        private static int SetSignForAdd(bigInt left, bigInt right)
        {
            if (left.Positive == right.Positive)
            {
                return left.Positive;
            }

            return 0;
        }

        private static void GetAbsList(List<sbyte> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = Math.Abs(list[i]);
            }
        }

        // Adds zeroes to the front of some List<>, if count of elements not equal
        private static void MakeEqualLength(bigInt left, bigInt right)
        {
            if (left.Values.Count != right.Values.Count)
            {
                if (left.Values.Count > right.Values.Count)
                    for (int i = right.Values.Count; i < left.Values.Count; i++)
                        right.Values.Insert(0, 0);

                if (left.Values.Count < right.Values.Count)
                    for (int i = left.Values.Count; i < right.Values.Count; i++)
                        left.Values.Insert(0, 0);
            }
        }

        private static int GetBiggerNum(bigInt left, bigInt right)
        {
            if (left.Values.Count.CompareTo(right.Values.Count) == 0)
            {
                for (int i = 0; i < left.Values.Count; i++)
                {
                    if (left.Values[i].CompareTo(right.Values[i]) > 0)
                    {
                        return 1; 
                        break;
                    }
                    if (left.Values[i].CompareTo(right.Values[i]) < 0)
                    {
                        return -1;
                        break;
                    }
                }
            }
            if (left.Values.Count.CompareTo(right.Values.Count) > 0)
            {
                return 1;
            }
            if (left.Values.Count.CompareTo(right.Values.Count) < 0)
            {
                return -1;
            }

            return 0;
        }

        private static void DeleteUnusualZeroes(bigInt bigInt)
        {
            for (int i = 0; i < bigInt.Values.Count; i++)
            {
                if (bigInt.Values[i] == 0)
                    bigInt.Values.RemoveAt(i);
                else
                    break;
            }
        }

        /*------------COMPARISON------------*/
        public static bool operator >(bigInt left, bigInt right)
        {
            return left.CompareTo(right) == 1;
        }
        public static bool operator <(bigInt left, bigInt right)
        {
            return left.CompareTo(right) == -1;
        }
        public static bool operator >=(bigInt left, bigInt right)
        {
            return left > right || left == right;
        }
        public static bool operator <=(bigInt left, bigInt right)
        {
            return left < right || left == right;
        }
        public static bool operator ==(bigInt left, bigInt right)
        {
            return left.CompareTo(right) == 0;
        }
        public static bool operator !=(bigInt left, bigInt right)
        {
            return left.CompareTo(right) != 0;
        }

        public override string ToString()
        {
            string result;

            if (Positive == -1)
                result = "-" + string.Join("", Values);
            else
                result = string.Join("", Values);


            return result;
        }

        public int CompareTo(bigInt other)
        {
            if (Positive != other.Positive) return Positive.CompareTo(other.Positive);

            if (Values.Count != other.Values.Count) return Values.Count.CompareTo(other.Values.Count) * Positive;

            if (Values.Count != other.Values.Count) return Values.Count.CompareTo(other.Values.Count);

            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i] > other.Values[i]) return 1;
                if (Values[i] < other.Values[i]) return -1;
            }

            return 0;
        }        
    }
}
