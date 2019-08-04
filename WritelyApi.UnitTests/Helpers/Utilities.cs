using System.Collections.Generic;
using WritelyApi.Journals;

namespace WritelyApi.UnitTests.Helpers
{
    public static class Utilities
    {
        public static List<Journal> GenerateJournals(int numOfJournals, string userId = "123456")
        {
            var journalsList = new List<Journal>();
            for (int i = 0, num = 1; i < numOfJournals; ++i, ++num)
            {
                journalsList.Add(new Journal
                {
                    Title = $"Journal No. {num}",
                    UserId = userId
                });
            }

            return journalsList;
        }
    }
}