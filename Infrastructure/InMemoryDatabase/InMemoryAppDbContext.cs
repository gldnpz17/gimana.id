using DomainModel.Entities;
using DomainModel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;

namespace InMemoryDatabase
{
    public class InMemoryAppDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseInMemoryDatabase(databaseName: "AppDb");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                b.OwnsOne(e => e.ProfilePicture);

                b.OwnsMany(e => e.Privileges);
            });

            builder.Entity<Article>(b =>
            {
                b.OwnsOne(e => e.HeroImage);

                b.OwnsMany(e => e.Parts, part =>
                {
                    part.OwnsMany(e => e.Steps, step => 
                    {
                        step.OwnsOne(e => e.Image);
                    });
                });
            });

            builder.Entity<Email>(b =>
            {
                b.HasKey(e => e.EmailAddress);

                b.OwnsOne(e => e.VerificationToken);
            });

            builder.Entity<AuthToken>(b =>
            {
                b.HasKey(e => e.Token);
            });

            builder.Entity<PasswordCredential>(b =>
            {
                b.HasKey(e => e.HashedPassword);

                b.OwnsOne(e => e.PasswordResetToken);
            });
        }
    }
}
