using GimanaIdApi.Entities.Entities;
using GimanaIdApi.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaId.Infrastructure.Mocks.InMemoryDataAccess
{
    public class InMemoryAppDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseInMemoryDatabase("TestDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEmail>(
                (b) =>
                {
                    b.HasKey(e => e.EmailAddress);
                });
            modelBuilder.Entity<User>(
                (b) =>
                {
                    b
                    .HasOne(e => e.PasswordCredential)
                    .WithOne(e => e.User)
                    .HasForeignKey<PasswordCredential>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                    b
                    .HasOne(e => e.Email)
                    .WithOne()
                    .HasForeignKey<UserEmail>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                    b
                    .HasMany(e => e.Privileges)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

                    b
                    .Property(e => e.Id)
                    .HasDefaultValue(Guid.NewGuid());

                    b.OwnsOne(e => e.ProfilePicture);
                });
            modelBuilder.Entity<Article>(
                (b) =>
                {
                    b.OwnsOne(e => e.HeroImage);

                    b
                    .Property(e => e.Id)
                    .HasDefaultValue(Guid.NewGuid());
                });
            modelBuilder.Entity<ArticleHistory>(
                (b) =>
                {
                    b.Property<Guid>("Id")
                    .HasDefaultValue(Guid.NewGuid());

                    b.HasKey("Id");

                    b.OwnsOne(e => e.HeroImage);
                });
            modelBuilder.Entity<ArticlePart>(
                (b) =>
                {
                    b.Property<Guid>("Id")
                    .HasDefaultValue(Guid.NewGuid());

                    b.HasKey("Id");
                });
            modelBuilder.Entity<ArticleStep>(
                (b) =>
                {
                    b.Property<Guid>("Id")
                    .HasDefaultValue(Guid.NewGuid());

                    b.HasKey("Id");
                });
            modelBuilder.Entity<AuthToken>(
                (b) =>
                {
                    b.HasKey(e => e.Token);
                });
            modelBuilder.Entity<PasswordResetToken>(
                (b) =>
                {
                    b.HasKey(e => e.Token);
                });
            modelBuilder.Entity<EmailVerificationToken>(
                (b) =>
                {
                    b.HasKey(e => e.Token);
                });
            modelBuilder.Entity<PasswordCredential>(
                (b) =>
                {
                    b.HasKey(e => e.UserId);
                });
            modelBuilder.Entity<UserPrivilege>(
                (b) =>
                {
                    b.HasKey(e => e.PrivilegeName);
                });
            modelBuilder.Entity<ArticleIssue>(
                (b) =>
                {
                    b
                    .Property(e => e.Id)
                    .HasDefaultValue(Guid.NewGuid());
                });
            modelBuilder.Entity<ArticleRating>(
                (b) =>
                {
                    b
                    .Property(e => e.Id)
                    .HasDefaultValue(Guid.NewGuid());
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
