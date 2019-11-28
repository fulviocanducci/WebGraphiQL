using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using QL.MapTypes;
using System;
using System.Linq;

namespace QL
{
  public partial class QueryTypes
  {
    public void PeopleDiversQueryType()
    {
      #region List_People_By_Created 
      //{"query":"{peoples_by_created (created:\"1993-03-03 00:00:00\") {id name created active}}"} - POSTMAN      
      //{peoples_by_created (created:"1993-03-03 00:00:00") {id name created active}} - GraphiQL
      Field<ListGraphType<PeopleMapType>>(
        name: "peoples_by_created",
        description: "List of Peoples By Created",
        arguments: new QueryArguments(
          new QueryArgument<DateTimeGraphType>() { Name = "created" }
        ),
        resolve: context =>
        {
          DateTime created = context.GetArgument<DateTime>("created");
          return DatabaseAccess
          .People
          .Where(x => x.Created == created)
          .AsNoTracking()
          .ToList();
        }
      );
      #endregion
    }
  }
}
