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

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var commit = obj as Commit;
            return commit != null && commit.GetHashCode() == GetHashCode();
        }
    }
}
