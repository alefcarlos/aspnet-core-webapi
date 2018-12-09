using Demo.Application.Shared.Enum;
using GraphQL.Types;

namespace Demo.Application.GraphQL.Types.Family
{
    public class RelativeKindEnumType : EnumerationGraphType<ERelativeKind>
    {
        public RelativeKindEnumType()
        {
            Name = "FamilyKind";
            Description = "Grau parentesco";
        }
    }
}
