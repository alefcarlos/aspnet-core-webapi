using Demo.Core.Data.MySql.Entities;
using Demo.Core.Shared.Enum;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Core.GraphQL.Types.Character.Models
{
    public class CharacterModel
    {
        public CharacterModel()
        {

        }

        public CharacterModel(CharacterEntity entity)
        {
            ID = entity.ID;
            Name = entity.Name;
            BirthDate = entity.BirthDate;
            Kind = entity.Kind;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public ECharecterKind Kind { get; set; }
        public CharacterModel[] Relatives { get; set; }

        public static List<CharacterModel> ParseEntities(params CharacterEntity[] entities)
        {
            return entities.Select(x => new CharacterModel(x)).ToList();
        }
    }
}
