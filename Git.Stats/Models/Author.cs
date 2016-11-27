using System.Collections.Generic;

namespace Git.Stats.Models
{
    public sealed class Author
    {
        private static  IDictionary<string, int> authors = new Dictionary<string, int>(); 
        private static int id = 0;

        public Author(string name, string email)
        {
            Name = name;
            Email = email;
            Id = GetId(name);
        }

        public string Name { get; }

        public string Email { get; }

        public int Id { get; } 
 
        public override bool Equals(object obj)
        {
            var author = obj as Author;

            return author != null && author.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return string.Format($"Author {Name}").GetHashCode();
        }

        private int GetId(string name)
        {
            if (!authors.ContainsKey(name))
            {
                authors.Add(name, ++id);
            }

            return authors[name];
        }
    }
}
