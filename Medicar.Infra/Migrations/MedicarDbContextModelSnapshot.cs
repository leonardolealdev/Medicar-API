﻿// <auto-generated />
using System;
using Medicar.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Medicar.Infra.Migrations
{
    [DbContext(typeof(MedicarDbContext))]
    partial class MedicarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Medicar.Domain.Entities.Agenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Dia")
                        .HasColumnType("Date");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.ToTable("Agendas", (string)null);
                });

            modelBuilder.Entity("Medicar.Domain.Entities.Horario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AgendaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAgendamento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("AgendaId");

                    b.ToTable("Horarios", (string)null);
                });

            modelBuilder.Entity("Medicar.Domain.Entities.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CRM")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Medicos", (string)null);
                });

            modelBuilder.Entity("Medicar.Domain.Entities.Agenda", b =>
                {
                    b.HasOne("Medicar.Domain.Entities.Medico", "Medico")
                        .WithMany("Agendas")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Medicar.Domain.Entities.Horario", b =>
                {
                    b.HasOne("Medicar.Domain.Entities.Agenda", "Agenda")
                        .WithMany("Horarios")
                        .HasForeignKey("AgendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agenda");
                });

            modelBuilder.Entity("Medicar.Domain.Entities.Agenda", b =>
                {
                    b.Navigation("Horarios");
                });

            modelBuilder.Entity("Medicar.Domain.Entities.Medico", b =>
                {
                    b.Navigation("Agendas");
                });
#pragma warning restore 612, 618
        }
    }
}
