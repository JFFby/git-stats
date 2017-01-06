using System;
using System.Collections.Generic;
using Git.Stats.Models;

namespace Git.Stats.Infrastructure.Services.Arguments
{
    public sealed class GroupingArgs
    {
        public GroupingArgs(IList<Commit> commits, string method, DateTime from, DateTime to)
        {
            Commits = commits;
            Method = method.ToUpper();
            From = from;
            To = to;
        }

        public DateTime From { get; }

        public DateTime To { get; }

        public IList<Commit> Commits { get; }

        public string Method { get; }
    }
}
