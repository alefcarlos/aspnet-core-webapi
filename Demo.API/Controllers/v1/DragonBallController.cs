using Demo.Core.Contracts.DragonBall.Request;
using Demo.Core.Contracts.GraphQL;
using Demo.Core.GraphQL;
using Demo.Core.Services;
using Framework.WebAPI;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers.v1
{
    /// <summary>
    /// RESTFull services for Dragon Ball
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{api-version:apiVersion}/[controller]")]
    public class DragonBallController : BaseController
    {
        private readonly ICharacterServices _services;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        IEnumerable<IValidationRule> _validationRules;

        public DragonBallController(ICharacterServices services, IDocumentExecuter documentExecuter, ISchema schema, IEnumerable<IValidationRule> validationRules)
        {
            _services = services;
            _documentExecuter = documentExecuter;
            _schema = schema;
            _validationRules = validationRules;
        }

        /// <summary>
        /// Adiciona um novo personagem.
        /// </summary>
        /// <param name="request">Dados do personagem</param>
        [HttpPost("character")]
        public async Task<IActionResult> Post([FromBody]DragonBallPostRequest request)
        {
            var result = await _services.CreateAsync(request);

            return result.ParseResult();
        }


        /// <summary>
        /// Adiciona um familiar a um determinado personagem
        /// </summary>
        /// <param name="request">Dados personagem familiar</param>
        /// <param name="id">ID do personagem</param>
        [HttpPost("character/{id}/relative/")]
        public async Task<IActionResult> PostRelative(int id, [FromBody]DragonBallPostRelativeRequest request)
        {
            var result = await _services.CreateRelative(id, request);

            return result.ParseResult();
        }

        //[HttpPost("graphql")]
        //public async Task<IActionResult> Post([FromBody] GraphQLParameter query)
        //{
        //    if (query == null)
        //    { throw new ArgumentNullException(nameof(query)); }
        //    var inputs = query.Variables.ToInputs();
        //    var executionOptions = new ExecutionOptions
        //    {
        //        Schema = _schema,
        //        Query = query.Query,
        //        Inputs = inputs,
        //        UserContext = new GraphQLUserContext
        //        {
        //            User = User
        //        },
        //        ValidationRules = DocumentValidator.CoreRules().Concat(_validationRules).ToList()
        //    };

        //    var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

        //    if (result.Errors?.Count > 0)
        //    {
        //        return BadRequest(result);
        //    }

        //    return Ok(result);
        //}
    }
}
