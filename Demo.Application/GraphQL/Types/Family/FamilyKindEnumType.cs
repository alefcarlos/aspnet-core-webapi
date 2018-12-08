using Demo.Application.Data.MySql.Entities;
using GraphQL.Types;

namespace Demo.Application.GraphQL.Types.Family
{
    public class FamilyKindEnumType : EnumerationGraphType<EFamilyKind>
    {
        public FamilyKindEnumType()
        {
            Name = "FamilyKind";
            Description = "Grau parentesco";
        }
    }
}
