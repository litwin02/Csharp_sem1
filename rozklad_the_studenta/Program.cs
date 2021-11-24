using System;

namespace rozklad_the_studenta
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

    public class Lecture
    {
        public string Lecture_Name;
        private int Difficulty;
        private string Kolos_Type;
        private void Init(int difficulty = 1)
        {
            this.Difficulty = difficulty;
        }

        public string getKolos() {return this.Kolos_Type;}
        public int getDifficulty() {return this.Difficulty;}
        public Lecture(string lecture)
        {
            Lecture_Name = lecture;
            switch(lecture)
            {
                case "math":
                    this.Kolos_Type="Kolos z matmy";
                    Init(7);
                    break;
                case "eng":
                    this.Kolos_Type="Kolos z angielskiego";
                    Init(3);
                    break;
                case "physics":
                    this.Kolos_Type="Kolos z fizyki";
                    Init(9);
                    break;
                default:
                    Init();
                    break;
            }
        }

    }
    public class Hero
    {
        public string Name;
        private int Strength;
        private int Social_Skills;
        private int Intelligence;
        private int ECTS = 0;
        private double Chance_To_Pass;
        private void Init(int strength = 5, int social_skills = 5, int intelligence = 5)
        {
            this.Strength = strength;
            this.Social_Skills = social_skills;
            this.Intelligence = intelligence;
            this.Chance_To_Pass = (strength + social_skills + intelligence)/3;
        }
        private void UpdateChance() {this.Chance_To_Pass = (this.Intelligence + this.Strength + this.Social_Skills)/3;}
        public void UpIntelligence() {this.Intelligence+=1;}
        public void UpSocialSkills() {this.Social_Skills+=1;}
        public void UpStrength() {this.Strength+=1;}
        public void LevelUp(int choice)
        {
            // 1 social skills 2 intelligence 3 strength
            switch (choice)
            {
                case 1:
                    UpSocialSkills();
                    UpdateChance();
                    break;
                case 2:
                    UpIntelligence();
                    UpdateChance();
                    break;
                case 3:
                    UpStrength();
                    UpdateChance();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór, punkty idą w siłę!");
                    UpStrength();
                    UpdateChance();
                    break;
            }
        }

        public int GetStrength() {return this.Strength;}
        public int GetSocialSkills() {return this.Social_Skills;}
        public int GetIntelligence() {return this.Intelligence;}
        public double GetChance() {return this.Chance_To_Pass;}
        public int GetECTS() {return this.ECTS;}
        public Hero(string name, string myclass)
        {
            // (siła, komunikacja, inteligencja || suma punktów musi być 33)
            Name = name;
            switch (myclass)
            {
                case "INFORMATYKA":
                    Init(7, 9, 17);
                    break;
                case "NAWIGACJA":
                    Init(23, 10, 10);
                    break;
                case "TRANSPORT I LOGISTYKA":
                    Init(11, 20, 12);
                    break;
                default:
                    Init();
                    break;
            }
        }
        public void Kolos(int difficulty_to_pass)
        {
            double chance = GetChance();
            int difficulty = difficulty_to_pass;
            if(chance>=difficulty)
            {
                Console.WriteLine("Gratulacje student. Zdałeś kolosa! Twoje punkty ECTS zwiększają się o 1!");
                this.ECTS++;
            }
            else
                Console.WriteLine("Znowu chlałeś i nie zdałeś baranie.");
        }
    }

    class Program
    {
        static void Log(string wiadomosc) {Console.Write(wiadomosc);}

        static int exp_choice()
        {
            Log("Wybierz co chcesz robić przez ten tydzień:\n");
            Log("1. Piwo z kolegami z roku\n2. Nauka przez weekend\n3. Klata biceps i barki na siłowni\n");
            Log("Twój wybór(wpisz numer 1, 2 albo 3): ");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }
        static void Main(string[] args)
        {
            Log("Witaj w ROZKŁADZIE THE-STUDENTA\n");
            Log("Jakie jest Twoje imie studencie: ");
            string name = Console.ReadLine();

            Log("WYBIERZ SWÓJ KIERUNEK STUDIÓW: (INFORMATYKA, NAWIGACJA, TRANSPORT I LOGIYSTKA): ");
            string char_class = Console.ReadLine();
            char_class = char_class.ToUpper();
            Log("\n");
            Hero hero = new Hero(name, char_class);

            Log($"Witaj {hero.Name}! Twoja wybrana klasa to: {char_class}! Oto jej statystyki:\n");
            Log($"1. Inteligencja: {hero.GetIntelligence()}\n2. Umiejętność współpracy: {hero.GetSocialSkills()}\n3. Siła: {hero.GetStrength()}\n");
            Log("###############\n");
            Log("OTO ZASADY GRY\n");
            Log("###############\n");
            Log("Twoim zadaniem jest zdobyć jak najwięcej punktów ECTS i zaliczyć semestr. Przeszkadzać Ci będą w tym oczywiście zawsze pomocni wykładowcy.\n");
            Log("Pokonuj ich kolosy za pomocą intelektu, współpracuj z innymi debil... studentami oraz trenuj siłę, aby być bardziej odpornym na ich sztuczki.\n");
            Log("Kolosy są co 3 tygodnie. Dodawaj mądzrze swoje punkty umiejętności, aby zdać je pierwszym razem.\n");
            Log($"Gra kończy się gdy zdobędziesz 30 punktów ECTS. Twoja obecna ilość punktów to: {hero.GetECTS()}. Powodzenia student!!!!\n");
            Log("###############################################################################################################################################\n");

            Log("Aby rozpocząć grę wciśnij dowolny klawisz\n");
            Console.ReadLine();

            int game_week = 1;
            int skill_upgrade;
            string kolos_name;
            int kolos_diff;
            Lecture matma = new Lecture("math");

            while(hero.GetECTS()!=30)
            {
                while(game_week%4!=0)
                {
                    Log($"Tydzień {game_week} studiów.\n");
                    skill_upgrade = exp_choice();
                    Log("\n");
                    Log("Twoje statystyki: \n");
                    Log(($"1. Inteligencja: {hero.GetIntelligence()}\n2. Umiejętność współpracy: {hero.GetSocialSkills()}\n3. Siła: {hero.GetStrength()}\n"));
                    game_week++;
                }
                Log("Czas kolosa!\n");
                kolos_name = matma.getKolos();
                kolos_diff = matma.getDifficulty();
                Log($"Twój kolos to {kolos_name}\n");
                hero.Kolos(kolos_diff);
                game_week++;
            }
        }
    }
}
