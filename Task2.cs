using System;

namespace CSLab2._2
{
    class Program
    {

        static void SecondTask()
        {
            int A;
            int B;
            int k;
            int remainder;
            string quotient = "";

            Console.WriteLine("First number");
            A = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Second number");
            B = Convert.ToInt16(Console.ReadLine());
            if (A == B) { Console.WriteLine("1"); }
            else
            {
                k = SignificantUnit(A) - SignificantUnit(B);
                B <<= k;

                int NotB = (~B);
                NotB++;
                remainder = A + NotB;
                if (remainder > 0)
                {
                    quotient += "1";
                }
                if (remainder < 0)
                {
                    quotient += "0";
                }
                for (int i = 0; i < k; i++)
                {
                    Console.WriteLine("----- Step {0} -----", i);
                    Console.WriteLine("Remainder  " + remainder);
                    remainder <<= 1;


                    if (remainder > 0)
                    {

                        remainder += NotB;
                        if (remainder > 0)
                        {
                            quotient += "1";
                        }
                        if (remainder < 0)
                        {
                            quotient += "0";
                        }
                    }
                    else if (remainder < 0)
                    {

                        remainder += B;
                        if (remainder > 0)
                        {
                            quotient += "1";
                        }
                        if (remainder < 0)
                        {
                            quotient += "0";
                        }
                    }
                    if (remainder == 0)
                    {
                        quotient += "1";
                        remainder += NotB;
                    }
                    Console.WriteLine("Quotient " + quotient);
                }
                if (remainder < 0)
                {
                    remainder += B;
                }
                remainder >>= k;
                if (remainder == 0 && (quotient == "1" || quotient == "10" || quotient == "100" || quotient == "1000")) { Console.WriteLine("Result " + quotient + "0"); }
                else { Console.WriteLine("Result " + quotient + "   " + Convert.ToString(remainder, 2)); }
            }
        }
        static int SignificantUnit(int number)
        {
            int sig = 0;
            for (int i = 32768; i > 0; i /= 2)
            {

                if ((number & i) != 0)
                {

                    sig = (number & i);
                    break;

                }
            }

            int count = 0;
            for (int i = sig; i > 0; i /= 2)
            {
                count++;
            }

            return count;
        }
        static void Main(string[] args)
        {
            SecondTask();
        }
    }
}
