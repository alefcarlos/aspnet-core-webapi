using GraphQL;
using GraphQL.Types;

namespace Demo.Application.GraphQL.Models
{
    public class DemoSchema : Schema
    {
        public DemoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<DemoQuery>();
            //Mutation = resolver.Resolve<DemoMutation>();
        }
    }
}
