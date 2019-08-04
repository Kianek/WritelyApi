using System;

namespace WritelyApi.Entries
{
    public class EntryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public int JournalId { get; set; }
        public string UserId { get; set; }

        public EntryDto(Entry entry)
        {
            Id = entry.Id;
            Title = entry.Title;
            Tags = entry.Tags;
            Body = entry.Body;
            CreatedAt = entry.CreatedAt;
            LastModified = entry.LastModified;
            JournalId = entry.JournalId;
            UserId = entry.UserId;
        }

        public EntryDto() { }
    }
}