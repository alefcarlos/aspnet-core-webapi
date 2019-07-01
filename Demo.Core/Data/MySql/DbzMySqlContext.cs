using Demo.Core.Data.MySql.Entities;
using Demo.Core.Data.MySql.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Data.MySql
{
    /// <summary>
    /// Contexto de conexão para a base DBZ
    /// </summary>
    public class DbzMySqlContext : DbContext
    {
        public DbSet<CharacterEntity> Characters { get; set; }
        public DbSet<FamilyEntity> Families { get; set; }

        public DbzMySqlContext(DbContextOptions<DbzMySqlContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CharacterModelBuilder();
            modelBuilder.FamilyModelBuilder();
        }
    }
}
