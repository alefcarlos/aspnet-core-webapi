using Demo.Application.Data.MySql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Application.Data.MySql.ModelBuilders
{
    public static class EntityModelBuilderExtensions
    {
        public static EntityTypeBuilder<CharacterEntity> CharacterModelBuilder(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<CharacterEntity>();

            entityBuilder.ToTable("character");

            entityBuilder
                .Property(x => x.Name)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            entityBuilder
                .Property(x => x.Kind)
                .IsRequired();

            return entityBuilder;
        }

        public static EntityTypeBuilder<FamilyEntity> FamilyModelBuilder(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<FamilyEntity>();

            entityBuilder.ToTable("family");

            entityBuilder
                .HasKey(x => new { x.CharacterID, x.RelativeID });

            //https://stackoverflow.com/questions/42745465/ef-core-many-to-many-relationship-on-a-class/44574378#44574378
            entityBuilder
                .HasOne(c => c.Character)
                .WithMany()
                .HasForeignKey(r => r.CharacterID);

            entityBuilder
                .HasOne(r => r.Relative)
                .WithMany(c => c.Relatives)
                .HasForeignKey(r => r.RelativeID);

            return entityBuilder;
        }
    }
}
