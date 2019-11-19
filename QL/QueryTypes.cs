using DataAccess;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models;
using QL.InputTypes;
using QL.MapTypes;
using QL.ReturnTypes;
namespace QL
{
  public class QueryTypes : ObjectGraphType
  {
    public DatabaseAccess DatabaseAccess { get; }
    public QueryTypes(DatabaseAccess databaseAccess)
    {
      DatabaseAccess = databaseAccess;
      PeopleQueryType();
    }

    public void PeopleQueryType()
    {
      #region List_People
      //{"query":"{peoples {id name created active}}"} - POSTMAN      
      //{peoples {id name created active}} - GraphiQL
      FieldAsync<ListGraphType<PeopleMapType>>(
        name: "peoples",
        description: "List of Peoples",
        arguments: null,
        resolve: async context =>
        {
          return await DatabaseAccess.People.ToListAsync();
        }
      );
      #endregion
      #region Add_People
      //{"query":"{people_add(people:{id:0,name:\"Test\",created:\"2001-10-10\",active:true}){id,name,created,active}}"} - POSTMAN
      //{people_add(people: { id: 0,name: "Test 6",created: "1980-01-06",active: true}) { id name created active }}  - GraphiQL
      FieldAsync<PeopleMapType>(
        name: "people_add",
        description: "New People",
        arguments: new QueryArguments(new QueryArgument<PeopleMapInput>() { Name = "people" }),
        resolve: async context =>
        {
          People people = context.GetArgument<People>("people");
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          try
          {
            DatabaseAccess.People.Add(people);
            await DatabaseAccess.SaveChangesAsync();
            transaction.Commit();            
          }
          catch
          {
            transaction?.Rollback();
          }
          finally
          {
            transaction?.Dispose();
          }
          return people;
        }
      );
      #endregion
      #region Edit_People
      //{"query":"{people_edit(people:{id:7,name:\"Test 7777\",created:\"2001-10-10\",active:true}){status description operation}}"} - POSTMAN
      //{people_edit(people:{ id: 1,name: "Test5 - bu",created: "2001-10-11",active: true}) { status description operation }} - GraphiQL
      FieldAsync<ReturnEditedType>(
        name: "people_edit",
        arguments: new QueryArguments(new QueryArgument<PeopleMapInput>() { Name = "people" }),
        resolve: async context =>
        {
          People people = context.GetArgument<People>("people");
          bool status = false;
          if (people.Id > 0 && await DatabaseAccess.People.AnyAsync(x => x.Id == people.Id))
          {
            IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
            try
            {
              DatabaseAccess.People.Update(people);
              status = await DatabaseAccess.SaveChangesAsync() > 0;
              transaction.Commit();
            }
            catch
            {
              transaction?.Rollback();
            }
            finally
            {
              transaction?.Dispose();
            }            
          }
          return new EditedType()
          {
            Status = status,
            Description = status ? "Successfully updated" : "Does not exist or errors"
          };
        }
      );
      #endregion
      #region Delete_People
      //{"query":"{people_delete(id:7){status description operation}}"} - POSTMAN
      //{people_delete(id: 5) { status description operation}} - GraphiQL
      FieldAsync<ReturnDeletedType>(
        name:"people_delete",
        arguments: new QueryArguments(new QueryArgument<IdGraphType>() {  Name = "id", DefaultValue = 0}),
        resolve: async context =>
        {
          int id = context.GetArgument<int>("id");
          bool status = false;
          People people = DatabaseAccess.People.Find(id);
          if (people != null)
          {
            IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
            try
            {
              DatabaseAccess.People.Remove(people);
              status = await DatabaseAccess.SaveChangesAsync() > 0;
              transaction.Commit();
            }
            catch
            {
              transaction?.Rollback();
            }
            finally
            {
              transaction?.Dispose();
            }
          }
          return new DeletedType()
          {
            Status = status,
            Description = status ? "Successfully deleted" : "Does not exist or errors"
          };
        }
      );
      #endregion
      #region Find_People 
      //{"query":"{people: people_find(id:6){id name created active}}"} - POSTMAN
      //{people_find(id: 6) {id name created active}} - GraphiQL
      FieldAsync<PeopleMapType>(
        name: "people_find",
        arguments: new QueryArguments(new QueryArgument<IdGraphType>() { Name = "id" }),
        resolve: async context =>
        {
          int id = context.GetArgument<int>("id");
          return await DatabaseAccess.People.FindAsync(id);
        }
      );
      #endregion
    }
  }
}
