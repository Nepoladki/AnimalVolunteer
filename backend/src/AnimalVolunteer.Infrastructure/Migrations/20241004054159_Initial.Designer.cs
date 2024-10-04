﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AnimalVolunteer.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalVolunteer.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20241004054159_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.PetType.Entities.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.ComplexProperty<Dictionary<string, object>>("Title", "AnimalVolunteer.Domain.Aggregates.PetType.Entities.Breed.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("character varying(30)")
                                .HasColumnName("title");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.PetType.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Title", "AnimalVolunteer.Domain.Aggregates.PetType.Species.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("character varying(30)")
                                .HasColumnName("title");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("ContactInfoList")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("contact_info");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("current_status");

                    b.Property<string>("PaymentDetailsList")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("payment_details");

                    b.Property<string>("PetPhotosList")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("pet_photos");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("country");

                            b1.Property<string>("House")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("house");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInfo", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.HealthInfo#HealthInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("health_description");

                            b1.Property<bool>("IsNeutered")
                                .HasColumnType("boolean")
                                .HasColumnName("is_neutered");

                            b1.Property<bool>("IsVaccinated")
                                .HasColumnType("boolean")
                                .HasColumnName("is_vaccinated");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(25)
                                .HasColumnType("character varying(25)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhysicalParameters", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.PhysicalParameters#PhysicalParameters", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Color")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("color");

                            b1.Property<double>("Height")
                                .HasColumnType("double precision")
                                .HasColumnName("height");

                            b1.Property<double>("Weight")
                                .HasColumnType("double precision")
                                .HasColumnName("weight");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Position", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.Position#Position", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("position");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesAndBreed", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.SpeciesAndBreed#SpeciesAndBreed", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ContactInfoList")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("contact_info");

                    b.Property<string>("PaymentDetailsList")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("payment_details");

                    b.Property<string>("SocialNetworksList")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("social_networks");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(25)
                                .HasColumnType("character varying(25)")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(25)
                                .HasColumnType("character varying(25)")
                                .HasColumnName("last_name");

                            b1.Property<string>("SurName")
                                .HasMaxLength(25)
                                .HasColumnType("character varying(25)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Statistics", "AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer.Statistics#Statistics", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("ExpirienceYears")
                                .HasColumnType("integer")
                                .HasColumnName("expirience_years");

                            b1.Property<int>("PetsFoundedHome")
                                .HasColumnType("integer")
                                .HasColumnName("pets_founded_home");

                            b1.Property<int>("PetsInVetClinic")
                                .HasColumnType("integer")
                                .HasColumnName("pets_in_vet_clinic");

                            b1.Property<int>("PetsLookingForHome")
                                .HasColumnType("integer")
                                .HasColumnName("pets_looking_for_home");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.PetType.Entities.Breed", b =>
                {
                    b.HasOne("AnimalVolunteer.Domain.Aggregates.PetType.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet", b =>
                {
                    b.HasOne("AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.PetType.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Root.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
