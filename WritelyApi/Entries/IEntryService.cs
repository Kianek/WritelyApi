using System.Threading.Tasks;

namespace WritelyApi.Entries
{
    public interface IEntryService
    {
        Task<EntryDto> Add(EntryCreationDto entry);

        Task<EntryDto> Update(EntryDto entry);

        Task<int> Delete(EntryDeletionDto dto);
    }
}