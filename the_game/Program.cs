using System;
using System.IO;
using System.Data.SQLite;

namespace project
{
    public class Rand
    {
        public int Run(int min, int max)
        {
            int range = (max - min) + 1;
            Random rng = new Random();
            return min + rng.Next() % range;
        }
    }
    public class Hero
    {
        public string Name;
        public int Strength;
        public int Dexterity;
        public int Intelligence;
        public double HP;
        public double MP;
        public double Shield;
        public void Init(int strength = 10, int dexterity = 10, int intelligence = 10)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Intelligence = intelligence;
            HP = 50 + strength;
            MP = 2 * intelligence;
            Shield = 0;
        }
        public void UpStrength()
        {
            Strength += 5;
            HP += 5;
        }
        public void UpDexterity()
        {
            Dexterity += 5;
        }
        public void UpIntelligence()
        {
            Intelligence += 5;
            MP += (3 * Intelligence);
        }
        public Hero(string name="", string hero_class="")
        {
            Starter(name, hero_class);
        }
        public void Attack(Enemy enemy)
        {
            Rand rand = new Rand();
            double damage = Strength * rand.Run(5, 10) / 10;
            double enemyDamage = enemy.Strength * rand.Run(5, 10) / 10;

            if (rand.Run(0, 100)*Dexterity/15 > enemy.Dexterity)
            {
                Console.WriteLine("You are attacking first!");
                Console.WriteLine($"Bang! Damage given to the enemy: {damage}");
                if(enemy.HP-damage>0)
                {
                    enemy.LowerHP(damage);
                    Console.WriteLine("Weakned enemy tried to defend himself and managed to attack you!");
                    Console.WriteLine($"Bang! Damage given to the you: {Math.Abs(enemyDamage-15)}");
                    ReduceAfterDamage(Math.Abs(enemyDamage-15));
                }
                else
                {
                    enemy.LowerHP(damage);
                    Console.WriteLine("AND YOU'VE KILLED HIM!");
                }
            }
            else
            {
                Console.WriteLine("Bad news! You've been attacked first");
                Console.WriteLine($"Bang! Damage given to the you: {enemyDamage}");
                if(HP-enemyDamage>0)
                {
                    ReduceAfterDamage(enemyDamage);
                }
                else
                {
                    Console.WriteLine("AND YOU'VE BEEN KILLED... GOODBYE");
                    HP=-enemyDamage;
                }
            }
        }
        public void LevelUp()
        {
            Console.Write("1:Strength, 2:Dexterity, 3:Intelligence: ");
            int opt = int.Parse(Console.ReadLine());

            switch (opt)
            {
                case 1:
                    UpStrength();
                    break;
                case 2:
                    UpDexterity();
                    break;
                case 3:
                    UpIntelligence();
                    break;
            }
            Console.WriteLine();
        }
        public void ReduceAfterDamage(double damage)
        {
            if(Shield-damage>=0)
                Shield-=damage;
            else
                HP-=damage;
        }
        public void Spell()
        {
            Console.WriteLine("Spells:   1.Shield   2.Feel Good Inc.   3.Laser spear");
            Console.WriteLine("Cost of one spell: 10MP");
            Console.Write("Your choice: ");
            int choice_spell = int.Parse(Console.ReadLine());
            if(MP>0)
            {
            switch(choice_spell)
            {
                case 1:
                    Shield += 15;
                    Console.WriteLine("\nAdded additional shield!");
                    MP -= 10;
                    break;
                case 2:
                    HP += 10;
                    Console.WriteLine("\nAdded health!");
                    MP -= 10;
                    break;
                case 3:
                    this.Strength += 10;
                    Console.WriteLine("\nYou found a laser spear left by aliens. Now you're stronger");
                    MP -= 10;
                    break;
            }
            }
            else
                Console.WriteLine("Not enough MP!");
        }
        public void regeneration() {HP += 3; MP+=1;}
        public bool IF_HP_NOT_ZERO()
        {
            if(HP<=0)
                return false;
            else
                return true;
        }
        public Hero LoadHero(string database_name)
        {
            string select = "SELECT * from Player";
            string name=""; decimal HP=0; decimal MP=0; decimal Shield=0; Int64 Strength=0; Int64 Dexterity=0; Int64 Intelligence=0;
            using(var connection = new SQLiteConnection($"Data Source={database_name};"))
            {
            connection.Open();
            var command = new SQLiteCommand(select, connection);
            SQLiteDataReader data = null;
            data = command.ExecuteReader();
            while(data.Read())
            {
                name = data["Name"].ToString();
                HP = (decimal)data["HP"];
                MP = (decimal)data["MP"];
                Shield = (decimal)data["Shield"];
                Strength = (Int64)data["Strength"];
                Dexterity = (Int64)data["Strength"];
                Intelligence = (Int64)data["Strength"];
            }
            connection.Close();
            }
            Hero hero = new Hero();
            hero.Name = name;
            hero.HP = decimal.ToDouble(HP);
            hero.MP = decimal.ToDouble(MP);
            hero.Shield = decimal.ToDouble(Shield);
            hero.Strength = Convert.ToInt32(Strength);
            hero.Dexterity = Convert.ToInt32(Dexterity);
            hero.Intelligence = Convert.ToInt32(Intelligence);
            return hero;
        }
        public void NewGameHero(string name, string hero_class)
        {
            Starter(name, hero_class);
        }
        public void Starter(string name, string hero_class)
        {
            Name = name;
            switch (hero_class)
            {
                case "warior":
                    Init(15, 10, 5);
                    break;
                case "assassin":
                    Init(5, 15, 10);
                    break;
                case "sorcerer":
                    Init(5, 5, 20);
                    break;
                default:
                    Init();
                    break;
            }
        }
    }
    public class Enemy
    {   public string Name;
        public double HP;
        public int Strength;
        public int Dexterity;
        public int Intelligence;        
        public void LowerHP(double damage) {HP -= damage;}
        private void Init(int strength = 10, int dexterity = 10, int intelligence = 10)
        {
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            HP = 50 + strength;
        }
        public bool ENEMY_HP_NOT_ZERO()
        {
            if(HP>0)
                return true;
            else
                return false;
        }
        public Enemy(string enemytype="")
        {
            switch(enemytype)
            {
                case "Eto":
                    Name = "Eto";
                    Init(5, 15, 5);
                    break;
                case "Ibra":
                    Name = "Ibra";
                    Init(12, 6, 7);
                    break;
                case "Marik1234":
                    Name = "Marik1234";
                    Init(4, 20, 1);
                    break;
                case "Ozil":
                    Name = "Ozil";
                    Init(8, 9, 8);
                    break;
            }
        }
        public Enemy LoadEnemy(string database_name, string EnemyName)
        {
            string select = $"SELECT * from Enemies WHERE Name='{EnemyName}'";
            string name=""; decimal HP=0; Int64 Strength=0; Int64 Dexterity=0; Int64 Intelligence=0;
            using(var connection = new SQLiteConnection($"Data Source={database_name};"))
            {
            connection.Open();
            var command = new SQLiteCommand(select, connection);
            SQLiteDataReader data = null;
            data = command.ExecuteReader();
            while(data.Read())
            {
                name = data["Name"].ToString();
                HP = (decimal)data["HP"];
                Strength = (Int64)data["Strength"];
                Dexterity = (Int64)data["Dexterity"];
                Intelligence = (Int64)data["Intelligence"];
            }
            connection.Close();
            }
            Enemy enemy = new Enemy();
            enemy.Name = name;
            enemy.HP = decimal.ToDouble(HP);
            enemy.Strength = Convert.ToInt32(Strength);
            enemy.Dexterity = Convert.ToInt32(Dexterity);
            enemy.Intelligence = Convert.ToInt32(Intelligence);
            return enemy;
        }
    }
    public class FileHandling
    {
        public string filepath;
        public string dir = "./saves";
        public bool IF_FILE_EXIST(string filename)
        {
            filename = $"./saves/{filename}.db";
            if(File.Exists(filename))
                return true;
            else 
                return false;
        }
        public string GET_SAVE_DIRECTORY() {return dir;}
        public bool IF_DIRECTORY_EXIST()
        {
            if(Directory.Exists(dir))
                return true;
            else
                return false;
        }
        public void CREATE_DIRECTORY()
        {
            if(!IF_DIRECTORY_EXIST())
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(dir);
                    Console.WriteLine("The directory for saves was created successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            } 
        }
        public void ListFiles()
        {
            string [] fileEntries = Directory.GetFiles(dir);
            Console.WriteLine("Found saves:");
            foreach(string fileName in fileEntries)
                Console.WriteLine(fileName);
        }    
    }
    public class DatabaseHandling
    {
        string database_name="";
        public void ConnectAndExecute(string sql_command)
        {
            using(var connection = new SQLiteConnection($"Data Source={database_name};"))
            {
            connection.Open();
            var command = new SQLiteCommand(sql_command, connection);
            command.ExecuteNonQuery();
            connection.Close();
            }
        }
        public void CreateNewDatabase(string name)
        {
            var files = new FileHandling();
            if(files.IF_DIRECTORY_EXIST())
            {
                database_name = $"{files.GET_SAVE_DIRECTORY()}/{name}.db";
                SQLiteConnection.CreateFile(database_name);
            }
            else
            {
                files.CREATE_DIRECTORY();
                database_name = $"{files.GET_SAVE_DIRECTORY()}/{name}.db";
                SQLiteConnection.CreateFile(database_name);
            }
        }
        public void CreatePlayerTable()
        {
            string create_new_table = "CREATE TABLE Player(ID	INTEGER NOT NULL UNIQUE, Name	TEXT NOT NULL, HP	NUMERIC NOT NULL, MP	NUMERIC NOT NULL,Shield	NUMERIC NOT NULL, Strength	INTEGER NOT NULL, Dexterity	INTEGER NOT NULL, Intelligence	INTEGER NOT NULL, PRIMARY KEY(ID AUTOINCREMENT))";
            ConnectAndExecute(create_new_table);
        }
        public void CreateEnemyTable()
        {
            string create_new_table = "CREATE TABLE Enemies (ID	INTEGER NOT NULL UNIQUE, Name TEXT NOT NULL, HP	NUMERIC NOT NULL, Strength	INTEGER NOT NULL, Dexterity	INTEGER NOT NULL, Intelligence INTEGER NOT NULL, PRIMARY KEY(ID AUTOINCREMENT))";
            ConnectAndExecute(create_new_table);
        }
        public void InsertNewData(Hero hero, Enemy en1, Enemy en2, Enemy en3, Enemy en4)
        {
            string player_data = $"INSERT INTO Player (Name, HP, MP, Shield, Strength, Dexterity, Intelligence) VALUES('{hero.Name}', '{hero.HP}', '{hero.MP}', '{hero.Shield}', '{hero.Strength}', '{hero.Dexterity}', '{hero.Intelligence}')";
            ConnectAndExecute(player_data);
            string enemy_data = $"INSERT INTO Enemies (Name, HP, Strength, Dexterity, Intelligence) VALUES('{en1.Name}', '{en1.HP}', '{en1.Strength}', '{en1.Dexterity}', '{en1.Intelligence}')";
            ConnectAndExecute(enemy_data);
            enemy_data = $"INSERT INTO Enemies (Name, HP, Strength, Dexterity, Intelligence) VALUES('{en2.Name}', '{en2.HP}', '{en2.Strength}', '{en2.Dexterity}', '{en2.Intelligence}')";
            ConnectAndExecute(enemy_data);
            enemy_data = $"INSERT INTO Enemies (Name, HP, Strength, Dexterity, Intelligence) VALUES('{en3.Name}', '{en3.HP}', '{en3.Strength}', '{en3.Dexterity}', '{en3.Intelligence}')";
            ConnectAndExecute(enemy_data);
            enemy_data = $"INSERT INTO Enemies (Name, HP, Strength, Dexterity, Intelligence) VALUES('{en4.Name}', '{en4.HP}', '{en4.Strength}', '{en4.Dexterity}', '{en4.Intelligence}')";
            ConnectAndExecute(enemy_data);
        }      
        public void UpdateData(Hero hero, Enemy en1, Enemy en2, Enemy en3, Enemy en4)
        {
            string player_data = $"UPDATE Player SET Name = '{hero.Name}', HP = '{hero.HP}', MP = '{hero.MP}', Shield = '{hero.Shield}', Strength = '{hero.Strength}', Dexterity = '{hero.Dexterity}', Intelligence = '{hero.Intelligence}' WHERE ID = 1;";
            ConnectAndExecute(player_data);
            string enemy_data = $"UPDATE Enemies SET Name = '{en1.Name}', HP = '{en1.HP}', Strength = '{en1.Strength}', Dexterity = '{en1.Dexterity}', Intelligence = '{en1.Intelligence}' WHERE ID = 1;";
            ConnectAndExecute(enemy_data);
            enemy_data = $"UPDATE Enemies SET Name = '{en2.Name}', HP = '{en2.HP}', Strength = '{en2.Strength}', Dexterity = '{en2.Dexterity}', Intelligence = '{en2.Intelligence}' WHERE ID = 2;";
            ConnectAndExecute(enemy_data);
            enemy_data = $"UPDATE Enemies SET Name = '{en3.Name}', HP = '{en3.HP}', Strength = '{en3.Strength}', Dexterity = '{en3.Dexterity}', Intelligence = '{en3.Intelligence}' WHERE ID = 3;";
            ConnectAndExecute(enemy_data);
            enemy_data = $"UPDATE Enemies SET Name = '{en4.Name}', HP = '{en4.HP}', Strength = '{en4.Strength}', Dexterity = '{en4.Dexterity}', Intelligence = '{en4.Intelligence}' WHERE ID = 4;";
            ConnectAndExecute(enemy_data);
        }
        
        public string GetNameOfFile(string name)
        {
            var files = new FileHandling();
            if(files.IF_DIRECTORY_EXIST())
            {
                database_name = $"{files.GET_SAVE_DIRECTORY()}/{name}.db";
            }
            return database_name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = new DatabaseHandling();
            Hero hero = new Hero();
            Enemy Eto = new Enemy("Eto");
            Enemy Ibra = new Enemy("Ibra");
            Enemy Marik1234 = new Enemy("Marik1234");
            Enemy Ozil = new Enemy("Ozil");
            FileHandling files = new FileHandling();
            files.CREATE_DIRECTORY();
            bool END_LOOP = false;
           
            while(!END_LOOP)
            {
            Console.WriteLine("Welcome to Heroes of Desperados!");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Load Game");
            Console.Write("Your choice: ");
            int menu_choice = int.Parse(Console.ReadLine());
            string name_for_hero="";
            string class_of_hero="";
            switch(menu_choice)
            {
                case 1:
                    Console.Write("Enter name for your hero: ");
                    name_for_hero = Console.ReadLine();
                    Console.WriteLine("\nChoose the class of your character: Warior, Assassin, Sorcerer");
                    Console.Write("Your choice (type name not number): ");
                    class_of_hero = Console.ReadLine();
                    hero.NewGameHero(name_for_hero, class_of_hero);
                    db.CreateNewDatabase(hero.Name);
                    db.CreatePlayerTable();
                    db.CreateEnemyTable();
                    END_LOOP = true;
                    db.InsertNewData(hero, Eto, Ibra, Marik1234, Ozil);
                    break;
                case 2:
                    files.ListFiles();
                    Console.Write("Type your save name:");
                    string save_name = Console.ReadLine();
                    if(files.IF_FILE_EXIST(save_name))
                    {
                        hero = hero.LoadHero(db.GetNameOfFile(save_name));
                        Eto = Eto.LoadEnemy(db.GetNameOfFile(save_name), "Eto");
                        Ibra = Ibra.LoadEnemy(db.GetNameOfFile(save_name), "Ibra");
                        Marik1234 = Marik1234.LoadEnemy(db.GetNameOfFile(save_name), "Marik1234");
                        Ozil = Ozil.LoadEnemy(db.GetNameOfFile(save_name), "Ozil");
                        END_LOOP = true;
                    }
                    else
                        Console.WriteLine("Invalid name! ");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            }//end of while loop
            Console.WriteLine();
            int tour = 1;
            while (hero.IF_HP_NOT_ZERO())
            {
                Console.WriteLine("\n" + hero.Name + $": Strength:{hero.Strength} Dexterity:{hero.Dexterity} Intelligence:{hero.Intelligence} HP:{hero.HP} MP:{hero.MP} Shield:{hero.Shield} ");

                Console.WriteLine("1: Attack, 2: Spell, 3: LevelUp 4. Save&Exit");
                Console.Write("Your choice(write a number): ");
                int opt = int.Parse(Console.ReadLine());
                Console.WriteLine();
                if(tour%3==0)
                {
                    if(opt != 1)
                    {
                        Console.WriteLine("You must attack every third round!");
                        opt = 1;
                    }
                }
                switch(opt)
                {
                    case 1:
                    if(Eto.ENEMY_HP_NOT_ZERO())
                        hero.Attack(Eto);
                    else if(Ibra.ENEMY_HP_NOT_ZERO())
                        hero.Attack(Ibra);
                    else if(Marik1234.ENEMY_HP_NOT_ZERO())
                        hero.Attack(Marik1234);
                    else if(Ozil.ENEMY_HP_NOT_ZERO())
                        hero.Attack(Ozil);
                        break;
                    case 2:
                        hero.Spell();
                        break;
                    case 3:
                        hero.LevelUp();
                        break;
                    case 4:
                        db.UpdateData(hero, Eto, Ibra, Marik1234, Ozil);
                        hero.HP = -100000000;
                        break;
                }

                Console.WriteLine();
                hero.regeneration();         
                tour++;
                
            }                       
        }
    }
}