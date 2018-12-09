using Demo.Core.Shared.Enum;
using GraphQL.Types;

namespace Demo.Core.GraphQL.Types.Family
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
