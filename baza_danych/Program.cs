using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace zajęcia_10._11._2021
{
    class Program
    {
        static void CreateBase(string table_name)
        {
            string connStr = "server=localhost;user=root;database=infa;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = $"CREATE TABLE {table_name}(id BIGINT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,login VARCHAR(32) UNIQUE NOT NULL,email VARCHAR(64) UNIQUE NOT NULL,password VARCHAR(32) NOT NULL,PRIMARY KEY (id))";
                
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Utworzono tabelę o nazwie {table_name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }
        static void AddUser(string table_name, string login, string email, string passwd)
        {
            string connStr = "server=localhost;user=root;database=infa;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = $"INSERT INTO {table_name} (id, login, email, password) VALUES ('null','{login}', '{email}', '{passwd}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Dodano użykowników");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("PANEL ADMINISTRATORA BAZY DANYCH");
                Console.WriteLine("================================");
                Console.WriteLine("Opcje do wybrou: ");
                Console.WriteLine("1. Stwórz bazę danych");
                Console.WriteLine("2. Dodaj użytkowanika do istniejącej bazy danych: ");
                Console.WriteLine("3. Wyjście z programu");
                Console.WriteLine("================================");
                Console.Write("Twój wybór: ");
                int usr_choice = int.Parse(Console.ReadLine());
                switch(usr_choice)
                {
                    case 1:
                        Console.Write("Jak chcesz nazwać bazę danych?: ");
                        string table_name = Console.ReadLine();
                        CreateBase(table_name);
                        break;
                    case 2:
                        Console.WriteLine("Podaj atrybuty dla bazy danych: ");
                        Console.WriteLine("Nazwa bazy danych: ");
                        table_name = Console.ReadLine();

                        Console.WriteLine("login: ");
                        string login = Console.ReadLine();

                        Console.WriteLine("email: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("hasło: ");
                        string passwd = Console.ReadLine();
                        AddUser(table_name, login, email, passwd);
                        break;
                    case 3: break;
                    default:
                        Console.WriteLine("Została podana zła opcja!");
                        break;
                }

                if(usr_choice==3) break;
            }
        }
    }
}