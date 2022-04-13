using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigInt
{
    internal struct bigInt : IComparable<bigInt>
    {
        public bigInt(string values, int positive)
        {
            Values = new();
            Positive = positive;

            for (int i = 0; i < values.Length; i++)
            {
                string numbers = values[i].ToString();

                Values.Add(Convert.ToSByte(numbers));
            }
        }

        private List<sbyte> Values { get; set; }

        private int Positive { get; set; }

        /*------CALCULATING------*/
        public static bigInt Add(bigInt left, bigInt right)
        {

        }

        /*------COMPARISON------*/

        public static bool operator >(bigInt left, bigInt right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(bigInt left, bigInt right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator ==(bigInt left, bigInt right)
        {
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(bigInt left, bigInt right)
        {
            return left.CompareTo(right) != 0;
        }

        public static bool operator >=(bigInt left, bigInt right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(bigInt left, bigInt right)
        {
            return left < right || left == right;
        }

        public override string ToString()
        {
            string result = string.Join("", Values);

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
