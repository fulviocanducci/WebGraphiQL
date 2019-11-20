using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Models;
using QL.MapTypes;
using System.Linq;
namespace QL
{
  public partial class QueryTypes
  {
    public void StateQueryType()
    {
      Field<ListGraphType<StateMapType>>(
        name: "states",
        arguments: new QueryArguments(
          new QueryArgument<BooleanGraphType>() {  Name = "load", DefaultValue = false}
        ),
        resolve: context =>
        {
          bool load = context.GetArgument<bool>("load");
          return load
            ? DatabaseAccess.State.AsNoTracking().Include(x => x.Country).ToList()
            : DatabaseAccess.State.AsNoTracking().ToList();
        }
      );
      Field<ListGraphType<StateMapType>>(
        name: "states_state",
        arguments: new QueryArguments(
          new QueryArgument<StringGraphType>() { Name = "state", DefaultValue = "" }, 
          new QueryArgument<BooleanGraphType>() { Name = "load", DefaultValue = false }
        ),
        resolve: context =>
        {
          string state = context.GetArgument<string>("state");
          bool load = context.GetArgument<bool>("load");
          IQueryable<State> query = load
            ? DatabaseAccess.State.AsNoTracking().Include(x => x.Country)
            : DatabaseAccess.State.AsNoTracking();
          if (!string.IsNullOrEmpty(state))
          {
            query = query.Where(x => x.Uf == state);
          }
          return query.ToList();
        }
      );
    }
  }
}
