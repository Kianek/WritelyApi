namespace WritelyApi.Entries
{
    public class EntryCreationDto
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Body { get; set; }
        public int JournalId { get; set; }
    }
}