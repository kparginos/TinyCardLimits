﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TinyCardLimits.Core.Data;

namespace TinyCardLimits.Migrations.Migrations
{
    [DbContext(typeof(TinyCardLimitsDBContext))]
    [Migration("20210223115309_Add_CardNumber_As_UniqueKey")]
    partial class Add_CardNumber_As_UniqueKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TinyCardLimits.Core.Model.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AvailableBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CardId");

                    b.HasIndex("CardNumber")
                        .IsUnique()
                        .HasFilter("[CardNumber] IS NOT NULL");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("TinyCardLimits.Core.Model.CardLimit", b =>
                {
                    b.Property<int>("CardLimitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AggrAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ApplyDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("CardLimitId");

                    b.HasIndex("CardId");

                    b.ToTable("Limit");
                });

            modelBuilder.Entity("TinyCardLimits.Core.Model.CardLimit", b =>
                {
                    b.HasOne("TinyCardLimits.Core.Model.Card", null)
                        .WithMany("Limits")
                        .HasForeignKey("CardId");
                });

            modelBuilder.Entity("TinyCardLimits.Core.Model.Card", b =>
                {
                    b.Navigation("Limits");
                });
#pragma warning restore 612, 618
        }
    }
}
