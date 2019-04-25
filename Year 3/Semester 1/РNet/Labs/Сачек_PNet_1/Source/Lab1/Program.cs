using System;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var gym = InitGym();
            var mostPopularTrainers = gym.GetMostPopularTrainers();

            foreach(var trainer in mostPopularTrainers)
            {
                Console.WriteLine("Trainer {0}. Count = {1}. Persons: {2}", 
                    trainer.Id, trainer.Persons.Count, string.Join(", ", trainer.Persons.Select(x => x.Id)));
            }

            Console.ReadLine();
        }

        static Gym InitGym()
        {
            var gym = new Gym();

            var trainer1 = new Trainer("T1");
            var trainer2 = new Trainer("T2");
            var trainer3 = new Trainer("T3");
            var trainer4 = new Trainer("T4");

            gym.TryAddTrainer(trainer1);
            gym.TryAddTrainer(trainer2);
            gym.TryAddTrainer(trainer3);
            gym.TryAddTrainer(trainer4);

            var person1 = new Person("P1");
            var person2 = new Person("P2");
            var person3 = new Person("P3");
            var person4 = new Person("P4");
            var person5 = new Person("P5");

            gym.TryAddPerson(person1, trainer1);
            gym.TryAddPerson(person2, trainer1);
            gym.TryAddPerson(person3, trainer2);
            gym.TryAddPerson(person4, trainer2);
            gym.TryAddPerson(person5, trainer3);

            return gym;
        }
    }
}
