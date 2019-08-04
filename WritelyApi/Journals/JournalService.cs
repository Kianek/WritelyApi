using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritelyApi.Data;

namespace WritelyApi.Journals
{
    public class JournalService : IJournalService
    {
        private readonly AppDbContext _context;

        public JournalService(AppDbContext context) => _context = context;

        public async Task<JournalDto> FindById(int id)
        {
            try
            {
                var journal = await _context
                    .Journals
                    .AsNoTracking()
                        .Include(j => j.Entries)
                    .SingleAsync(j => j.Id == id);

                return new JournalDto(journal);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Unable to locate journal. Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<JournalDto>> GetJournals(string userId)
        {
            return await _context
                .Journals
                .Where(j => j.UserId == userId)
                    .Include(j => j.Entries)
                .MapJournalToDto()
                .ToListAsync();
        }

        public async Task<JournalDto> Create(string title, string userId)
        {
            var journal = new Journal() { Title = title, UserId = userId };
            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();

            return new JournalDto(journal);
        }

        public async Task<JournalDto> Update(JournalDto journal)
        {
            var existingJournal = await _context.Journals
                .Include(j => j.Entries)
                .SingleAsync(j => j.Id == journal.Id);
            if (existingJournal != null)
            {
                existingJournal.Title = journal.Title;
                existingJournal.Update();

                _context.Journals.Update(existingJournal);
                await _context.SaveChangesAsync();
            }
            return new JournalDto(existingJournal);
        }

        public async Task<int> Delete(int journalId)
        {
            try
            {
                var existingJournal = await _context.Journals
                    .Include(j => j.Entries)
                    .SingleAsync(j => j.Id == journalId);
                if (existingJournal != null)
                {
                    _context.Journals.Remove(existingJournal);
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Unable to remove journal. Error: {ex.Message}");
                return 0;
            }
        }
    }
}