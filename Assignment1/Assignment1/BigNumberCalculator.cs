

namespace Assignment1
{
    using System.Text;

    public class BigNumberCalculator
    {
        private int mBitCount;
        private EMode mMode;

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            this.mBitCount = bitCount;
            this.mMode = mode;
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

            int carry = 1;
            int outBitCount = num.Length - 2;

            // 접두사 붙이기
            StringBuilder outStringBuilder = new StringBuilder(oneComplement.Length + 1);

            for (int i = 0; i < oneComplement.Length - 2; i++)
            {
                char c = oneComplement[oneComplement.Length - 1 - i];

                int value = FromCharToInt(c) + carry;

                if (value >= 2)
                {
                    value -= 2;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                outStringBuilder.Append(FromIntToChar(value));
            }

            // 원본 길이만큼만 보장
            if (outStringBuilder.Length > outBitCount)
            {
                outStringBuilder.Remove(outBitCount, outStringBuilder.Length);
            }

            return ReverseStringBuilder(ref outStringBuilder).Insert(0, "0b").ToString();
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

            // 16진수
            if (notation == ENotation.Hex)
            {
                StringBuilder outHexStringBuilder = new StringBuilder(num.Length * 4);

                num = num.Substring(2);
                foreach (char c in num)
                {
                    int value = FromCharToInt(c);

                    outHexStringBuilder.Append(ToStringInFourLetterBinary(value));
                }

                return outHexStringBuilder.Insert(0, "0b").ToString();
            }

            // 10진수

            if (num == "0")
            {
                return "0b0";
            }

            bool bNegativeFlag = false;

            if (notation == ENotation.Decimal && num.StartsWith('-'))
            {
                bNegativeFlag = true;
                num = num.Substring(1);
            }

            string quotient = num;
            StringBuilder outStringBuilder = new StringBuilder(num.Length);

            while (quotient != null && quotient != "")
            {
                int digit = FromCharToInt(quotient[quotient.Length - 1]);

                if (digit % 2 == 0)
                {
                    outStringBuilder.Append("0");
                }
                else
                {
                    outStringBuilder.Append("1");
                }

                quotient = GetQuotientDividedByTwo(quotient);
            }

            ReverseStringBuilder(ref outStringBuilder);
            outStringBuilder.Insert(0, "0b");

            if (bNegativeFlag)
            {
                string outResult = GetTwosComplementOrNull(outStringBuilder.ToString());
                if (outResult[2] == '0')
                {
                    outResult = outResult.Insert(2, "1");
                }

                return outResult;
            }

            outStringBuilder.Insert(2, '0');
            return outStringBuilder.ToString();
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

            string numInBinary = ToBinaryOrNull(num);

            return FromBinaryToHex(numInBinary);
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

            string binary = ToBinaryOrNull(num);

            return FromBinaryToDecimal(binary);
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (CheckNotation(num1) == ENotation.Not_Supported || CheckNotation(num2) == ENotation.Not_Supported)
            {
                return null;
            }

            string binary1 = ToBinaryOrNull(num1);
            string binary2 = ToBinaryOrNull(num2);

            int maxBitCount = binary1.Length > binary2.Length ? binary1.Length - 2 : binary2.Length - 2;
            if (maxBitCount > this.mBitCount)
            {
                return null;
            }

            string maxAbsBitCountForNegativeDecimal = GetMaxDecimal(this.mBitCount);
            string maxAbsBitCountForPositiveDecimal = SubtractTwoPositiveDecimalNumbers(maxAbsBitCountForNegativeDecimal, "1");

            string decimal1 = ToDecimalOrNull(num1);
            string decimal2 = ToDecimalOrNull(num2);

            string result = AddTwoDecimalNumbers(decimal1, decimal2);

            if (result[0] != '-')
            {
                string bigger = GetBiggerPositiveDecimalNum(maxAbsBitCountForPositiveDecimal, result);
                if (bigger == result && result != maxAbsBitCountForPositiveDecimal)
                {
                    bOverflow = true;
                    string difference = SubtractTwoPositiveDecimalNumbers(result, maxAbsBitCountForPositiveDecimal);
                    difference = SubtractTwoPositiveDecimalNumbers(difference, "1");

                    result = SubtractTwoPositiveDecimalNumbers(maxAbsBitCountForNegativeDecimal, difference).Insert(0, "-");
                }
            }
            else
            {
                string resultWithoutMinus = result.Substring(1);
                string bigger = GetBiggerPositiveDecimalNum(maxAbsBitCountForNegativeDecimal, resultWithoutMinus);
                if (bigger == resultWithoutMinus && resultWithoutMinus != maxAbsBitCountForNegativeDecimal)
                {
                    bOverflow = true;
                    string difference = SubtractTwoPositiveDecimalNumbers(result, maxAbsBitCountForNegativeDecimal);
                    difference = SubtractTwoPositiveDecimalNumbers(difference, "1");

                    result = SubtractTwoPositiveDecimalNumbers(maxAbsBitCountForPositiveDecimal, difference);
                }
            }

            if (this.mMode == EMode.Binary)
            {
                string binaryResult = ToBinaryOrNull(result);
                int differenceInBitCount = this.mBitCount - (binaryResult.Length - 2);

                for (int i = 0; i < differenceInBitCount; i++)
                {
                    if (binaryResult[2] == '0')
                    {
                        binaryResult = binaryResult.Insert(2, "0");
                    }
                    else
                    {
                        binaryResult = binaryResult.Insert(2, "1");
                    }
                }

                return binaryResult;
            }
            return result;
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (CheckNotation(num1) == ENotation.Not_Supported || CheckNotation(num2) == ENotation.Not_Supported)
            {
                return null;
            }

            string decimal1 = ToDecimalOrNull(num1);
            string decimal2 = ToDecimalOrNull(num2);

            string binary1 = ToBinaryOrNull(num1);
            string binary2 = ToBinaryOrNull(num2);

            int maxBitCount = binary1.Length > binary2.Length ? binary1.Length - 2 : binary2.Length - 2;
            if (maxBitCount > this.mBitCount)
            {
                return null;
            }

            if (decimal1 == decimal2)
            {
                if (binary1.Length - 2 > this.mBitCount)
                {
                    return null;
                }

                if (binary1[2] == '1')
                {
                    bOverflow = true;
                    for (int i = 3; i < binary1.Length; i++)
                    {
                        if (binary1[i] != '0')
                        {
                            bOverflow = false;
                            break;
                        }
                    }
                }

                if (this.mMode == EMode.Binary)
                {
                    StringBuilder binaryZero = new StringBuilder(this.mBitCount + 2);
                    binaryZero.Append("0b");
                    for (int i = 0; i < this.mBitCount; i++)
                    {
                        binaryZero.Append("0");
                    }
                    return binaryZero.ToString();
                }
                return "0";
            }

            if (decimal2[0] == '-')
            {
                decimal2 = decimal2.Substring(1);
            }
            else
            {
                decimal2 = decimal2.Insert(0, "-");
            }

            return AddOrNull(decimal1, decimal2, out bOverflow);
        }

