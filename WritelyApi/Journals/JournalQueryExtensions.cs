using System.Linq;

namespace WritelyApi.Journals
{
    public static class JournalQueryExtensions
    {
        public static IQueryable<JournalDto> MapJournalToDto(this IQueryable<Journal> journals)
        {
            return journals.Select(j => new JournalDto(j));
        }
    }
}