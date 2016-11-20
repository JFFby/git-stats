namespace Git.Stats.Models
{
    public sealed class Author
    {
        public Author(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }

        public string Email { get; }

        public override bool Equals(object obj)
        {
            var author = obj as Author;

            return author != null && author.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return string.Format($"Author {Name}").GetHashCode();
        }
    }
}
