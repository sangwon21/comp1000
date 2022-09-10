

namespace Assignment1
{
    using System.Text;

    public class BigNumberCalculator
    {
        private int bitCount;
        private EMode mode;

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            this.bitCount = bitCount;
            this.mode = mode;
        }

        private static string intToNotationForm(int num)
        {
            if (num < 10)
            {
                return num.ToString();
            }

            return ((num - 10) + 'A').ToString();
        }

        private static string BinaryToNotationForm(string num, int expotential)
        {
            string numInBinary = ToBinaryOrNull(num);
            StringBuilder outStringBuilder = new StringBuilder(numInBinary.Length / expotential);

            return null;
            /*   numInBinary.Reverse().toString();

               for (int i = 0; i < num.Length / expotential - 1; i++)
               {
                   int value = num[expotential * i + 0] * 1;
                   value += num[expotential * i + 1] * 2;
                   value += num[expotential * i + 2] * 4;
                   value += num[expotential * i + 3] * 8;

                   outStringBuilder.Append(intToNotationForm(value));
               }

               for (int i = 0; i < num.Length % expotential; i++)
               {
                   int quotient = num.Length / expotential;
                   int value = num[3 * quotient + i] * Pow(2, i);

                   outStringBuilder.Append(intToNotationForm(value));
               }

               string outString = outStringBuilder.ToString();
               outString.Reverse();

               return outString;*/
        }

        // 0 ~ 9 혹은 A ~ F 까지 숫자만 받는다
        private static int CharToIntValue(char c)
        {
            if ('0' <= c && c <= '9')
            {
                return c - '0';
            }

            return c - 'A';
        }

        private static string ToStringInBinary(int num)
        {
            if (num < 2)
            {
                return num.ToString();
            }

            int remainder = num % 2;

            return remainder.ToString() + ToStringInBinary(num / 2);
        }

        private static ENotation CheckNotation(string num)
        {
            if (num == null)
            {
                return ENotation.Not_Supported;
            }

            // 2진법
            if (num.StartsWith("0b"))
            {
                if (num.Length == 2)
                {
                    return ENotation.Not_Supported;
                }

                num = num.Substring(2);
                foreach (char c in num)
                {
                    if (!(c == '0' || c == '1'))
                    {
                        return ENotation.Not_Supported;
                    }
                }
                return ENotation.Binary;
            }

            if (num.StartsWith("0x"))
            {
                if (num.Length == 2)
                {
                    return ENotation.Not_Supported;
                }

                num = num.Substring(2);
                foreach (char c in num)
                {
                    // A ~ F 혹은 0 ~ 9
                    if (!('A' <= c && c <= 'F') || !('0' <= c && c <= '9'))
                    {
                        return ENotation.Not_Supported;
                    }
                }
                return ENotation.Hex;
            }


            if (num.StartsWith('-'))
            {
                num = num.Substring(1);
            }

            foreach (char c in num)
            {
                // 0 ~ 9
                if (!('0' <= c && c <= '9'))
                {
                    return ENotation.Not_Supported;
                }
            }
            return ENotation.Decimal;
        }

        private static int Pow(int x, int y)
        {
            int outResult = 1;
            for (int i = 0; i < y; i++)
            {
                outResult *= x;
            }

            return outResult;
        }

        // num의 1의 보수를 2진수 포맷으로 반환합니다.
        // 이 함수는 num이 10진수나 16진수 포맷일 경우 null을 반환해야 합니다.
        // num이 올바르지 않은 포맷인 경우에도 null을 반환합니다.
        public static string GetOnesComplementOrNull(string num)
        {
            ENotation notation = CheckNotation(num);

            if (notation != ENotation.Binary)
            {
                return null;
            }

            // 접두사 붙이기
            StringBuilder outStringBuilder = new StringBuilder(num.Length);
            outStringBuilder.Append("0b");

            num = num.Substring(2);
            foreach (char c in num)
            {
                if (c == '0')
                {
                    outStringBuilder.Append("1");
                    continue;
                }
                outStringBuilder.Append("0");
            }

            return outStringBuilder.ToString();
        }

        public static string GetTwosComplementOrNull(string num)
        {
            string oneComplement = GetOnesComplementOrNull(num);

            if (oneComplement == null)
            {
                return null;
            }

            int carry = 0;

            // 접두사 붙이기
            StringBuilder outStringBuilder = new StringBuilder(oneComplement.Length + 1);
            outStringBuilder.Append("0b");

            for (int i = 0; i < oneComplement.Length; i++)
            {
                char c = oneComplement[oneComplement.Length - i];

                int value = CharToIntValue(c) + carry;

                if (value >= 2)
                {
                    value -= 2;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                outStringBuilder.Append(value + '0');
            }

            return outStringBuilder.ToString();
        }

        public static string ToBinaryOrNull(string num)
        {
            ENotation notation = CheckNotation(num);

            if (notation == ENotation.Not_Supported)
            {
                return null;
            }

            if (notation == ENotation.Binary)
            {
                return num;
            }

            StringBuilder outString = new StringBuilder(num.Length * 4);

            foreach (char c in num)
            {
                int value = CharToIntValue(c);

                outString.Append(ToStringInBinary(value));
            }

            return outString.ToString();
        }

        public static string ToHexOrNull(string num)
        {
            ENotation notation = CheckNotation(num);

            if (notation == ENotation.Not_Supported)
            {
                return null;
            }

            if (notation == ENotation.Hex)
            {
                return num;
            }

            return BinaryToNotationForm(num, 3);
        }

        public static string ToDecimalOrNull(string num)
        {
            ENotation notation = CheckNotation(num);

            if (notation == ENotation.Not_Supported)
            {
                return null;
            }

            if (notation == ENotation.Decimal)
            {
                return num;
            }

            return BinaryToNotationForm(num, 4);
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (CheckNotation(num1) == ENotation.Not_Supported || CheckNotation(num2) == ENotation.Not_Supported)
            {
                return null;
            }

            string num1InBinary = ToBinaryOrNull(num1);
            string num2InBinary = ToBinaryOrNull(num2);

            // TODO: StringBuilder에 0b 0x + - 추가
            StringBuilder outStringBuilder = new StringBuilder(this.bitCount + 2);
            int shorterBitCount = num1InBinary.Length > num2InBinary.Length ? num2InBinary.Length : num1InBinary.Length;

            int carry = 0;
            for (int i = 0; i < shorterBitCount; i++)
            {
                int num1IntValue = CharToIntValue(num1InBinary[i]);
                int num2IntValue = CharToIntValue(num2InBinary[i]);

                int addedValue = num1IntValue + num2IntValue + carry;

                if (addedValue > 2)
                {
                    addedValue -= 2;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                outStringBuilder.Append(addedValue);
            }

            if (outStringBuilder.Length > this.bitCount)
            {
                bOverflow = true;
                return null;
            }

            if (this.mode == EMode.Decimal)
            {
                return null;
            }

            return null;

        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            return null;
        }
    }
}
