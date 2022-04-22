/*
 * ПРОБЛЕМНЫЕ ЧИСЛА
 * 5563 + (-4466)
 * (-4465453 + 5765486)
 */

namespace BigInt
{
    internal class BigInt : IComparable<BigInt>
    {
        public BigInt(string values)
        {
            Values = new();

            if (values[0] == '0') Sign = 0;
            if (values[0] != '0' && values[0] != '-') Sign = 1;

            if (values[0] == '-')
            {
                Sign = -1;
                values = values.Remove(0, 1);
            }

            for (int i = 0; i < values.Length; i++)
            {
                string numbers = values[i].ToString();

                Values.Add(Convert.ToSByte(numbers));
            }
        }

        private BigInt(BigInt other)
        {
            Values = new(other.Values);
            Sign = other.Sign;
        }

        private List<sbyte> Values { get; set; }

        public int Sign { get; set; } = 0;

        /*------------CALCULATING------------*/

        /*------------ADD------------*/
        public static BigInt operator +(BigInt left, BigInt right)
        {
            return Add(left, right, false);
        }

        //Note: it work only with positive values
        private static BigInt Add(BigInt a, BigInt b, bool isSubtraction)
        {
            int x, y, tempResult, result;
            bool differentSign = false;
            bool needNegativeSign = false;
            BigInt left, right;

            int overflow = 0;
            string tempString = "";
            string tempString2 = "";
            BigInt bigResult = new("0");

            List<sbyte> tempList = new List<sbyte>();

            //Bigger number will be on top, smaller on bottom
            if (!isSubtraction)
            {
                if (GetBiggerNum(a, b) == 1)
                {
                    tempString = a.ToString();
                    tempString2 = b.ToString();
                }
                else if (GetBiggerNum(a, b) == 0)
                {
                    tempString = a.ToString();
                    tempString2 = b.ToString();
                }
                else if (GetBiggerNum(a, b) == -1)
                {
                    tempString = b.ToString();
                    tempString2 = a.ToString();
                }
            }
            else
            {
                tempString = a.ToString(); 
                tempString2 = b.ToString(); 
            }

            left = new BigInt(tempString);
            right = new BigInt(tempString2);

            MakeEqualLength(left, right);

            for (int i = left.Values.Count - 1; i >= 0; i--)
            {
                x = left.Values[i];
                y = right.Values[i];

                if (left.Sign != right.Sign)
                {
                    differentSign = true;

                    x += overflow;
                    overflow = 0;

                    needNegativeSign = IsNeedOverflow(ref x, y);
                }

                tempResult = (x * left.Sign) + (y * right.Sign) + overflow;

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
            }

            if (left.Sign == right.Sign && overflow != 0)
            {
                tempList.Insert(tempList.Count, 1);
            }

            bigResult = PrepareResult(bigResult, tempList);

            DeleteUnusualZeroes(right);
            bigResult.Sign = IdentifySign(left, right);

            return bigResult;
        }
        private static void GetAbsList(List<sbyte> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = Math.Abs(list[i]);
            }
        }

        // Adds zeroes to the front of some List<>, if count of elements not equal
        private static void MakeEqualLength(BigInt left, BigInt right)
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

        private static int GetBiggerNum(BigInt left, BigInt right)
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

        private static void DeleteUnusualZeroes(BigInt bigInt)
        {
            for (int i = 0; i < bigInt.Values.Count; i++)
            {
                if (bigInt.Values[i] == 0)
                    bigInt.Values.RemoveAt(i);
                else
                    break;
            }
        }

        /*------------SUBTRACTION------------*/
        /*        public static BigInt operator -(BigInt left, BigInt right)
                {
                    return Subtraction(left, right);
                }*/

        public static BigInt operator -(BigInt left, BigInt right)
        {
            return Add(left, -right, true);
        }

        public static BigInt operator -(BigInt bigInt)
        {
            BigInt bigInt2 = new(bigInt);
            bigInt2.Sign *= -1;

            return bigInt2;
        }

        /*------------CALCULATING UTILLS------------*/
        //ДОРАБОТАТЬ
        private static int IdentifySign(BigInt left, BigInt right)
        {
            int sign = 0;

            //Работает - не трогай
            if (left.Sign == right.Sign)
            {
                return left.Sign;
            }

            if (left.Values.Count != right.Values.Count)
            {
                sign = left.Values.Count > right.Values.Count ? 1 : -1;
            }
            else
            {
                for (int i = 0; i < left.Values.Count; i++)
                {
                    sign = Math.Abs(left.Values[i]) > right.Values[i] ? 1 : -1;
                }
            }

            return sign;
        }

        private static BigInt PrepareResult(BigInt result, List<sbyte> list)
        {
            BigInt tempResult = result;

            GetAbsList(list);
            list.ToString();

            for (int i = 0; i <= list.Count - 1; i++)
            {
                tempResult.Values.Add(list[i]);
            }

            tempResult.Values.RemoveAt(0);
            tempResult.Values.Reverse();

            DeleteUnusualZeroes(tempResult);

            return tempResult;
        }

        private static bool IsNeedOverflow(ref int x, int y)
        {
            bool needNegativeSign = false;

            if (Math.Abs(x) == Math.Abs(y))
            {
                x += 10;
            }
            if (Math.Abs(x) < Math.Abs(y))
            {
                x += 10;
                needNegativeSign = true;
            }

            return needNegativeSign;
        }

        /*------------COMPARISON------------*/
        public static bool operator >(BigInt left, BigInt right)
        {
            return left.CompareTo(right) == 1;
        }
        public static bool operator <(BigInt left, BigInt right)
        {
            return left.CompareTo(right) == -1;
        }
        public static bool operator >=(BigInt left, BigInt right)
        {
            return left > right || left == right;
        }
        public static bool operator <=(BigInt left, BigInt right)
        {
            return left < right || left == right;
        }
        public static bool operator ==(BigInt left, BigInt right)
        {
            return left.CompareTo(right) == 0;
        }
        public static bool operator !=(BigInt left, BigInt right)
        {
            return left.CompareTo(right) != 0;
        }


        /*------------STANDART METODS------------*/
        public override string ToString()
        {
            string result;

            if (Sign == -1)
                result = "-" + string.Join("", Values);
            else
                result = string.Join("", Values);


            return result;
        }

        public int CompareTo(BigInt other)
        {
            if (Sign != other.Sign) return Sign.CompareTo(other.Sign);

            if (Values.Count != other.Values.Count) return Values.Count.CompareTo(other.Values.Count) * Sign;

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
