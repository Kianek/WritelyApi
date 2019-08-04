using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WritelyApi.Data;
using WritelyApi.Journals;

namespace WritelyApi.Entries
{
    public class EntryService : IEntryService
    {
        private readonly AppDbContext _context;

        public EntryService(AppDbContext context) => _context = context;

        public async Task<EntryDto> Add(EntryCreationDto dto)
        {
            var entry = new Entry(dto);
            var journal = await FindJournalById(dto.JournalId);

            if (journal != null)
            {
                entry.UserId = journal.UserId;
                journal.Entries.Add(entry);
                _context.Journals.Update(journal);
                await _context.SaveChangesAsync();
                return new EntryDto(entry);
            }

            return null;
        }

        public async Task<EntryDto> Update(EntryDto entry)
        {
            var journal = await FindJournalById(entry.JournalId);

            if (journal != null)
            {
                var existingEntry = journal.Entries.FirstOrDefault(e => e.Id == entry.Id);
                existingEntry.UpdateFromDto(entry);
                journal.Update();
                await _context.SaveChangesAsync();

                return new EntryDto(existingEntry);
            }

            return null;
        }

        public async Task<int> Delete(EntryDeletionDto dto)
        {
            var journal = await FindJournalById(dto.JournalId);

            if (journal != null)
            {
                journal.Entries = journal.Entries.Where(e => e.Id != dto.EntryId).ToList();
                _context.Journals.Update(journal);

                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        private async Task<Journal> FindJournalById(int journalId)
        {
            return await _context
                .Journals
                    .Include(j => j.Entries)
                .SingleAsync(j => j.Id == journalId);
        }
    }
}