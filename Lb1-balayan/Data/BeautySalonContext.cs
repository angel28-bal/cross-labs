using Microsoft.EntityFrameworkCore;
using Lb1_balayan.Models;
using System;

namespace Lb1_balayan.Data
{
    public class BeautySalonContext : DbContext
    {
        public BeautySalonContext(DbContextOptions<BeautySalonContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Stylist> Stylists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связей между сущностями
            modelBuilder.Entity<Appointment>()
                .HasOne<Stylist>()
                .WithMany()
                .HasForeignKey(a => a.StylistId);

            modelBuilder.Entity<Appointment>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(a => a.ClientId);

            // Добавление начальных данных для Stylists
            modelBuilder.Entity<Stylist>().HasData(
                new Stylist { Id = 1, Name = "Иван Иванов", ExperienceLevel = 5 },
                new Stylist { Id = 2, Name = "Петр Петров", ExperienceLevel = 3 }
            );

            // Добавление начальных данных для Clients
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "Алексей Смирнов", PreferredStyle = "Классический" },
                new Client { Id = 2, Name = "Мария Сидорова", PreferredStyle = "Модерн" }
            );

            // Добавление начальных данных для Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, StylistId = 1, ClientId = 1, AppointmentTime = DateTime.Now.AddHours(1) },
                new Appointment { Id = 2, StylistId = 2, ClientId = 2, AppointmentTime = DateTime.Now.AddHours(3) }
            );
        }
    }
}
