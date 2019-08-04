using System;
using System.Collections.Generic;
using System.Linq;
using WritelyApi.Entries;

namespace WritelyApi.Journals
{
    public class JournalDto
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<EntryDto> Entries { get; set; } = new List<EntryDto>();
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public JournalDto() { }

        public JournalDto(Journal journal)
        {
            UserId = journal.UserId;
            Id = journal.Id;
            Title = journal.Title;
            Entries = journal.Entries?.Select(e => new EntryDto(e));
            CreatedAt = journal.CreatedAt;
            LastModified = journal.LastModified;
        }
    }
}