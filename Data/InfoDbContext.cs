using Microsoft.EntityFrameworkCore;

namespace TodoInfo.Api.Data;

public class InfoDbContext(DbContextOptions<InfoDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Priority> Priorities => Set<Priority>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Category>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.Property(x => x.Color).HasMaxLength(16).IsRequired();
            e.HasData(
                new Category { Id = 1, Name = "Work", Color = "#2563eb" },
                new Category { Id = 2, Name = "Personal", Color = "#16a34a" },
                new Category { Id = 3, Name = "Shopping", Color = "#d97706" },
                new Category { Id = 4, Name = "Health", Color = "#db2777" });
        });

        mb.Entity<Priority>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(50).IsRequired();
            e.HasData(
                new Priority { Id = 1, Name = "Low", Level = 1 },
                new Priority { Id = 2, Name = "Medium", Level = 2 },
                new Priority { Id = 3, Name = "High", Level = 3 },
                new Priority { Id = 4, Name = "Urgent", Level = 4 });
        });
    }
}
