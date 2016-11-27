using System.Collections.Generic;
using Git.Stats.Models;

namespace Git.Stats.Infrastructure.Services.Models
{
    internal sealed class MergeAuthorsModel
    {
        internal static MergeAuthorsModel Success(Author mainAuthor, IList<int> duplicates)
        {
            return new MergeAuthorsModel(mainAuthor, duplicates, null);
        }
        internal static MergeAuthorsModel Error(string error)
        {
            return new MergeAuthorsModel(null, null, error);
        }

        private MergeAuthorsModel(Author mainAuthor, IList<int> duplicates, string error)
        {
            MainAuthor = mainAuthor;
            Duplicates = duplicates;
            ErrorMessage = error;
        }

        internal Author MainAuthor { get; }

        internal IList<int> Duplicates { get; }

        internal string ErrorMessage { get; }
    }
}
