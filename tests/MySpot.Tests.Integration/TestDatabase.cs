using Microsoft.EntityFrameworkCore;

using MySpot.Infrastructure.DAL;

namespace MySpot.Tests.Integration
{
    internal sealed class TestDatabase : IDisposable
    {
        public MySpotDbContext Context { get; }

        public TestDatabase()
        {
            var option = new OptionsProvider()
                .Get<PostgresOptions>("postgres");
            Context = new MySpotDbContext(new DbContextOptionsBuilder<MySpotDbContext>()
                .UseNpgsql(option.ConnectionString).Options);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
