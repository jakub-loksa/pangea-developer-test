using Database.Logic.Entities.Diff;
using Microsoft.EntityFrameworkCore;

namespace Database.Logic
{
    /// <summary>
    /// Required empty constructor for IoC.
    /// </summary>
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public const string DiffSchema = "Diff";

        public virtual DbSet<DiffEntity> Diffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiffEntity>().ToTable(nameof(Diffs), DiffSchema);
        }
    }
}
