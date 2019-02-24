using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base (options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Country> Countries { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.phones)
                .WithOne(e => e.user);


            modelBuilder.Entity<User>().HasOne(c => c.address);

            modelBuilder.Entity<Address>().HasOne(a => a.city);

            modelBuilder.Entity<City>().HasOne(a => a.state);

            modelBuilder.Entity<State>().HasOne(s => s.country);

            modelBuilder.Entity<State>().HasKey(m => m.id);
            modelBuilder.Entity<City>().HasKey(m => m.id);
            modelBuilder.Entity<Address>().HasKey(m => m.id);
            modelBuilder.Entity<User>().HasKey(m => m.id);
            modelBuilder.Entity<Country>().HasKey(m => m.id);
            modelBuilder.Entity<Phone>().HasKey(m => m.id);






            modelBuilder.Entity<User>()
                          .Property(s => s.gender)
                          .HasConversion<string>();

            modelBuilder.HasDefaultSchema("api-db");
            //Map entity to table
            modelBuilder.Entity<User>().ToTable("users", "dbo");
            modelBuilder.Entity<Address>().ToTable("addresses", "dbo");
            modelBuilder.Entity<Country>().ToTable("countries", "dbo");
            modelBuilder.Entity<City>().ToTable("cities", "dbo");

            modelBuilder.Entity<State>().ToTable("states", "dbo");
            modelBuilder.Entity<Phone>().ToTable("phones", "dbo");

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }


}

