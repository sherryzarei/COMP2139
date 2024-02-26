using System;
using A1_COMP2139.Models;
using Microsoft.EntityFrameworkCore;

namespace A1_COMP2139.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}

