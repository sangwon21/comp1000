using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== STARTED =====");

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("as89fdf0") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0xFAKEHEX") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0bFAKEBINARY") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("FAKEDECIMAL") == null);

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("-") == null);

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("-10") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0xFC34") == null);

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b0000111010110") == "0b1111000101001");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b1000") == "0b0111");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b0110101011101011100000") == "0b1001010100010100011111");

            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b0000111010110") == "0b1111000101010");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b1000") == "0b1000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b00001101011") == "0b00001101011");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x00F24") == "0b00000000111100100100");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("123") == "0b01111011");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-123") == "0b10000101");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-144") == "-144");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x443FF") == "279551");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x843FF") == "-506881");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x843FF66FFCDDCDDDCDFFF") == "-9350296660948911804063745");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b011110001111010101011") == "990891");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b11110000") == "-16");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("-155555551") == "0xF6BA6921");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("5258") == "0x148A");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0x53ABC") == "0x53ABC");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b110001001") == "0xF89");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b000000110001001") == "0x0189" || BigNumberCalculator.ToHexOrNull("0b000000110001001") == "0x189");

            bool bOverflow = false;
            BigNumberCalculator calc1 = new BigNumberCalculator(8, EMode.Decimal);

            Debug.Assert(calc1.AddOrNull("127", "-45", out bOverflow) == "82");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc1.AddOrNull("128", "-45", out bOverflow) == null);
            Debug.Assert(!bOverflow);

            Debug.Assert(calc1.AddOrNull("120", "17", out bOverflow) == "-119");
            Debug.Assert(bOverflow);

            Debug.Assert(calc1.AddOrNull("-127", "0xE", out bOverflow) == "127");
            Debug.Assert(bOverflow);

            Debug.Assert(calc1.SubtractOrNull("25", "52", out bOverflow) == "-27");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc1.SubtractOrNull("0b100110", "-12", out bOverflow) == "-14");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc1.SubtractOrNull("0b0001101", "10", out bOverflow) == "3");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc1.SubtractOrNull("-125", "100", out bOverflow) == "31");
            Debug.Assert(bOverflow);

            BigNumberCalculator calc2 = new BigNumberCalculator(8, EMode.Binary);

            Debug.Assert(calc2.AddOrNull("127", "-45", out bOverflow) == "0b01010010");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc2.AddOrNull("0b10000000", "0x6", out bOverflow) == "0b10000110");
            Debug.Assert(!bOverflow);

            // 15 - 1
            Debug.Assert(calc2.AddOrNull("0b01111", "0b11", out bOverflow) == "0b00001110");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc2.AddOrNull("50", "0b0110", out bOverflow) == "0b00111000");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc2.SubtractOrNull("25", "52", out bOverflow) == "0b11100101");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc2.SubtractOrNull("0b100110", "-12", out bOverflow) == "0b11110010");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc2.SubtractOrNull("0b0001101", "10", out bOverflow) == "0b00000011");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc2.SubtractOrNull("-125", "100", out bOverflow) == "0b00011111");
            Debug.Assert(bOverflow);

            BigNumberCalculator calc3 = new BigNumberCalculator(100, EMode.Decimal);

            Debug.Assert(calc3.AddOrNull("126585123123216548452353151521", "5646862135432184515421587", out bOverflow) == "126590769985351980636868573108");
            Debug.Assert(!bOverflow);

            Debug.Assert(calc3.SubtractOrNull("-889874837998729348827376462", "577257635827634627837676734", out bOverflow) == "-1467132473826363976665053196");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.SubtractOrNull("1", "ff", out bOverflow) == null);

            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0B") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b0") == "0b0");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b10") == "0b10");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b100") == "0b100");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b1000") == "0b1000");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b10000") == "0b10000");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b100000") == "0b100000");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b1000000") == "0b1000000");

            // D01_Invalid Input Format
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-0") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0101") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0023") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("--11") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("00000000") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("+11") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b0b") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b0x") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0xx0b") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("  24aA1  ") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull(" 123 3VXCa  ") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0bAA") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("KJDSLF:N(&#") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("#$@#$@#$") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("SER#$V@$V") == null);
            // D03_Decimal Input : Decimal -> Binary
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-3") == "0b101");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("123"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("123") == "0b01111011");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("-123"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-123") == "0b10000101");
            Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("0"));
            Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("0x0"));
            Console.WriteLine(BigNumberCalculator.ToDecimalOrNull("0"));
            Console.WriteLine(BigNumberCalculator.ToHexOrNull("0"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0") == "0b0");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("10"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("10") == "0b01010");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("100"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("100") == "0b01100100");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("1000"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("1000") == "0b01111101000");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("10000"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("10000") == "0b010011100010000");
            //Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("-13579"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-13579") == "0b100101011110101");
            // Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("-"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-135799753113579") == "0b100001000111110110100111111101001000000000010101");
            Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("-9223372036854775808")); // long.minvalue
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-9223372036854775808") == "0b1000000000000000000000000000000000000000000000000000000000000000");
            Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("                                                 "));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-9223372036854775809") == "0b10111111111111111111111111111111111111111111111111111111111111111");
            Console.WriteLine(BigNumberCalculator.ToBinaryOrNull("-9223372036854775810"));
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-9223372036854775810") == "0b10111111111111111111111111111111111111111111111111111111111111110");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("1") == "0b01");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("2") == "0b010");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("4") == "0b0100");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("8") == "0b01000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("16") == "0b010000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("32") == "0b0100000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-1") == "0b1");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-2") == "0b10");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-4") == "0b100");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-8") == "0b1000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-16") == "0b10000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-32") == "0b100000");


            // E01_InvalidInputFormat //
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("as89fdf0") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xFAKEHEX") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0bFAKEBINARY") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("FAKEDECIMAL") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-0") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0101") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0023") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("--11") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("00000000") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("+11") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b0b") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b0x") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xx0b") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("  24aA1  ") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull(" 123 3VXCa  ") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0bAA") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("KJDSLF:N(&#") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("#$@#$@#$") == null);
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("SER#$V@$V") == null);
            // E03_DecimalInput //
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0") == "0");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("1") == "1");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("2") == "2");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("4") == "4");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("8") == "8");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("16") == "16");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("32") == "32");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-1") == "-1");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-2") == "-2");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-4") == "-4");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-8") == "-8");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-16") == "-16");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("-32") == "-32");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("-155555551") == "0xF6BA6921");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("5258") == "0x148A");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0x53ABC") == "0x53ABC");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b110001001") == "0xF89");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b000000110001001") == "0x0189" || BigNumberCalculator.ToHexOrNull("0b000000110001001") == "0x189");
            // F01_InvalidInputFormat //
            Debug.Assert(BigNumberCalculator.ToHexOrNull("as89fdf0") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0xFAKEHEX") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0bFAKEBINARY") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("FAKEDECIMAL") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-0") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0101") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0023") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("--11") == null);

            Debug.Assert(BigNumberCalculator.ToHexOrNull("00000000") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("+11") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b0b") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b0x") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0xx0b") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("  24aA1  ") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull(" 123 3VXCa  ") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0bAA") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("KJDSLF:N(&#") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("#$@#$@#$") == null);
            Debug.Assert(BigNumberCalculator.ToHexOrNull("SER#$V@$V") == null);
            // F03_DecimalInput //
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0") == "0x0");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("1") == "0x1");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("2") == "0x2");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("4") == "0x4");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("8") == "0x08");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("16") == "0x10");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("32") == "0x20");

            AddOrNullTest();
            Test();

            Console.WriteLine("===== FINISHED =====");

        }
        static void Test()
        {
            var value = new List<string> { "p@cu", null, "", " ", " 1", "01", "0x", "0xfF", "-0", "+0", "0x3AB1", "256", "-1" };

            #region GetOnesComplementOrNull

            // Invalid Value Test
            for (int i = 0; i < value.Count; ++i)
            {
                Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull(value[i]) == null);
            }

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b0") == "0b1");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b00") == "0b11");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b000") == "0b111");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b0000") == "0b1111");

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b01") == "0b10");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b1") == "0b0");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b10") == "0b01");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b100") == "0b011");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b101") == "0b010");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b110") == "0b001");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b111") == "0b000");
            #endregion

            #region GetTwosComplementOrNull

            // Invalid Value Test
            for (int i = 0; i < value.Count; ++i)
            {
                Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull(value[i]) == null);
            }

            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b0") == "0b0");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b00") == "0b00");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b000") == "0b000");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b0000") == "0b0000");

            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b01") == "0b11");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b1") == "0b1");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b10") == "0b10");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b100") == "0b100");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b101") == "0b011");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b110") == "0b010");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b111") == "0b001");
            #endregion

            #region ToBinaryOrNull

            // Invalid Value Test
            for (int i = 0; i < value.Count - 4; ++i)
            {
                Debug.Assert(BigNumberCalculator.ToBinaryOrNull(value[i]) == null);
            }

            // Decimal to Birnary test
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0") == "0b0");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("1") == "0b01");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-1") == "0b1");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("2") == "0b010");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-2") == "0b10");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("3") == "0b011");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-3") == "0b101");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("4") == "0b0100");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-4") == "0b100");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("5") == "0b0101");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-5") == "0b1011");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("6") == "0b0110");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-6") == "0b1010");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("7") == "0b0111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-7") == "0b1001");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("8") == "0b01000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-8") == "0b1000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("9") == "0b01001");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-9") == "0b10111");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("10") == "0b01010");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-10") == "0b10110");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("11") == "0b01011");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-11") == "0b10101");


            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("12") == "0b01100");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-12") == "0b10100");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("13") == "0b01101");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-13") == "0b10011");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("14") == "0b01110");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-14") == "0b10010");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("15") == "0b01111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-15") == "0b10001");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("16") == "0b010000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-16") == "0b10000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("9223372036854775807") == "0b0111111111111111111111111111111111111111111111111111111111111111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-9223372036854775808") == "0b1000000000000000000000000000000000000000000000000000000000000000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("18446744073709551615") == "0b01111111111111111111111111111111111111111111111111111111111111111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-18446744073709551616") == "0b10000000000000000000000000000000000000000000000000000000000000000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("170141183460469231731687303715884105727") == "0b01111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-170141183460469231722463931679029329920") == "0b10000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000");

            // Hex to Binary Test
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x08") == "0b00001000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0xF8") == "0b11111000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x8") == "0b1000");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0xFF") == "0b11111111");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x7FFFFFFFFFFFFFFF") == "0b0111111111111111111111111111111111111111111111111111111111111111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x8000000000000000") == "0b1000000000000000000000000000000000000000000000000000000000000000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x0FFFFFFFFFFFFFFFF") == "0b00001111111111111111111111111111111111111111111111111111111111111111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0xF0000000000000000") == "0b11110000000000000000000000000000000000000000000000000000000000000000");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF") == "0b01111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x80000000000000008000000000000000") == "0b10000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000");
            #endregion

            #region ToDecimalOrNull

            // Invalid Value Test
            for (int i = 0; i < value.Count - 4; ++i)
            {
                Debug.Assert(BigNumberCalculator.ToDecimalOrNull(value[i]) == null);
            }

            //Binary to Decimal Test
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b0") == "0");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b01") == "1");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b1") == "-1");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b10") == "-2");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b010") == "2");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b0100") == "4");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b100") == "-4");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b01000") == "8");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b1000") == "-8");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b010000") == "16");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b10000") == "-16");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b0111111111111111111111111111111111111111111111111111111111111111") == "9223372036854775807");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b1000000000000000000000000000000000000000000000000000000000000000") == "-9223372036854775808");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b01111111111111111111111111111111111111111111111111111111111111111") == "18446744073709551615");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b10000000000000000000000000000000000000000000000000000000000000000") == "-18446744073709551616");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b01111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111") == "170141183460469231731687303715884105727");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0b10000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000") == "-170141183460469231722463931679029329920");

            // Hex to Decimal Test
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x0") == "0");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x01") == "1");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x1") == "1");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xF") == "-1");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xFF") == "-1");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x2") == "2");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x02") == "2");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xFE") == "-2");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x04") == "4");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xFC") == "-4");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x08") == "8");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xF8") == "-8");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x8") == "-8");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x010") == "16");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xF0") == "-16");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x7FFFFFFFFFFFFFFF") == "9223372036854775807");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x8000000000000000") == "-9223372036854775808");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x0FFFFFFFFFFFFFFFF") == "18446744073709551615");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0xF0000000000000000") == "-18446744073709551616");

            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF") == "170141183460469231731687303715884105727");
            Debug.Assert(BigNumberCalculator.ToDecimalOrNull("0x80000000000000008000000000000000") == "-170141183460469231722463931679029329920");
            #endregion

            #region ToHexOrNull

            // Invalid Value Test
            for (int i = 0; i < value.Count - 4; ++i)
            {
                Debug.Assert(BigNumberCalculator.ToHexOrNull(value[i]) == null);
            }

            // Binary to Hex Test
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b0") == "0x0");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b01") == "0x1"); // 1
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b1") == "0xF"); // -1
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b11") == "0xF"); // - 1
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b11111") == "0xFF"); // - 1
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b111111") == "0xFF"); // - 1

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b010") == "0x2");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b10") == "0xE");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b0100") == "0x4");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b100") == "0xC");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b1000") == "0x8");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b01000") == "0x08");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b11000") == "0xF8");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b010000") == "0x10");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b10000") == "0xF0");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b0111111111111111111111111111111111111111111111111111111111111111") == "0x7FFFFFFFFFFFFFFF");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b1000000000000000000000000000000000000000000000000000000000000000") == "0x8000000000000000");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b01111111111111111111111111111111111111111111111111111111111111111") == "0x0FFFFFFFFFFFFFFFF");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b10000000000000000000000000000000000000000000000000000000000000000") == "0xF0000000000000000");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b01111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111") == "0x7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0b10000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000") == "0x80000000000000008000000000000000");

            // Decimal to Hex Test
            Debug.Assert(BigNumberCalculator.ToHexOrNull("0") == "0x0");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("1") == "0x1");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-1") == "0xF");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("2") == "0x2");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-2") == "0xE");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("4") == "0x4");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-4") == "0xC");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("8") == "0x08");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-8") == "0x8");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("16") == "0x10");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-16") == "0xF0");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("9223372036854775807") == "0x7FFFFFFFFFFFFFFF");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-9223372036854775808") == "0x8000000000000000");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("18446744073709551615") == "0x0FFFFFFFFFFFFFFFF");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-18446744073709551616") == "0xF0000000000000000");

            Debug.Assert(BigNumberCalculator.ToHexOrNull("170141183460469231731687303715884105727") == "0x7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            Debug.Assert(BigNumberCalculator.ToHexOrNull("-170141183460469231722463931679029329920") == "0x80000000000000008000000000000000");
            #endregion

            #region AddOrNull

            bool bOverflow;
            var calculator = new BigNumberCalculator(8, EMode.Binary);

            Debug.Assert(calculator.AddOrNull(value[1], value[0], out bOverflow) == null);

            calculator = new BigNumberCalculator(8, EMode.Binary);
            Debug.Assert(calculator.AddOrNull("127", "-45", out bOverflow) == "0b01010010" && !bOverflow);
            Debug.Assert(calculator.AddOrNull("0b10000000", "0x6", out bOverflow) == "0b10000110" && !bOverflow);
            Debug.Assert(calculator.AddOrNull("0b01111", "0b11", out bOverflow) == "0b00001110" && !bOverflow);
            Debug.Assert(calculator.AddOrNull("50", "0b0110", out bOverflow) == "0b00111000" && !bOverflow);

            calculator = new BigNumberCalculator(8, EMode.Decimal);
            Debug.Assert(calculator.AddOrNull("39", "88", out bOverflow) == "127" && !bOverflow);
            Debug.Assert(calculator.AddOrNull("0b1", "0b11111111", out bOverflow) == "-2" && !bOverflow);
            Debug.Assert(calculator.AddOrNull("64", "64", out bOverflow) == "-128" && bOverflow);
            Debug.Assert(calculator.AddOrNull("120", "17", out bOverflow) == "-119" && bOverflow);
            Debug.Assert(calculator.AddOrNull("-127", "-2", out bOverflow) == "127" && bOverflow);
            Debug.Assert(calculator.AddOrNull("-1", "0b01", out bOverflow) == "0" && !bOverflow);

            calculator = new BigNumberCalculator(64, EMode.Decimal);
            Debug.Assert(calculator.AddOrNull("0b0110111011011101111011110000101110000010000101100111111011101011", "1234567890123456789", out bOverflow) == "-9223372036854775808" && bOverflow);
            #endregion

            #region SubtractOrNull

            Debug.Assert(calculator.SubtractOrNull(value[1], value[0], out bOverflow) == null);

            calculator = new BigNumberCalculator(8, EMode.Binary);
            Debug.Assert(calculator.SubtractOrNull("25", "52", out bOverflow) == "0b11100101" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("0b100110", "-12", out bOverflow) == "0b11110010" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("0b0001101", "10", out bOverflow) == "0b00000011" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("64", "0x0100", out bOverflow) == null && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("-125", "100", out bOverflow) == "0b00011111" && bOverflow);

            calculator = new BigNumberCalculator(8, EMode.Decimal);
            Debug.Assert(calculator.SubtractOrNull("0b01", "0x1", out bOverflow) == "0" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("0xC0", "0x40", out bOverflow) == "-128" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("39", "-88", out bOverflow) == "127" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("-1", "0x1", out bOverflow) == "-2" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("25", "52", out bOverflow) == "-27" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("0b100110", "-12", out bOverflow) == "-14" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("0b0001101", "10", out bOverflow) == "3" && !bOverflow);
            Debug.Assert(calculator.SubtractOrNull("-125", "100", out bOverflow) == "31" && bOverflow);

            calculator = new BigNumberCalculator(64, EMode.Binary);
            Debug.Assert(calculator.SubtractOrNull("5290939864485030958", "0xC96D31C62E956C2E", out bOverflow) == "0b1000000000000000000000000000000000000000000000000000000000000000" && bOverflow);
            #endregion

        }

        static void AddOrNullTest()
        {
            bool bOverflow = false;

            /********************************* Binary Test ***********************************/

            BigNumberCalculator calc1 = new BigNumberCalculator(8, EMode.Binary);
            // AddOrNull test
            Debug.Assert(calc1.AddOrNull("127", "0", out bOverflow) == "0b01111111");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("-128", "0", out bOverflow) == "0b10000000");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("-64", "-65", out bOverflow) == "0b01111111");
            Debug.Assert(bOverflow);
            Debug.Assert(calc1.AddOrNull("64", "64", out bOverflow) == "0b10000000");
            Debug.Assert(bOverflow);
            Debug.Assert(calc1.AddOrNull("64", "63", out bOverflow) == "0b01111111");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("1", "-1", out bOverflow) == "0b00000000");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("1", "0xFF", out bOverflow) == "0b00000000");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("0b1", "0b11111111", out bOverflow) == "0b11111110");
            Debug.Assert(!bOverflow);

            // SubtractOrNull test
            Debug.Assert(calc1.SubtractOrNull("1", "-1", out bOverflow) == "0b00000010");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.SubtractOrNull("0b11", "0b0001", out bOverflow) == "0b11111110");
            Debug.Assert(!bOverflow);
            // Debug.Assert(calc1.SubtractOrNull("0b10000000", "0b10000000", out bOverflow) == "0b00000000");
            // Debug.Assert(bOverflow);
            /********************************* Binary Test ***********************************/




            /********************************* Decimal Test *********************************/
            // Decimal Calculator test
            calc1 = new BigNumberCalculator(8, EMode.Decimal);

            // AddOrNull test
            Debug.Assert(calc1.AddOrNull("127", "0", out bOverflow) == "127");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("-128", "0", out bOverflow) == "-128");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("-64", "-65", out bOverflow) == "127");
            Debug.Assert(bOverflow);
            Debug.Assert(calc1.AddOrNull("64", "64", out bOverflow) == "-128");
            Debug.Assert(bOverflow);
            Debug.Assert(calc1.AddOrNull("64", "63", out bOverflow) == "127");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("1", "-1", out bOverflow) == "0");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("1", "0xFF", out bOverflow) == "0");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.AddOrNull("0b1", "0b11111111", out bOverflow) == "-2");
            Debug.Assert(!bOverflow);

            // SubtractOrNull test
            Debug.Assert(calc1.SubtractOrNull("0b11", "0b0001", out bOverflow) == "-2");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.SubtractOrNull("1", "-1", out bOverflow) == "2");
            Debug.Assert(!bOverflow);
            Debug.Assert(calc1.SubtractOrNull("0b10000000", "0b10000000", out bOverflow) == "0");
            Debug.Assert(bOverflow);
            /********************************* Decimal Test *********************************/

        }

    }
}
