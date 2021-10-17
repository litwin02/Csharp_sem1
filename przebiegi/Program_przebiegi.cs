using System;

namespace kodowanie_pierwsze
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // przebieg C
            double y;
            Console.Write("Podaj wartość x dla przebiegu C: ");
            double x = double.Parse(Console.ReadLine());

            if(x < -4 || x > 2)
            {
                y = ((-1.0/3.0) * x) + (2.0/3.0);
                Console.WriteLine(y);
            }
            else
            {
                y = (1.0/4.0)*(x*x) + (1.0/4.0)*x - (3.0/2.0);
                Console.WriteLine(y);
            }
            
            // przebieg D

            Console.Write("Podaj wartość x dla przebiegu D: ");
            x = double.Parse(Console.ReadLine());

            if (x >= -6 && x < -2)
            {
                y = (-3.0/2.0)*x-6.0;
                Console.Write(y);
            }
            else if(x >= -2 && x < 2)
            {
                y = (-3.0/2.0)*x;
                Console.Write(y);
            }
            else if(x >= 2 && x < 6)
            {
                y = (-3.0/2.0)*x+6.0;
                Console.Write(y);
            }
        }
    }
}
