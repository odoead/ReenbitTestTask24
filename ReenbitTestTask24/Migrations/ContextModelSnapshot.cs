﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReenbitTestTask24.DB;

#nullable disable

namespace ReenbitTestTask24.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReenbitTestTask24.Entities.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("ReenbitTestTask24.Entities.Sentiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatMessageId")
                        .HasColumnType("int");

                    b.Property<double>("NegativeScore")
                        .HasColumnType("float");

                    b.Property<double>("NeutralScore")
                        .HasColumnType("float");

                    b.Property<double>("PositiveScore")
                        .HasColumnType("float");

                    b.Property<int>("SentimentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatMessageId")
                        .IsUnique();

                    b.ToTable("Sentiments");
                });

            modelBuilder.Entity("ReenbitTestTask24.Entities.Sentiment", b =>
                {
                    b.HasOne("ReenbitTestTask24.Entities.ChatMessage", "ChatMessage")
                        .WithOne("Sentiment")
                        .HasForeignKey("ReenbitTestTask24.Entities.Sentiment", "ChatMessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatMessage");
                });

            modelBuilder.Entity("ReenbitTestTask24.Entities.ChatMessage", b =>
                {
                    b.Navigation("Sentiment")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}