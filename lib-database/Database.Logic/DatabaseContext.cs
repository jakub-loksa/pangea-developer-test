using Database.Logic.Entities.Diff;
using Microsoft.EntityFrameworkCore;

namespace Database.Logic
{
    public class DatabaseContext : DbContext
    {
        public const string DiffSchema = "Diff";

        /// <summary>
        /// Required empty constructor for IoC.
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DiffEntity> Diffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiffEntity>().ToTable(nameof(Diffs), DiffSchema);
        }
    }
}
