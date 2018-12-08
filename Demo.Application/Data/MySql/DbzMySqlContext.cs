using Demo.Application.Data.MySql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Application.Data.MySql
{
    /// <summary>
    /// Contexto de conexão para a base DBZ
    /// </summary>
    public class DbzMySqlContext : DbContext
    {
        public DbzMySqlContext(DbContextOptions<DbzMySqlContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<CharacterEntity> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterEntity>()
                .ToTable("Character")
                .Property(x => x.Name)
                .HasColumnType("NVARCHAR(100)");
        }
    }
}
