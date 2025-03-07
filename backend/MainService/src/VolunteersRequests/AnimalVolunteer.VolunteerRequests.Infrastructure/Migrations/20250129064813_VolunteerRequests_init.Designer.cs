﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20250129064813_VolunteerRequests_init")]
    partial class VolunteerRequests_init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("volunteer_requests")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalVolunteer.VolunteerRequests.Domain.VolunteerRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid")
                        .HasColumnName("admin_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_id");

                    b.Property<DateTime?>("LastRejectionAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_rejection_at");

                    b.Property<string>("RejectionComment")
                        .HasColumnType("text")
                        .HasColumnName("rejection_comment");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status_enum");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ComplexProperty<Dictionary<string, object>>("VolunteerInfo", "AnimalVolunteer.VolunteerRequests.Domain.VolunteerRequest.VolunteerInfo#VolunteerInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("ExpirienceDescription")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("expirience_description");

                            b1.Property<string>("Passport")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("passport");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer_requests");

                    b.ToTable("volunteer_requests", "volunteer_requests");
                });
#pragma warning restore 612, 618
        }
    }
}
