using System;
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

            Console.WriteLine("===== FINISHED =====");

        }
    }
}
