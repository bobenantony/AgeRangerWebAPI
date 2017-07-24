using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AgeRangerWebAPI.Entities;

namespace AgeRangerWebAPI.Migrations
{
    [DbContext(typeof(PersonInfoContext))]
    [Migration("20170716004949_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgeRangerWebAPI.Entities.AgeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MaxAge");

                    b.Property<int>("MinAge");

                    b.HasKey("Id");

                    b.ToTable("AgeGroups");
                });

            modelBuilder.Entity("AgeRangerWebAPI.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });
        }
    }
}
