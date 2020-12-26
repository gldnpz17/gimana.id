﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PostgresDatabase;

namespace PostgresDatabase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.Property<Guid>("ArticlesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("ArticlesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ArticleUser");
                });

            modelBuilder.Entity("DomainModel.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("HeroImageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HeroImageId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("DomainModel.Entities.AuthToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsRemembered")
                        .HasColumnType("boolean");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Token");

                    b.HasIndex("UserId");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("DomainModel.Entities.Email", b =>
                {
                    b.Property<string>("EmailAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("EmailAddress");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("DomainModel.Entities.PasswordCredential", b =>
                {
                    b.Property<string>("HashedPassword")
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("HashedPassword");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PasswordCredentials");
                });

            modelBuilder.Entity("DomainModel.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProfilePictureId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DomainModel.ValueObjects.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("9cff84af-9a3a-433a-b4f0-23ca8dff2631"));

                    b.Property<string>("Base64EncodedData")
                        .HasColumnType("text");

                    b.Property<string>("FileFormat")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.HasOne("DomainModel.Entities.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainModel.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DomainModel.Entities.Article", b =>
                {
                    b.HasOne("DomainModel.ValueObjects.Image", "HeroImage")
                        .WithMany()
                        .HasForeignKey("HeroImageId");

                    b.OwnsMany("DomainModel.ValueObjects.ArticlePart", "Parts", b1 =>
                        {
                            b1.Property<Guid>("ArticleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("Description")
                                .HasColumnType("text");

                            b1.Property<int>("PartNumber")
                                .HasColumnType("integer");

                            b1.Property<string>("Title")
                                .HasColumnType("text");

                            b1.HasKey("ArticleId", "Id");

                            b1.ToTable("ArticlePart");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");

                            b1.OwnsMany("DomainModel.ValueObjects.ArticleStep", "Steps", b2 =>
                                {
                                    b2.Property<Guid>("ArticlePartArticleId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("ArticlePartId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer")
                                        .UseIdentityByDefaultColumn();

                                    b2.Property<string>("Description")
                                        .HasColumnType("text");

                                    b2.Property<Guid?>("ImageId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("StepNumber")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Title")
                                        .HasColumnType("text");

                                    b2.HasKey("ArticlePartArticleId", "ArticlePartId", "Id");

                                    b2.HasIndex("ImageId");

                                    b2.ToTable("ArticleStep");

                                    b2.HasOne("DomainModel.ValueObjects.Image", "Image")
                                        .WithMany()
                                        .HasForeignKey("ImageId");

                                    b2.WithOwner()
                                        .HasForeignKey("ArticlePartArticleId", "ArticlePartId");

                                    b2.Navigation("Image");
                                });

                            b1.Navigation("Steps");
                        });

                    b.Navigation("HeroImage");

                    b.Navigation("Parts");
                });

            modelBuilder.Entity("DomainModel.Entities.AuthToken", b =>
                {
                    b.HasOne("DomainModel.Entities.User", "User")
                        .WithMany("AuthTokens")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainModel.Entities.Email", b =>
                {
                    b.HasOne("DomainModel.Entities.User", "User")
                        .WithOne("Email")
                        .HasForeignKey("DomainModel.Entities.Email", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DomainModel.ValueObjects.EmailVerificationToken", "VerificationToken", b1 =>
                        {
                            b1.Property<string>("EmailAddress")
                                .HasColumnType("text");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("Token")
                                .HasColumnType("text");

                            b1.HasKey("EmailAddress");

                            b1.ToTable("Emails");

                            b1.WithOwner()
                                .HasForeignKey("EmailAddress");
                        });

                    b.Navigation("User");

                    b.Navigation("VerificationToken");
                });

            modelBuilder.Entity("DomainModel.Entities.PasswordCredential", b =>
                {
                    b.HasOne("DomainModel.Entities.User", "User")
                        .WithOne("PasswordCredential")
                        .HasForeignKey("DomainModel.Entities.PasswordCredential", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DomainModel.ValueObjects.PasswordResetToken", "PasswordResetToken", b1 =>
                        {
                            b1.Property<string>("PasswordCredentialHashedPassword")
                                .HasColumnType("text");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("Token")
                                .HasColumnType("text");

                            b1.HasKey("PasswordCredentialHashedPassword");

                            b1.ToTable("PasswordCredentials");

                            b1.WithOwner()
                                .HasForeignKey("PasswordCredentialHashedPassword");
                        });

                    b.Navigation("PasswordResetToken");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainModel.Entities.User", b =>
                {
                    b.HasOne("DomainModel.ValueObjects.Image", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.OwnsMany("DomainModel.ValueObjects.UserPrivilege", "Privileges", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("PrivilegeName")
                                .HasColumnType("text");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("UserPrivilege");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Privileges");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("DomainModel.Entities.User", b =>
                {
                    b.Navigation("AuthTokens");

                    b.Navigation("Email");

                    b.Navigation("PasswordCredential");
                });
#pragma warning restore 612, 618
        }
    }
}
