using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanyakanIdApi.Entities.Entities;
using TanyakanIdApi.Entities.ValueObjects;

namespace TanyakanIdApi.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.UseInMemoryDatabase("TestDB");
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("TANYAKAN_ID_CONNECTION_STRING"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
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
                    .HasDefaultValueSql("uuid_generate_v4()");

                    b.OwnsOne(e => e.ProfilePicture);
                });
            modelBuilder.Entity<Article>(
                (b) =>
                {
                    b.OwnsOne(e => e.HeroImage);

                    b
                    .Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                });
            modelBuilder.Entity<ArticleHistory>(
                (b) =>
                {
                    b.Property<Guid>("Id").HasDefaultValueSql("uuid_generate_v4()");

                    b.HasKey("Id");

                    b.OwnsOne(e => e.HeroImage);
                });
            modelBuilder.Entity<ArticlePart>(
                (b) =>
                {
                    b.Property<Guid>("Id").HasDefaultValueSql("uuid_generate_v4()");

                    b.HasKey("Id");
                });
            modelBuilder.Entity<ArticleStep>(
                (b) =>
                {
                    b.Property<Guid>("Id").HasDefaultValueSql("uuid_generate_v4()");

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
                    .HasDefaultValueSql("uuid_generate_v4()");
                });
            modelBuilder.Entity<ArticleRating>(
                (b) =>
                {
                    b
                    .Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
