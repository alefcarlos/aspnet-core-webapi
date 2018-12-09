using GraphQL;
using GraphQL.Types;

namespace Demo.Core.GraphQL.Types
{
    public class DbzSchema : Schema
    {
        public DbzSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<DbzQuery>();
            Mutation = resolver.Resolve<DbzMutation>();
        }
    }
}
