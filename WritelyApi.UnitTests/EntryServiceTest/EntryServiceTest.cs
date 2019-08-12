using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using WritelyApi.Data;
using WritelyApi.Entries;
using WritelyApi.Journals;

namespace WritelyApi.UnitTests.EntryServiceTest
{
    public class EntryServiceTest
    {
        public class AddEntryTest
        {
            private readonly DbContextOptions<AppDbContext> _options;

            public AddEntryTest()
            {
                _options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("EntryServiceTestDb")
                    .Options;
            }

            [Fact]
            public async Task CanAddNewEntryToJournal()
            {
                using (var context = new AppDbContext(_options))
                {
                    var journal = new Journal { Title = "My Journal" };
                    context.Journals.Add(journal);
                    await context.SaveChangesAsync();
                    var service = new EntryService(context);

                    var addedEntry = await service.Add(new EntryCreationDto
                    {
                        Title = "My Entry",
                        Tags = "tag1,tag2",
                        Body = "My first entry",
                        JournalId = journal.Id
                    });

                    addedEntry.Should().BeOfType<EntryDto>().And.NotBeNull();
                }
            }
        }

        public class UpdateEntryTest
        {
            private readonly DbContextOptions<AppDbContext> _options;

            public UpdateEntryTest()
            {
                _options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("EntryServiceTestDb")
                    .Options;
            }

            [Fact]
            public async Task CanUpdateExistingEntry()
            {
                using (var context = new AppDbContext(_options))
                {
                    var service = new EntryService(context);
                    var journal = new Journal { Id = 1, Title = "My Journal" };
                    var entry =
                        new Entry
                        {
                            Id = 1,
                            Title = "Original Entry",
                            Tags = "tag",
                            Body = "Body",
                            JournalId = journal.Id
                        };
                    var updatedEntry =
                        new EntryDto
                        {
                            Id = entry.Id,
                            Title = "New Title",
                            Tags = "tag1,tag2",
                            Body = "New Body",
                            JournalId = journal.Id
                        };

                    journal.Entries = new List<Entry>();
                    journal.Entries.Add(entry);

                    context.Journals.Add(journal);
                    await context.SaveChangesAsync();

                    var result = await service.Update(entry.Id, updatedEntry);
                    result.Title.Should().Be(updatedEntry.Title);
                }
            }
        }

        public class DeleteEntryTest
        {

            private readonly DbContextOptions<AppDbContext> _options;

            public DeleteEntryTest()
            {
                _options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("EntryServiceTestDb")
                    .Options;
            }

            [Fact]
            public async Task CanDeleteExistingEntry()
            {
                using (var context = new AppDbContext(_options))
                {
                    var service = new EntryService(context);
                    var entry = new Entry { Title = "Delete Me", Body = "Boring stuff" };
                    var journal =
                        new Journal
                        {
                            Id = 1,
                            Title = "My Journal",
                            Entries = new List<Entry>()
                        };

                    journal.Entries.Add(entry);
                    context.Journals.Add(journal);
                    await context.SaveChangesAsync();

                    var result =
                        await service.Delete(new EntryDeletionDto { EntryId = entry.Id, JournalId = journal.Id });

                    result.Should().BeGreaterThan(0, "because more than one object was updated");
                }
            }
        }
    }
}