using System;

namespace liczby_pierwsze_tablice
{
    class Program
    {
        static void Main(string[] args)
        {   
            int n = 0;
            while(true){
                Console.Write("Podaj ile liczb pierwszych chcesz wypisać: ");
                n = int.Parse(Console.ReadLine());

                if(n>1) break;
                else Console.WriteLine("Najmniejsza liczba pierwsza to 2!");
            }
            // n+1 wynika z tego, że tworząc tablice tylko z n to indeksy tablicy są od 0 do n-1 co potem daje błąd w następnej pętli
            bool[] arr = new bool[n+1];

            for(int i = 2; i <= n; i++) arr[i] = true;

            int pierwiastek = (int) Math.Floor(Math.Sqrt(n));

            for (int j = 2; j <= pierwiastek; j++)
            {
                if (arr[j] == true)
                {
                    for(int g = j*j; g <= n; g+=j) arr[g] = false;
                }
            }
            
            for(int i = 2; i <= n; i++)
            {
                if(arr[i]==true) Console.Write(i + " ");
            }
        }
    }
}
