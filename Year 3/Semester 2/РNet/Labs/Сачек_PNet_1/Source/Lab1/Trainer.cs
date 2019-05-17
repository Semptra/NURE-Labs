using System.Collections.Generic;

namespace Lab1
{
    internal class Trainer
    {
        internal string Id { get; }
        internal ICollection<Person> Persons { get; private set; }

        internal Trainer(string id)
        {
            Id = id;
            Persons = new List<Person>();
        }

        internal bool TryAssignPerson(Person person)
        {
            if (Persons.Contains(person))
            {
                return false;
            }

            Persons.Add(person);

            return true;
        }
    }
}
