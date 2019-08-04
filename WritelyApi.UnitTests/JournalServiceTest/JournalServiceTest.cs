using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using WritelyApi.Data;
using WritelyApi.Entries;
using WritelyApi.UnitTests.Helpers;
using WritelyApi.Journals;
using WritelyApi.Users;
using Xunit;

namespace WritelyApi.UnitTests.JournalServiceTest
{
    public class JournalServiceTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _fixture;
        private readonly DbContextOptions<AppDbContext> _options;

        public JournalServiceTest(DbFixture fixture)
        {
            _fixture = fixture;
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("WritelyInMemoryTest")
                .Options;

        }

        [Fact]
        public async Task CanCreateNewJournalSuccessfully()
        {
            using (var context = new AppDbContext(_options))
            {

                var service = new JournalService(context);
                var userId = "User12345";
                var title = "Test";
                var journal = await service.Create(title, userId);

                journal.Title.Should().Be(title);
                journal.UserId.Should().Be(userId);
            }
        }

        [Fact]
        public async Task CanLoadJournalBasicDtos()
        {
            using (var context = new AppDbContext(_options))
            {
                var service = new JournalService(context);
                var userId = "Zippy12343";
                var journals = Utilities.GenerateJournals(5, userId);

                context.Journals.AddRange(journals);
                await context.SaveChangesAsync();

                var result = await service.GetJournals(userId);

                result.Should().HaveCount(journals.Count);
            }
        }

        [Fact]
        public async Task CanFindExistingJournalById()
        {
            using (var context = new AppDbContext(_options))
            {
                var service = new JournalService(context);
                var journal = new Journal { Title = "Test" };

                context.Journals.Add(journal);
                await context.SaveChangesAsync();

                var foundJournal = await service.FindById(journal.Id);
                foundJournal.Id.Should().Be(journal.Id);
            }
        }

        [Fact]
        public async Task CannotFindJournal()
        {
            using (var context = new AppDbContext(_options))
            {
                var service = new JournalService(context);
                var unassignedId = 1;

                var nonExistingJournal = await service.FindById(unassignedId);

                nonExistingJournal.Should().BeNull();
            }
        }

        [Fact]
        public async Task CanUpdateJournalTitle()
        {
            using (var context = new AppDbContext(_options))
            {
                var service = new JournalService(context);
                var journal = new Journal { Title = "Test" };
                context.Journals.Add(journal);
                await context.SaveChangesAsync();

                journal.Title = "New Title";
                var updatedJournal = await service.Update(new JournalDto(journal));

                updatedJournal.Title.Should().Be(journal.Title);
            }
        }

        [Fact]
        public async Task CanDeleteJournal()
        {
            using (var context = new AppDbContext(_options))
            {
                var service = new JournalService(context);
                var journal = new Journal { Title = "Delete Me" };

                context.Journals.Add(journal);
                await context.SaveChangesAsync();
                var numOfDeletedJournals = await service.Delete(journal.Id);

                numOfDeletedJournals.Should().Be(1);
            }
        }
    }
}