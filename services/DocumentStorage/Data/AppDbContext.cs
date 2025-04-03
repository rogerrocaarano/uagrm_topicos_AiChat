using System.Collections.Generic;
using System.Reflection.Emit;
using DocumentStorage.Models;
using Microsoft.EntityFrameworkCore;


namespace DocumentStorage.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Fragment> Fragments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
    }

}
