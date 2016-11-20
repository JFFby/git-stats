namespace Git.Stats.Models.Statistics
{
    public class AuthorStatistic
    {
        public AuthorStatistic(Author author, int commits, int totalInsertions, int totalDeletions)
        {
            Author = author;
            TotalInsertions = totalInsertions;
            TotalDeletions = totalDeletions;
            Commits = commits;
        }

        public Author Author { get; }

        public int TotalInsertions { get; }

        public int TotalDeletions { get; }

        public int Commits { get; }
    }
}
