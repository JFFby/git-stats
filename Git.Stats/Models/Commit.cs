namespace Git.Stats.Models
{
    public sealed class Commit
    {
        public Commit(string name, Author author, int insertions, int deletions)
        {
            Name = name;
            Author = author;
            Insertions = insertions;
            Deletions = deletions;
        }

        public string Name { get; }

        public Author Author { get; }

        public int Insertions { get; }

        public int Deletions { get; }
    }
}
