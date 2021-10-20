using System;

namespace liczby_pierwsze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj zakres wypisywania liczb pierwszych: ");
            int zakres = int.Parse(Console.ReadLine());
            int start = 2;

            while(start <=zakres)
            {
                int pierwiastek = (int)Math.Floor(Math.Sqrt(start));
                int licznik = 2;
                bool czy_pierwsza = true;

                while(licznik <= pierwiastek)
                {
                    if(start%licznik == 0)
                    {
                        czy_pierwsza = false;
                        break;
                    }
                    licznik++;
                }
                
                if(czy_pierwsza) Console.Write(start + " "); 
            
                start++;
            }
        }
    }
}
