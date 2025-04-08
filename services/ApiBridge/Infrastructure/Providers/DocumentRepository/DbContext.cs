using Infrastructure.Providers.DocumentRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Providers.DocumentRepository
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Fragment> Fragments { get; set; }
        public DbSet<FragmentVectorDbId> VectorDbIds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.HasMany(e => e.Fragments)
                    .WithOne(f => f.Document)
                    .HasForeignKey(f => f.DocumentId);
            });

            modelBuilder.Entity<Fragment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
            });

            modelBuilder.Entity<FragmentVectorDbId>(entity =>
            {
                entity.HasKey(e => new { e.FragmentId, e.ExternalId });

                entity.HasOne(e => e.Fragment)
                    .WithMany()
                    .HasForeignKey(e => e.FragmentId);
            });
        }
    }
}