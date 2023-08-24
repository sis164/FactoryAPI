﻿using Microsoft.EntityFrameworkCore;
using FactoryAPI.Models;
using Npgsql;

namespace FactoryAPI.Controllers
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Card> Card { get; set; }
        public DbSet<Factory> Factory { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<CardOperation> CardOperations { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<NewsReport> NewsReports { get; set; }
        public ApplicationContext()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (NpgsqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.100.26;Port=5432;Database=db_enterprise;Username=postgres;Password=ssddss123");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
