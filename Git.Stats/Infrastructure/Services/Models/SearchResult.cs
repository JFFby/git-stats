using System.Collections.Generic;
using System.Management.Automation.Language;
using Git.Stats.Models;

namespace Git.Stats.Infrastructure.Services.Models
{
    internal sealed class SearchResult
    {
        internal static SearchResult Error(string message)
        {
            return new  SearchResult(null, message);
        }

        internal static SearchResult Success(IList<Commit> commits)
        {
            return new SearchResult(commits, null);
        }

        private SearchResult(IList<Commit> commtis, string errorMessage)
        {
            Commtis = commtis;
            ErrorMessage = errorMessage;
        }

        internal IList<Commit> Commtis { get; }

        internal string ErrorMessage { get; } 
    }
}