        private static ref StringBuilder ReverseStringBuilder(ref StringBuilder stringbuilder)
        {
            int left = 0;
            int right = stringbuilder.Length - 1;

            while (left < right)
            {
                char c = stringbuilder[left];
                stringbuilder[left] = stringbuilder[right];
                stringbuilder[right] = c;

                left++;
                right--;
            }

            return ref stringbuilder;
        }

        private static string FromIntToHex(int num)
        {
            if (num < 10)
            {
                return num.ToString();
            }

            return ((char)((num - 10) + 'A')).ToString();
        }

        private static string FromBinaryToHex(string numInBinary)
        {
            int expotential = 4;

            StringBuilder outStringBuilder = new StringBuilder(numInBinary.Length / expotential);
            numInBinary = numInBinary.Substring(2);


            if ((numInBinary.Length) % 4 != 0)
            {
                int target = FromCharToInt(numInBinary[0]);
                int remain = 4 - (numInBinary.Length) % 4;

                for (int i = 0; i < remain; i++)
                {
                    if (target % 2 == 0)
                    {
                        numInBinary = numInBinary.Insert(0, "0");
                        continue;
                    }
                    numInBinary = numInBinary.Insert(0, "1");
                }
            }

            StringBuilder toReverseStringBuilder = new StringBuilder(numInBinary);
            ReverseStringBuilder(ref toReverseStringBuilder);
            string reversedNumInBinary = toReverseStringBuilder.ToString();

            // 1000 0100 1001 0110 0101 1101 0110 1111
            for (int i = 0; i < reversedNumInBinary.Length / expotential - 1; i++)
            {
                int value = FromCharToInt(reversedNumInBinary[expotential * i + 0]) * 1;
                value += FromCharToInt(reversedNumInBinary[expotential * i + 1]) * 2;
                value += FromCharToInt(reversedNumInBinary[expotential * i + 2]) * 4;
                value += FromCharToInt(reversedNumInBinary[expotential * i + 3]) * 8;

                outStringBuilder.Append(FromIntToHex(value));
            }

            int restIndex = expotential * (reversedNumInBinary.Length / expotential - 1);
            string lastDigit = FromReversedBinaryInFourLetterToHex(reversedNumInBinary.Substring(restIndex));
            outStringBuilder.Append(lastDigit);

            return ReverseStringBuilder(ref outStringBuilder).Insert(0, "0x").ToString();
        }

