using System.Linq;

namespace WritelyApi.Entries
{
    public static class EntryQueryExtensions
    {
        public static IQueryable<EntryDto> MapEntriesToDto(this IQueryable<Entry> entries)
        {
            return entries.Select(entry => new EntryDto
            {
                Id = entry.Id,
                Title = entry.Title,
                Tags = entry.Tags,
                Body = entry.Body,
                CreatedAt = entry.CreatedAt,
                LastModified = entry.LastModified,
                JournalId = entry.JournalId,
                UserId = entry.UserId
            });
        }
    }
}