using System;

namespace data
{
    class Program
    {
        static void Main(string[] args)
        {   
            string date_arg = args[0];
            string time = args[1];
            var date = DateTime.Parse(date_arg+" "+time);
            
            for(int i = 2; i < args.Length; i++)
            {   
                if(args[i].Contains('s'))
                {
                    args[i] = args[i].TrimEnd('s');
                    int s = int.Parse(args[i]);
                    date = date.AddSeconds(s);
                }
                else if(args[i].Contains('o'))
                {
                    args[i] = args[i].TrimEnd('o');
                    args[i] = args[i].TrimEnd('m');
                    int mo = int.Parse(args[i]);
                    date = date.AddMonths(mo);
                }
                else if(args[i].Contains('m'))
                {
                    args[i] = args[i].TrimEnd('m');
                    int m = int.Parse(args[i]);
                    date = date.AddMinutes(m);
                }
                else if(args[i].Contains('h'))
                {
                    args[i] = args[i].TrimEnd('h');
                    int h = int.Parse(args[i]);
                    date = date.AddHours(h);
                }
                else if(args[i].Contains('d'))
                {
                    args[i] = args[i].TrimEnd('d');
                    int d = int.Parse(args[i]);
                    date = date.AddDays(d);
                }
                else if(args[i].Contains('y'))
                {
                    args[i] = args[i].TrimEnd('y');
                    int y = int.Parse(args[i]);
                    date = date.AddYears(y);
                }
            }
            Console.WriteLine(date.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}