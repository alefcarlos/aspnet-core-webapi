using GraphQL.Authorization;
using System.Security.Claims;

namespace Demo.Application
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