        private static string FromReversedBinaryInFourLetterToHex(string binary)
        {
            int result = 0;
            int accumulatedTwo = 1;
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                {
                    result += accumulatedTwo;
                }

                accumulatedTwo *= 2;
            }

            return FromIntToChar(result).ToString();
        }

        private static string FromBinaryToDecimal(string binary)
        {
            string decimalString = "0";
            string twoes = "1";
            bool bNegative = false;

            if (binary[2] == '1')
            {
                bNegative = true;
            }

            for (int i = binary.Length - 1; i > 2; i--)
            {
                if (binary[i] == '1')
                {
                    decimalString = AddTwoDecimalNumbers(twoes, decimalString);
                }

                twoes = AddTwoDecimalNumbers(twoes, twoes);
            }

            if (bNegative)
            {
                decimalString = SubtractTwoPositiveDecimalNumbers(twoes, decimalString);
                decimalString = decimalString.Insert(0, "-");
            }

            return decimalString;
        }

        private static string GetMaxDecimal(int bitCount)
        {
            string result = "1";

            if (bitCount < 1)
            {
                return result;
            }

            for (int i = 0; i < bitCount - 1; i++)
            {
                result = AddTwoDecimalNumbers(result, result);
            }

            return result;
        }

        private static string GetBiggerPositiveDecimalNum(string num1, string num2)
        {
            if (num1.Length > num2.Length)
            {
                return num1;
            }
            if (num2.Length > num1.Length)
            {
                return num2;
            }

            for (int i = 0; i < num1.Length; i++)
            {
                if (num1[i] > num2[i])
                {
                    return num1;
                }
                else if (num1[i] < num2[i])
                {
                    return num2;
                }
            }
            return num1;
        }

