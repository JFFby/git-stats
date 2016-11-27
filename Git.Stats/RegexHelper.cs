using System.Text.RegularExpressions;
using Git.Stats.Models;

namespace Git.Stats
{
    internal sealed class RegexHelper
    {
        private readonly RegexOptions options = RegexOptions.Compiled
                                                | RegexOptions.IgnoreCase
                                                | RegexOptions.Singleline;
        private readonly Regex commitNameRegex;
        private readonly Regex authorRegex;
        private readonly Regex insertionsRegex;
        private readonly Regex deletionsRegex;

        public RegexHelper()
        {
            this.commitNameRegex = new Regex(@"commit (\w+)", options);
            authorRegex = new Regex(@"Author: (?<name>[\w\s]+)\s<(?<email>.+)>", options);
            insertionsRegex = new Regex(@"(?<insertions>\d+) insertions?", options);
            deletionsRegex = new Regex(@"(?<deletions>\d+) deletions?", options);
        }



        internal string GetName(string input)
        {
            return commitNameRegex.IsMatch(input)
                ? commitNameRegex.Match(input).Groups[1].Value
                : string.Empty;
        }

        internal Author GetAuthor(string input)
        {
            if (authorRegex.IsMatch(input))
            {
                var result = authorRegex.Match(input);
                var name = result.Groups["name"].Value;
                var email = result.Groups["email"].Value;
                return new Author(name, email);
            }

            return null;
        }

        internal int GetInsertions(string input)
        {
            return insertionsRegex.IsMatch(input)
                ? int.Parse(insertionsRegex.Match(input).Groups["insertions"].Value)
                : default(int);
        }

        internal int GetDeletions(string input)
        {
            return deletionsRegex.IsMatch(input)
                ? int.Parse(deletionsRegex.Match(input).Groups["deletions"].Value)
                : default(int);
        }

        internal bool IsContainsLineStatistic(string input)
        {
            return insertionsRegex.IsMatch(input) || deletionsRegex.IsMatch(input);
        }
    }
}
