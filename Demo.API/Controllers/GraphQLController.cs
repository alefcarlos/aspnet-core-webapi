using Demo.Application.Contracts.GraphQL;
using Demo.Application.GraphQL;
using Framework.WebAPI;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class GraphQLController : BaseController
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        IEnumerable<IValidationRule> _validationRules;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema, IEnumerable<IValidationRule> validationRules)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _validationRules = validationRules;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLParameter query)
        {
            if (query == null)
            { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs,
                UserContext = new GraphQLUserContext
                {
                    User = User
                },
                ValidationRules = DocumentValidator.CoreRules().Concat(_validationRules).ToList()
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
