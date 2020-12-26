using Microsoft.EntityFrameworkCore;
using PostgresDatabase;

namespace InMemoryDatabase
{
    public class InMemoryAppDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseInMemoryDatabase(databaseName: "AppDb");
        }
    }
}
