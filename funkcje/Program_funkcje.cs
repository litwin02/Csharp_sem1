using System;

namespace zad_dom_funkcja
{
    class Program
    {
        static void Main(string[] args)
        {  
            Console.Write("Podaj współczynnik kierunkowy a: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Podaj współczynnik b: ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Podaj wyraz wolny c: ");
            double c = double.Parse(Console.ReadLine());

            double x1; double x2;
            if (a!=0)
            {
                double delta = (b*b)-(4*a*c);
                if (delta==0)
                {
                    x1 = -b/2*a;
                    Console.Write("x: {0}", x1);
                }
                else if (delta > 0)
                {
                x1 = (-b - Math.Sqrt(delta))/2*a;
                x2 = (-b + Math.Sqrt(delta))/2*a;
                Console.Write("x1: {0} x2: {1}", x1, x2);
                }
                else
                {
                    Console.WriteLine("Funkcja nie ma miejsc zerowych");
                }
            }
            else
            {
                x1 = -c/b;
                Console.Write("x: {0}", x1);
            }
        }
    }
}
