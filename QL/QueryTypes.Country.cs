using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Models;
using QL.MapTypes;
using System.Linq;

namespace QL
{
  public partial class QueryTypes
  {
    public void CountryQueryType()
    {
      Field<ListGraphType<CountryMapType>>(
        name: "countries",
        resolve: context =>
        {
          return DatabaseAccess          
          .Country
          .AsNoTracking()
          .ToList();
        }
      );

      Field<ListGraphType<CountryMapType>>(
        name: "countries_by_name",
        arguments: new QueryArguments(
          new QueryArgument<StringGraphType>() { Name = "name", DefaultValue = ""},
          new QueryArgument<BooleanGraphType>() { Name = "load", DefaultValue = false }
        ),
        resolve: context =>
        {
          string name = context.GetArgument<string>("name");
          bool load = context.GetArgument<bool>("load");
          IQueryable<Country> query = DatabaseAccess
          .Country
          .AsNoTracking()
          .Where(x => x.Name.Contains(name));
          return load
            ? query.Include(x => x.State).ToList()
            : query.ToList();
        }
      );

      Field<CountryMapType>(
        name: "country_find",
        arguments: new QueryArguments(
          new QueryArgument<IdGraphType>() { Name = "id"},
          new QueryArgument<BooleanGraphType>() { Name = "load" }
        ),
        resolve: context =>
        {
          int id = context.GetArgument<int>("id");
          bool load = context.GetArgument<bool>("load");
          IQueryable<Country> query = DatabaseAccess
          .Country
          .AsNoTracking()
          .Where(x => x.Id == id);
          return load
            ? query.Include(x => x.State).FirstOrDefault()
            : query.FirstOrDefault();
        }
      );
    }
  }
}
