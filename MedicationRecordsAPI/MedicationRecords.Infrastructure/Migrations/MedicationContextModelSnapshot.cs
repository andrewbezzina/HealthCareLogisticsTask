﻿// <auto-generated />
using MedicationRecords.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicationRecordsAPI.Migrations
{
    [DbContext(typeof(MedicationContext))]
    partial class MedicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedicationRecordsAPI.Models.ATCCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("ATCCodes");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.ActiveIngredients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActiveIngredientList")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("ActiveIngredients");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.Classification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassificationName")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.ToTable("Classifications");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ATCCodeId")
                        .HasColumnType("int");

                    b.Property<int>("ActiveIngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("ClassificationId")
                        .HasColumnType("int");

                    b.Property<int>("CompetentAuthorityStatus")
                        .HasColumnType("int");

                    b.Property<int>("InternalStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("PharmaceuticalFormsId")
                        .HasColumnType("int");

                    b.Property<int>("TherapeuticClassId")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ATCCodeId");

                    b.HasIndex("ActiveIngredientsId");

                    b.HasIndex("ClassificationId");

                    b.HasIndex("PharmaceuticalFormsId");

                    b.HasIndex("TherapeuticClassId");

                    b.HasIndex("UnitId");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.PharmaceuticalForms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PharmaceuticalFormsList")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("PharmaceuticalForms");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.TherapeuticClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TherapeuticClassName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TherapeuticClasses");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("MedicationRecordsAPI.Models.Medication", b =>
                {
                    b.HasOne("MedicationRecordsAPI.Models.ATCCode", "ATCCode")
                        .WithMany()
                        .HasForeignKey("ATCCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicationRecordsAPI.Models.ActiveIngredients", "ActiveIngredients")
                        .WithMany()
                        .HasForeignKey("ActiveIngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicationRecordsAPI.Models.Classification", "Classification")
                        .WithMany()
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicationRecordsAPI.Models.PharmaceuticalForms", "PharmaceuticalForms")
                        .WithMany()
                        .HasForeignKey("PharmaceuticalFormsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicationRecordsAPI.Models.TherapeuticClass", "TherapeuticClass")
                        .WithMany()
                        .HasForeignKey("TherapeuticClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicationRecordsAPI.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ATCCode");

                    b.Navigation("ActiveIngredients");

                    b.Navigation("Classification");

                    b.Navigation("PharmaceuticalForms");

                    b.Navigation("TherapeuticClass");

                    b.Navigation("Unit");
                });
#pragma warning restore 612, 618
        }
    }
}