using System.Collections.Generic;
using WritelyApi.Data;
using WritelyApi.Entries;
using WritelyApi.Journals;
using WritelyApi.Users;

namespace WritelyApi.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void SeedDatabase(AppDbContext db)
        {
            var userId = "TestUser12345";
            var entries = new List<Entry>
            {
                new Entry { UserId = userId, Title = "I ate food today", Tags = "yum", Body = "Much excite"},
                new Entry { UserId = userId, Title = "I ate food yesterday", Tags = "yum", Body = "More excite"},
                new Entry { UserId = userId, Title = "I ate food the day before yesterday", Tags = "yum", Body = "Even more excite"},
                new Entry { UserId = userId, Title = "I died today", Tags = "dead,sad,wow", Body = "Much burial"},
            };
            var journal = new Journal
            {
                UserId = userId,
                Title = "Test Journal",
                Entries = entries
            };

            db.Journals.Add(journal);
            db.SaveChanges();
        }
    }
}