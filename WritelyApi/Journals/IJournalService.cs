using System.Collections.Generic;
using System.Threading.Tasks;

namespace WritelyApi.Journals
{
    public interface IJournalService
    {
        Task<JournalDto> FindById(int id);

        Task<List<JournalDto>> GetJournals(string userId);

        Task<JournalDto> Create(string title, string userId);

        Task<JournalDto> Update(JournalDto journal);

        Task<int> Delete(int journalId);
    }
}