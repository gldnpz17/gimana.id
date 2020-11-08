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
            optionsBuilder.UseInMemoryDatabase("TestDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = new UserEmail()
                {
                    EmailAddress = "goldenpanzer17@gmail.com",
                    IsVerified = true
                },
                BanLiftedDate = DateTime.MinValue,
                Privileges = new List<UserPrivilege>() { new UserPrivilege() { PrivilegeName = "Admin"} },
                PasswordCredential = new PasswordCredential()
                {
                    HashedPassword = "7aUjVyYk+LCwYsqFc+QJt2psYGPvU0+upOMc5gN1INWpU2W6Cy1TYBC8GqLz2Ivvewbil4in2ZYM47sial3E2aSqddQB2oKiPYSg9SKLypk3OclNUvOCvEWyeRKxwPTwwMQoaJbyVVh0a5wxmUSGWK/UJQORGCYiFNBq7Sh2mYt5/f3rIYfTYx23jhAYwPNhDKSHx6LsPQQY31I6XXvlIIv3KIR7Fae/5v9D4SLZTfMkeYpFAW+cTCH4iagHETRfaOS938x1tkXmO0LAEopVOy+WhqWsHwaZg96J9U9i71aZa4yk5Lja51dmEhlfYEJtXxpc0bKDWU2PmQ4rekHxWw==",
                    PasswordSalt = "dcU9mccExbfd189crlqTTvWFhG76Z5vfItdnF/tGUy4H1FKGM+y4OgGrFK36BT4OkSspri4poshIj9v3fiDFvw=="
                }
            };

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
                    .HasForeignKey<PasswordCredential>(e => e.UserId);

                    b.OwnsOne(e => e.ProfilePicture);
                });
            modelBuilder.Entity<Article>(
                (b) =>
                {
                    b.OwnsOne(e => e.HeroImage);
                });
            modelBuilder.Entity<ArticleHistory>(
                (b) =>
                {
                    b.HasKey(e => e.Version);

                    b.OwnsOne(e => e.HeroImage);
                });
            modelBuilder.Entity<ArticlePart>(
                (b) =>
                {
                    b.HasKey(e => e.PartNumber);
                });
            modelBuilder.Entity<ArticleStep>(
                (b) =>
                {
                    b.HasKey(e => e.StepNumber);
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
