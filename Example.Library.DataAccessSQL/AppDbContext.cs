using Example.Library.DataAccessSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Library.DataAccessSQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EntityFramework will take care of the new table by convention
            // TODO: Check if you need the fluent API or Entityframework convention can take care of the relation
            modelBuilder.Entity<BookEntity>()
                .HasMany(x => x.Authors)
                .WithMany(x => x.Books);
        }

        public DbSet<BookEntity> Books { get; set; }

        public DbSet<AuthorEntity> Authors { get; set; }
    }
}
