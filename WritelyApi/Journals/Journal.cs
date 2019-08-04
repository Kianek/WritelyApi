using System.Collections.Generic;
using WritelyApi.Data;
using WritelyApi.Entries;

namespace WritelyApi.Journals
{
    public class Journal : Entity
    {
        public string Title { get; set; }

        // Navigation Properties
        public string UserId { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}