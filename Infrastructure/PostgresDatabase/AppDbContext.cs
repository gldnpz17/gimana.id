using DomainModel.Entities;
using DomainModel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgresDatabase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PasswordCredential> PasswordCredentials { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("MAIN_DATABASE_CONNECTION_STRING"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                b.HasOne(e => e.ProfilePicture).WithMany();

                b.OwnsMany(e => e.Privileges);
            });

            builder.Entity<Article>(b =>
            {
                b.HasOne(e => e.HeroImage).WithMany();

                b.OwnsMany(e => e.Parts, part =>
                {
                    part.OwnsMany(e => e.Steps, step =>
                    {
                        step.HasOne(e => e.Image).WithMany();
                    });
                });
            });

            builder.Entity<Email>(b =>
            {
                b.HasKey(e => e.EmailAddress);

                b.OwnsOne(e => e.VerificationToken);
            });

            builder.Entity<Image>(b =>
            {
                b.HasKey(e => e.Id);

                b.Property(e => e.Id).HasDefaultValue(Guid.NewGuid());
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