        private static string AddTwoDecimalNumbers(string bigger, string smaller)
        {
            bool bNegative = false;

            // 음수일 때 처리
            if (bigger[0] == '-' && smaller[0] == '-')
            {
                bNegative = true;
                bigger = bigger.Substring(1);
                smaller = smaller.Substring(1);
            }
            else if (bigger[0] == '-' || smaller[0] == '-')
            {
                if (bigger[0] == '-')
                {
                    bigger = bigger.Substring(1);
                    if (bigger == smaller)
                    {
                        return "0";
                    }

                    string result = GetBiggerPositiveDecimalNum(bigger, smaller);
                    if (result == bigger)
                    {
                        return SubtractTwoPositiveDecimalNumbers(bigger, smaller).Insert(0, "-");
                    }
                    return SubtractTwoPositiveDecimalNumbers(smaller, bigger);

                }
                else
                {
                    smaller = smaller.Substring(1);
                    if (bigger == smaller)
                    {
                        return "0";
                    }

                    string result = GetBiggerPositiveDecimalNum(bigger, smaller);
                    if (result == smaller)
                    {
                        return SubtractTwoPositiveDecimalNumbers(smaller, bigger).Insert(0, "-");
                    }
                    return SubtractTwoPositiveDecimalNumbers(bigger, smaller);
                }
            }


            if (bigger.Length < smaller.Length)
            {
                string tmp = bigger;
                bigger = smaller;
                smaller = tmp;
            }

            StringBuilder outStringBuilder = new StringBuilder(bigger.Length + 1);

            int differenceInDigits = bigger.Length - smaller.Length;
            for (int i = 0; i < differenceInDigits; i++)
            {
                smaller = smaller.Insert(0, "0");
            }

            int carry = 0;
            for (int i = 0; i < bigger.Length; i++)
            {
                int digit = FromCharToInt(bigger[bigger.Length - 1 - i]) + FromCharToInt(smaller[smaller.Length - 1 - i]) + carry;


                if (digit >= 10)
                {
                    digit -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                outStringBuilder.Append(FromIntToChar(digit));
            }

            if (carry != 0)
            {
                outStringBuilder.Append(FromIntToChar(carry));
            }

            if (bNegative)
            {
                outStringBuilder.Append('-');
            }

            return ReverseStringBuilder(ref outStringBuilder).ToString();
        }

        // 선조건: bigger, smaller 모두 양수
        private static string SubtractTwoPositiveDecimalNumbers(string bigger, string smaller)
        {
            string result = GetBiggerPositiveDecimalNum(bigger, smaller);

            if (result == smaller)
            {
                string tmp = bigger;
                bigger = smaller;
                smaller = tmp;
            }


            StringBuilder outStringBuilder = new StringBuilder(bigger.Length + 1);

            int differenceInDigits = bigger.Length - smaller.Length;
            for (int i = 0; i < differenceInDigits; i++)
            {
                smaller = smaller.Insert(0, "0");
            }


            bool bBorrow = false;
            for (int i = 0; i < bigger.Length; i++)
            {
                int digit = FromCharToInt(bigger[bigger.Length - 1 - i]) - FromCharToInt(smaller[smaller.Length - 1 - i]);

                if (bBorrow)
                {
                    digit -= 1;
                }

                if (digit < 0)
                {
                    bBorrow = true;
                    digit += 10;
                }
                else
                {
                    bBorrow = false;
                }

                outStringBuilder.Append(FromIntToChar((digit)));
            }

            string resultStr = ReverseStringBuilder(ref outStringBuilder).ToString();

            while (resultStr[0] == '0')
            {
                if (resultStr == "0")
                {
                    return resultStr;
                }

                resultStr = resultStr.Remove(0, 1);
            }

            return resultStr;
        }

        // 0 ~ 9 혹은 A ~ F 까지 숫자만 받는다
        private static int FromCharToInt(char c)
        {
            if ('0' <= c && c <= '9')
            {
                return c - '0';
            }

            return c - 'A' + 10;
        }

        private static char FromIntToChar(int digit)
        {
            if (digit >= 10)
            {
                return (char)((digit - 10 + 'A'));
            }

            return (char)((digit + '0'));
        }

        private static string ToStringInBinary(int num)
        {
            if (num < 2)
            {
                return num.ToString();
            }

            int remainder = num % 2;

            return ToStringInBinary(num / 2) + remainder.ToString();
        }

        private static string ToStringInFourLetterBinary(int num)
        {
            string binary = ToStringInBinary(num);

            while (binary.Length < 4)
            {
                binary = binary.Insert(0, "0");
            }

            return binary;
        }

        private static ENotation CheckNotation(string num)
        {
            if (num == null)
            {
                return ENotation.Not_Supported;
            }

            if (string.IsNullOrEmpty(num) || string.IsNullOrWhiteSpace(num))
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
                    if (!('A' <= c && c <= 'F') && !('0' <= c && c <= '9'))
                    {
                        return ENotation.Not_Supported;
                    }
                }
                return ENotation.Hex;
            }


            if (num.StartsWith('-'))
            {
                if (num.Length == 1 || num == "-0")
                {
                    return ENotation.Not_Supported;
                }
                num = num.Substring(1);
            }

            // 10진수, 01123
            if (num.StartsWith("0"))
            {
                if (num.Length == 1)
                {
                    return ENotation.Decimal;
                }

                return ENotation.Not_Supported;
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

        private static string GetQuotientDividedByTwo(string numInDecimal)
        {
            StringBuilder quotient = new StringBuilder(numInDecimal.Length);
            bool bRemainder = false;

            for (int i = 0; i < numInDecimal.Length; i++)
            {
                int digit = FromCharToInt(numInDecimal[i]);

                if (bRemainder)
                {
                    digit += 10;
                }

                quotient.Append((char)((digit / 2) + '0'));

                if (digit % 2 == 0)
                {
                    bRemainder = false;
                }
                else
                {
                    bRemainder = true;
                }
            }

            while (quotient[0] == '0')
            {
                quotient.Remove(0, 1);

                if (quotient.Length == 0)
                {
                    return null;
                }
            }

            return quotient.ToString();
        }
    }
}
