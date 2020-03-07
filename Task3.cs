using System;
using System.Collections.Generic;
namespace CSLab2
{
    class Program
    {

        static Int64 Multiplication(int multiplicand, int multiplier)
        {
            Int64 product = 0;
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
        static void ThirdTask()
        {

            Console.WriteLine("First number int");
            Int32 integer1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("First number fraction");
            double fractional1 = Convert.ToDouble(Console.ReadLine());
            //знак
            int sign1 = Sign(integer1);
            //экспонента
            int exp1 = Exponent(integer1);
            //мантисса
            string integ1 = Convert.ToString(integer1, 2);
            string fr1 = Convert.ToString(Fractonal(fractional1));
            string Mantissa1 = "";
            if (fr1.Length > (24 - integ1.Length))
            {
                Mantissa1 = integ1.Substring(1) + fr1.Substring(0, 24 - integ1.Length);
            }
            else
            {
                Mantissa1 = integ1.Substring(1) + fr1 + new string('0', (23 - Mantissa1.Length));
            }

            Console.WriteLine("1, " + Mantissa1);


            Console.WriteLine("Second number int");
            Int32 integer2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Second number fraction");
            double fractional2 = Convert.ToDouble(Console.ReadLine());
            //знак
            int sign2 = Sign(integer2);
            //экспонента
            int exp2 = Exponent(integer2);
            //мантисса
            string integ2 = Convert.ToString(integer2, 2);
            string fr2 = Convert.ToString(Fractonal(fractional2));
            string Mantissa2 = "";
            if (fr2.Length > (24 - integ2.Length))
            {
                Mantissa2 = integ2.Substring(1) + fr2.Substring(0, 24 - integ2.Length);
            }
            else
            {
                Mantissa2 = integ2.Substring(1) + fr2 + new string('0', (23 - Mantissa2.Length));
            }
            Console.WriteLine("1, " + Mantissa2);
            //вычисления знака результата
            int resSign = sign1 ^ sign2;
            Console.WriteLine("Multiply significands {0} = {1}^{2}", resSign, sign1, sign2);
            //вычисления мантиссы результата
            int resMantisa1 = ConvertToBase10('1' + Mantissa1);
            int resMantissa2 = ConvertToBase10('1' + Mantissa2);
            Int64 resMantisa = Multiplication(resMantisa1, resMantissa2);

            Console.WriteLine("Mantissa" + Convert.ToString(resMantisa, 2).Substring(1, 23));
            //вычисление экспоненты
            int resexp = exp1 + exp2 - 127;
            Console.WriteLine("Exponenta" + resexp);

            static int ConvertToBase10(string Mantissa)
            {
                int number = 0;
                for (int i = 1, n = Mantissa.Length; n != 0; n--, i *= 2)
                {
                    if (Mantissa[n - 1] == '1')
                    {
                        number += i;
                    }
                }
                return number;
            }
            static int Sign(int A)
            {
                int sign;
                if (A > 0)
                {
                    sign = 0;
                }
                else
                {
                    sign = 1;
                }
                return sign;
            }
            static int Exponent(int A)
            {
                string str = Convert.ToString(A, 2);
                return 127 + (str.Length - 1);
            }
            static string Fractonal(double B)
            {
                string str = "";
                while (B != 1)
                {
                    B *= 2;
                    if (B > 1)
                    {
                        B -= 1;
                        str += "1";
                    }
                    else if (B < 1)
                    {
                        str += "0";
                    }

                }
                str += "1";

                return str;
            }
        }

        static void Main()
        {
            ThirdTask();


        }
    }
}
