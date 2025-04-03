using PuebaTopicosSpacy.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


namespace PuebaTopicosSpacy.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Fragment> Fragments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
    }

}
