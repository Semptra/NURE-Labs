using System;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static GameDataDbEntities context = new GameDataDbEntities();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select an action:");
                Console.WriteLine("1. Show users statistics");
                string ans = Console.ReadLine();

                switch (ans)
                {
                    case "1":
                        ShowUsersStatistics();
                        break;
                    case "2":
                        ShowStageStatistics();
                        break;
                    default:
                        break;
                }
            }
        }

        static void ShowUsersStatistics()
        {
            Console.Clear();
            Console.WriteLine("Users statistics");
            Console.WriteLine("Total users: {0}", context.Users.Count());
            Console.WriteLine("Male users: {0}", context.Users.Count(x => x.Gender == "Male"));
            Console.WriteLine("Female users: {0}", context.Users.Count(x => x.Gender == "Female"));
            Console.WriteLine("Average age: {0}", context.Users.Average(x => x.Age));
            Console.WriteLine("Top 10 countries by users");

            var countiesStatistics = context.Users
                .GroupBy(x => x.Country)
                .OrderByDescending(x => x.Count())
                .Take(10);

            foreach (var userCountry in countiesStatistics)
            {
                Console.WriteLine(userCountry.Key + "\t\t" + userCountry.Count());
            }

            Console.WriteLine("Press Enter to continue . . .");
            Console.ReadLine();
        }

        static void ShowStageStatistics()
        {
            Console.Clear();
            Console.WriteLine("Stage statistics");
            Console.WriteLine("Total users: {0}", context.Users.Count());
            Console.WriteLine("Male users: {0}", context.Users.Count(x => x.Gender == "Male"));
            Console.WriteLine("Female users: {0}", context.Users.Count(x => x.Gender == "Female"));
            Console.WriteLine("Average age: {0}", context.Users.Average(x => x.Age));
            Console.WriteLine("Top 10 countries by users");

            var countiesStatistics = context.Users
                .GroupBy(x => x.Country)
                .OrderByDescending(x => x.Count())
                .Take(10);

            foreach (var userCountry in countiesStatistics)
            {
                Console.WriteLine(userCountry.Key + "\t\t" + userCountry.Count());
            }

            Console.WriteLine("Press Enter to continue . . .");
            Console.ReadLine();
        }
    }
}
