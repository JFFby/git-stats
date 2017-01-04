using System;
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
        private readonly Regex dateRegex;

        public RegexHelper()
        {
            this.commitNameRegex = new Regex(@"commit (\w+)", options);
            authorRegex = new Regex(@"Author: (?<name>.+)\s<(?<email>.+)>", options);
            insertionsRegex = new Regex(@"(?<insertions>\d+) insertions?", options);
            deletionsRegex = new Regex(@"(?<deletions>\d+) deletions?", options);
            dateRegex = new Regex(@"Date:\s+(?<dayOfWeek>\w+)\s(?<month>\w+)\s(?<day>\d+)\s(?<time>\d+:\d+:\d+)\s(?<year>\d+)", options);
        }

        internal DateTime GetDate(string input)
        {
            if (dateRegex.IsMatch(input))
            {
                var result = dateRegex.Match(input);
                var month = result.Groups["month"].Value;
                var year = result.Groups["year"].Value;
                var day = result.Groups["day"].Value;
                var time = result.Groups["time"].Value;

                return DateTime.Parse($"{month} {day} {year} {time}");
            }

            throw new ArgumentException();
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
