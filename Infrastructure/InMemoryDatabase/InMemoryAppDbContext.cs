using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;

namespace InMemoryDatabase
{
    public class InMemoryAppDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AppDb");
        }
    }
}
