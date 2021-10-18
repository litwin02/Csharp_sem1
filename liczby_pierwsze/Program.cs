using System;

namespace liczby_pierwsze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj zakres wypisywania liczb pierwszych: ");
            int zakres = int.Parse(Console.ReadLine());
            int pierwiastek = (Int32)Math.Floor(Math.Sqrt(zakres));
            

            for(int i = 2; i <= zakres; i++)
            {
                for(int j = 2; j <= pierwiastek; j++)
                {
                    
                }
            }
        }
    }
}
