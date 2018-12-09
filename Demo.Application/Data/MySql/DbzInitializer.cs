using Demo.Application.Data.MySql.Entities;
using System.Linq;

namespace Demo.Application.Data.MySql
{
    public static class DbzInitializer
    {
        public static void Initialize(DbzMySqlContext context)
        {
            context.Database.EnsureCreated();

            if (context.Characters.Any())
                return;

            //Adicionar personagens
            var characters = new CharacterEntity[]
            {
                new CharacterEntity
                {
                        ID =1,
                     Name = "Goku",
                     BirthDate = "Ano 737",
                     Kind = ECharecterKind.Sayajin,
                },
                new CharacterEntity
                {
                        ID = 2,
                     Name = "Vegeta",
                     BirthDate = "Ano 732",
                     Kind = ECharecterKind.Sayajin,
                },
                new CharacterEntity
                {
                    ID = 3,
                     Name = "Kuririn",
                     BirthDate = "Ano 736",
                     Kind = ECharecterKind.Human,
                },
                new CharacterEntity
                {
                     ID = 4,
                     Name = "Mestre Kame",
                     BirthDate = "Ano 430",
                     Kind = ECharecterKind.Human,
                },
                new CharacterEntity
                {
                     ID = 5,
                     Name = "Bardock",
                     BirthDate = "-",
                     Kind = ECharecterKind.Sayajin,
                },
                new CharacterEntity
                {
                     ID = 6,
                     Name = "Gohan",
                     BirthDate = "18 de Maio, Ano 757",
                     Kind = ECharecterKind.Human,
                },
                new CharacterEntity
                {
                    ID = 7,
                     Name = "Trunks",
                     BirthDate = "Ano 766",
                     Kind = ECharecterKind.Human,
                },
                new CharacterEntity
                {
                     ID = 8,
                     Name = "Goten",
                     BirthDate = "Ano 767",
                     Kind = ECharecterKind.Human,
                },
            };

            context.Characters.AddRange(characters);

            //Adicionar parentesco
            var families = new FamilyEntity[]
            {
                //Bardock - Goku
                new FamilyEntity
                {
                     CharacterID =5,
                     RelativeID = 1,
                     Kind = EFamilyKind.Father
                },
                //Goku - Bardock
                new FamilyEntity
                {
                     CharacterID = 1,
                     RelativeID = 5,
                     Kind = EFamilyKind.Son
                },
                //Goku - Gohan
                new FamilyEntity
                {
                     CharacterID =1,
                     RelativeID = 6,
                     Kind = EFamilyKind.Father
                },
                //Gohan - Goku
                new FamilyEntity
                {
                     CharacterID =6,
                     RelativeID = 1,
                     Kind = EFamilyKind.Son
                },
                //Goku - Goten
                new FamilyEntity
                {
                     CharacterID =1,
                     RelativeID = 8,
                     Kind = EFamilyKind.Father
                },
                //Goten - Goku
                new FamilyEntity
                {
                     CharacterID =8,
                     RelativeID = 1,
                     Kind = EFamilyKind.Son
                },
                //Goten - Gohan
                new FamilyEntity
                {
                     CharacterID =8,
                     RelativeID = 6,
                     Kind = EFamilyKind.Brother
                },
                //Gohan - Gohan
                new FamilyEntity
                {
                     CharacterID =6,
                     RelativeID = 8,
                     Kind = EFamilyKind.Brother
                },
                //Vegeta - Trunks
                new FamilyEntity
                {
                     CharacterID =2,
                     RelativeID = 7,
                     Kind = EFamilyKind.Father
                },
                //Trunks - Vegeta
                new FamilyEntity
                {
                     CharacterID =7,
                     RelativeID = 2,
                     Kind = EFamilyKind.Son
                }
            };

            context.Families.AddRange(families);

            context.SaveChanges();
        }
    }
}
