namespace Lab1
{
    internal class Person
    {
        internal string Id { get; }
        internal Trainer Trainer { get; private set; }

        internal Person(string id)
        {
            Id = id;
        }

        internal void AssignTrainer(Trainer trainer)
        {
            Trainer = trainer;
        }
    }
}
