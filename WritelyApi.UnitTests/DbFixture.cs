using Microsoft.EntityFrameworkCore;
using WritelyApi.Data;
using Xunit;

namespace WritelyApi.UnitTests
{
    public class DbFixture
    {
        public AppDbContext Context { get; private set; }

        public DbFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("WritelyTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                Context = context;
            }
        }
    }
}