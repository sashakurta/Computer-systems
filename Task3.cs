using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Part3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter A number:");
            float multiplicandF = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter B number:");
            float multiplierF = float.Parse(Console.ReadLine());
            Console.WriteLine("Result is: {0}", IEEE.MultiplyIEEE(multiplicandF, multiplierF));
            Console.ReadKey();
        }
    }
    public class IEEE
    {
        public static float MultiplyIEEE(float multiplicand, float multiplier)
        {
            byte[] multiplicandBit = BitConverter.GetBytes(multiplicand);
            byte[] multiplierBit = BitConverter.GetBytes(multiplier);
            int mantissa1, mantissa2;
            int sign1, sign2;
            int exp1, exp2;
            int res1 = BitConverter.ToInt32(multiplicandBit, 0);

            int res2 = BitConverter.ToInt32(multiplierBit, 0);
            const int bias = 127;

            sign1 = res1 & Convert.ToInt32("10000000000000000000000000000000", 2);
            sign2 = res2 & Convert.ToInt32("10000000000000000000000000000000", 2);
            exp1 = res1 & Convert.ToInt32("01111111100000000000000000000000", 2);
            exp1 >>= 23;
            exp2 = res2 & Convert.ToInt32("01111111100000000000000000000000", 2);
            exp2 >>= 23;


            mantissa1 = res1 & Convert.ToInt32("00000000011111111111111111111111", 2) | Convert.ToInt32("00000000100000000000000000000000", 2);

            mantissa2 = res2 & Convert.ToInt32("00000000011111111111111111111111", 2) | Convert.ToInt32("00000000100000000000000000000000", 2);

            if (multiplicand == 0 || multiplier == 0)
            {
                return 0f;
            }

            Console.WriteLine("Вхідні значення:");
            Console.WriteLine("Множене : Significand = " + Convert.ToString(sign1, 2) + " Exponent = " + Convert.ToString(exp1, 2) + " Mantissa = " + Convert.ToString(mantissa1, 2));
            Console.WriteLine("Множник : Significand = " + Convert.ToString(sign2, 2) + " Exponent = " + Convert.ToString(exp2, 2) + " Mantissa = " + Convert.ToString(mantissa2, 2));

            Console.WriteLine("COMPUTE EXPONENTS:");
            int exponent = exp1 + exp2 - bias;
            Console.WriteLine(exp1 + " + " + exp2 + " - 127 = " + Convert.ToString(exponent, 2) + "(2)\n");

            Console.WriteLine("MULTIPLY SIGNIFICANDS:");
            int significand = sign1 ^ sign2;
            Console.WriteLine(Convert.ToString(sign1, 2) + " ^ " + Convert.ToString(sign2, 2) + " = " + Convert.ToString(significand, 2) + "\n");

            Console.WriteLine("NORMALIZE RESULT:");
            long mantisaLong = ShiftRightForIEEE(mantissa1, mantissa2);
            int mantisa = 0;
            Console.WriteLine("Mantissa = " + Convert.ToString(mantisaLong, 2));
            if ((mantisaLong & 0x800000000000) == 0x800000000000)//чи є 47 біт "1"
            {
                Console.WriteLine("Exponent = " + exponent + " +1");
                exponent++;
            }
            else
                mantisaLong <<= 1;

            for (int i = 0; i < 24; i++)
            {
                if ((mantisaLong & 0x1000000) == 0x1000000)
                {
                    mantisa |= 0x800000;
                }
                if (i == 23)
                    break;
                mantisa >>= 1;
                mantisaLong >>= 1;
            }
            mantisa &= ~(1 << 23);
            Console.WriteLine("Final mantissa = " + "1." + Convert.ToString(mantisa, 2) + "\n");
            Console.WriteLine("RESULT:");
            Console.WriteLine(Convert.ToString(significand, 2) + " " + Convert.ToString(exponent, 2) + " " + Convert.ToString(mantisa, 2));
            int res = significand | (exponent << 23) | mantisa;
            byte[] b = BitConverter.GetBytes(res);
            return BitConverter.ToSingle(b, 0);
        }
        public static long ShiftRightForIEEE(int multiplicand, int multiplier)
        {
            long product = 0;
            for (int i = 32, n = 0; i > 0; i--, n++)
            {

                if ((multiplier & 1) == 1)
                {

                    product += (long)multiplicand << 32;
                    product >>= 1;

                }
                else
                    product >>= 1;
                multiplier >>= 1;
                String result = Convert.ToString(product, 2);
                result = new string('0', 64 - result.Length) + result;
            }
            return product;
        }
    }
}
