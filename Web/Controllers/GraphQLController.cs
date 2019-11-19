using DataAccess;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using QL;
using QL.Support;
using System.Threading.Tasks;

namespace Web.Controllers
{
  [Route("graphql")]
  [ApiController]
  public class GraphQLController : ControllerBase
  {
    public DatabaseAccess DatabaseAccess { get; }
    public GraphQLController(DatabaseAccess databaseAccess)
    {
      DatabaseAccess = databaseAccess;
    }
    public async Task<IActionResult> Post([FromBody]GraphQLQuery query)
    {
      try
      {
        Inputs inputs = query.Variables.ToInputs();
        ExecutionResult result = null;
        using (Schema schema = new Schema())
        {
          schema.Query = new QueryTypes(DatabaseAccess);
          void options(ExecutionOptions x)
          {
            x.Schema = schema;
            x.Query = query.Query;
            x.OperationName = query.OperationName;
            x.Inputs = inputs;
          }
          DocumentExecuter documentExecuter = new DocumentExecuter();
          result = await documentExecuter.ExecuteAsync(options);
          if (result?.Errors?.Count > 0)
          {
            return BadRequest(result.Errors);
          }
        }
        return Ok(result?.Data);
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex);
      }      
    }
  }
}