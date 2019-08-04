namespace WritelyApi.Journals
{
    public class JournalBasicDto
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public JournalBasicDto(Journal journal)
        {
            Id = journal.Id;
            Title = journal.Title;
        }

        public JournalBasicDto() { }
    }
}