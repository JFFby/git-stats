using System.Collections.Generic;

namespace Git.Stats.Models.Statistics
{
    public sealed class Statistic
    {
        public int TotalInserions { get; set; }

        public int TotalDeletions { get; set; }

        public IList<Commit> Commits { get; set; } 

        public IList<AuthorStatistic> AuhorStatistic { get; set; } 
    }
}
