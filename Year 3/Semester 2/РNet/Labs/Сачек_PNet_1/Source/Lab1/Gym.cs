using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    internal class Gym
    {
        internal ICollection<Trainer> Trainers { get; set; }
        internal ICollection<Person> Persons { get; set; }

        internal Gym()
        {
            Trainers = new List<Trainer>();
            Persons = new List<Person>();
        }

        internal bool TryAddTrainer(Trainer trainer)
        {
            if (Trainers.Contains(trainer))
            {
                return false;
            }

            Trainers.Add(trainer);

            return true;
        }

        internal bool TryAddPerson(Person person, Trainer personTrainer)
        {
            if(Persons.Contains(person))
            {
                return false;
            }

            if(!Trainers.Contains(personTrainer))
            {
                return false;
            }

            person.AssignTrainer(personTrainer);
            Persons.Add(person);
            personTrainer.TryAssignPerson(person);

            return true;
        }

        internal IEnumerable<Trainer> GetMostPopularTrainers()
        {
            return Trainers.Where(trainer => 
                trainer.Persons.Count == Trainers.Max(x => x.Persons.Count));
        }
    }
}
