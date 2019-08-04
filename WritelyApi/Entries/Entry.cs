using WritelyApi.Data;
using WritelyApi.Journals;

namespace WritelyApi.Entries
{
    public class Entry : Entity
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Body { get; set; }

        // Navigation Properties
        public string UserId { get; set; }
        public int JournalId { get; set; }
        public Journal Journal { get; set; }

        public Entry(EntryCreationDto entry)
        {
            Title = entry.Title;
            Tags = entry.Tags;
            Body = entry.Body;
            JournalId = entry.JournalId;
        }

        public Entry() { }

        public void UpdateFromDto(EntryDto dto)
        {
            Title = dto.Title;
            Tags = dto.Tags;
            Body = dto.Body;
            Update();
        }
    }
}