﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WEBAPP.Migrations
{
    [DbContext(typeof(dbcontextproduct))]
    [Migration("20230529200319_addMessage")]
    partial class addMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountChatModel", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("GroupsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AccountChatModel");
                });

            modelBuilder.Entity("CategProduct", b =>
                {
                    b.Property<int>("categId")
                        .HasColumnType("integer");

                    b.Property<int>("productsId")
                        .HasColumnType("integer");

                    b.HasKey("categId", "productsId");

                    b.HasIndex("productsId");

                    b.ToTable("CategProduct");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("No data");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Wallet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Categ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categ");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.ChatModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("GroupName")
                        .HasColumnType("uuid");

                    b.Property<string>("UnreadMessages")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("");

                    b.Property<string>("UserMessage")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("");

                    b.Property<int>("productId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.JWTModel", b =>
                {
                    b.Property<string>("Refreshtoken")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Refreshtoken");

                    b.ToTable("Refresh");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("accountId")
                        .HasColumnType("integer");

                    b.Property<int>("groupId")
                        .HasColumnType("integer");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Message");

                    b.HasKey("Id");

                    b.HasIndex("accountId");

                    b.HasIndex("groupId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathImage")
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("accountId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("accountId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("AccountChatModel", b =>
                {
                    b.HasOne("WEBAPP.Dbmodels.ChatModel", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEBAPP.Dbmodels.Account", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategProduct", b =>
                {
                    b.HasOne("WEBAPP.Dbmodels.Categ", null)
                        .WithMany()
                        .HasForeignKey("categId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEBAPP.Dbmodels.Product", null)
                        .WithMany()
                        .HasForeignKey("productsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.ChatModel", b =>
                {
                    b.HasOne("WEBAPP.Dbmodels.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Message", b =>
                {
                    b.HasOne("WEBAPP.Dbmodels.Account", "Account")
                        .WithMany()
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEBAPP.Dbmodels.ChatModel", "Group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Product", b =>
                {
                    b.HasOne("WEBAPP.Dbmodels.Account", "account")
                        .WithMany("Products")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("WEBAPP.Dbmodels.Account", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
