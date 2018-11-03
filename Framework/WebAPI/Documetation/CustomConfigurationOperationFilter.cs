using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.WebAPI.Documetation
{
    /// <summary>
    /// Middleware para customização do comportamento do swagger.
    /// </summary>
    public class CustomConfigurationOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Aplica a customização do comportamento do swagger.
        /// </summary>
        /// <param name="operation">Operação do swagger.</param>
        /// <param name="_">Não utilizado.</param>
        public void Apply(Operation operation, OperationFilterContext _)
        {
            operation.Produces = new string[] { "application/json" };
            operation.Consumes = new string[] { "application/json" };
        }
    }
}
