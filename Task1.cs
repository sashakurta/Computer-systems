using System;

namespace CSLab2._1
{
    class Program
    {
        static void FirstTask()
        {
            Console.WriteLine("Enter first number");
            Int32 multiplicand = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter second number");
            Int32 multiplier = Convert.ToInt32(Console.ReadLine());
            Int64 product = 0;
            for (int i = 32, n = 0; i > 0; i--, n++)
            {
                Console.WriteLine("------- Step {0} -----", n);
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
                Console.WriteLine(result);

            }


            Console.WriteLine("Result = " + product);


        }
        static void Main(string[] args)
        {
            FirstTask();
        }
    }
}
