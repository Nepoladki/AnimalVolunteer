﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AnimalVolunteer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalVolunteer.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("current_status");

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

                    b.ComplexProperty<Dictionary<string, object>>("SerialNumber", "AnimalVolunteer.Domain.Aggregates.Volunteer.Entities.Pet.SerialNumber#SerialNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("serial_number");
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

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer.FullName#FullName", b1 =>
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

                    b.ComplexProperty<Dictionary<string, object>>("Statistics", "AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer.Statistics#Statistics", b1 =>
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
                    b.HasOne("AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("AnimalVolunteer.Domain.Common.ValueObjects.ContactInfoList", "ContactInfos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("pets");

                            b1.ToJson("contact_infos");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo", "Contacts", b2 =>
                                {
                                    b2.Property<Guid>("ContactInfoListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasAnnotation("Relational:JsonPropertyName", "name");

                                    b2.Property<string>("Note")
                                        .HasMaxLength(500)
                                        .HasColumnType("character varying(500)")
                                        .HasAnnotation("Relational:JsonPropertyName", "note");

                                    b2.Property<string>("PhoneNumber")
                                        .IsRequired()
                                        .HasMaxLength(16)
                                        .HasColumnType("character varying(16)")
                                        .HasAnnotation("Relational:JsonPropertyName", "phone_number");

                                    b2.HasKey("ContactInfoListPetId", "Id");

                                    b2.ToTable("pets");

                                    b2.ToJson("contact_infos");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactInfoListPetId")
                                        .HasConstraintName("fk_pets_pets_contact_info_list_pet_id");
                                });

                            b1.Navigation("Contacts");
                        });

                    b.OwnsOne("AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetailsList", "PaymentDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("pets");

                            b1.ToJson("payment_details");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails", "Payments", b2 =>
                                {
                                    b2.Property<Guid>("PaymentDetailsListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .HasColumnType("character varying(500)")
                                        .HasAnnotation("Relational:JsonPropertyName", "description");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasAnnotation("Relational:JsonPropertyName", "name");

                                    b2.HasKey("PaymentDetailsListPetId", "Id");

                                    b2.ToTable("pets");

                                    b2.ToJson("payment_details");

                                    b2.WithOwner()
                                        .HasForeignKey("PaymentDetailsListPetId")
                                        .HasConstraintName("fk_pets_pets_payment_details_list_pet_id");
                                });

                            b1.Navigation("Payments");
                        });

                    b.OwnsOne("AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet.PetPhotoList", "PetPhotos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("pet_photos");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet.PetPhoto", "PetPhotos", b2 =>
                                {
                                    b2.Property<Guid>("PetPhotoListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("FilePath")
                                        .IsRequired()
                                        .HasMaxLength(200)
                                        .HasColumnType("character varying(200)")
                                        .HasAnnotation("Relational:JsonPropertyName", "path");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean")
                                        .HasAnnotation("Relational:JsonPropertyName", "is_main");

                                    b2.HasKey("PetPhotoListPetId", "Id");

                                    b2.ToTable("pets");

                                    b2.ToJson("pet_photos");

                                    b2.WithOwner()
                                        .HasForeignKey("PetPhotoListPetId")
                                        .HasConstraintName("fk_pets_pets_pet_photo_list_pet_id");
                                });

                            b1.Navigation("PetPhotos");
                        });

                    b.Navigation("ContactInfos")
                        .IsRequired();

                    b.Navigation("PaymentDetails")
                        .IsRequired();

                    b.Navigation("PetPhotos")
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer", b =>
                {
                    b.OwnsOne("AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer.SocialNetworkList", "SocialNetworks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("social_networks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(25)
                                        .HasColumnType("character varying(25)")
                                        .HasAnnotation("Relational:JsonPropertyName", "name");

                                    b2.Property<string>("URL")
                                        .HasMaxLength(150)
                                        .HasColumnType("character varying(150)")
                                        .HasAnnotation("Relational:JsonPropertyName", "url");

                                    b2.HasKey("SocialNetworkListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("social_networks");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_list_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.OwnsOne("AnimalVolunteer.Domain.Common.ValueObjects.ContactInfoList", "ContactInfos", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("contact_infos");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo", "Contacts", b2 =>
                                {
                                    b2.Property<Guid>("ContactInfoListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasAnnotation("Relational:JsonPropertyName", "name");

                                    b2.Property<string>("Note")
                                        .HasMaxLength(500)
                                        .HasColumnType("character varying(500)")
                                        .HasAnnotation("Relational:JsonPropertyName", "note");

                                    b2.Property<string>("PhoneNumber")
                                        .HasMaxLength(16)
                                        .HasColumnType("character varying(16)")
                                        .HasAnnotation("Relational:JsonPropertyName", "phone_number");

                                    b2.HasKey("ContactInfoListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("contact_infos");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactInfoListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_contact_info_list_volunteer_id");
                                });

                            b1.Navigation("Contacts");
                        });

                    b.OwnsOne("AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetailsList", "PaymentDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("payment_details");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails", "Payments", b2 =>
                                {
                                    b2.Property<Guid>("PaymentDetailsListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .HasMaxLength(500)
                                        .HasColumnType("character varying(500)")
                                        .HasAnnotation("Relational:JsonPropertyName", "description");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasAnnotation("Relational:JsonPropertyName", "name");

                                    b2.HasKey("PaymentDetailsListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("payment_details");

                                    b2.WithOwner()
                                        .HasForeignKey("PaymentDetailsListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_payment_details_list_volunteer_id");
                                });

                            b1.Navigation("Payments");
                        });

                    b.Navigation("ContactInfos")
                        .IsRequired();

                    b.Navigation("PaymentDetails")
                        .IsRequired();

                    b.Navigation("SocialNetworks")
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.PetType.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("AnimalVolunteer.Domain.Aggregates.Volunteer.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
