using GraphQL.Types;
using Microsoft.EntityFrameworkCore.Storage;
using Models;
using QL.InputTypes;
using QL.MapTypes;
using QL.ReturnTypes;
using System;
using System.Linq;

namespace QL
{
  public partial class QueryTypes
  {
    public void ItemsQueryType()
    {
      Field<ListGraphType<ItemsMapType>>(
        name: "items",
        arguments: null,
        resolve: context =>
        {
          return DatabaseAccess.Items.AsQueryable().ToList();
        }
      );

      Field<ItemsMapType>(
        name: "item_add",
        arguments: new QueryArguments(
          new QueryArgument<ItemsMapInput>() { Name = "item" }
        ),
        resolve: context =>
        {
          Items item = context.GetArgument<Items>("item");
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          try
          {
            DatabaseAccess.Items.Add(item);
            DatabaseAccess.SaveChanges();
            transaction?.Commit();
          }
          catch
          {
            transaction?.Rollback();
          }
          finally
          {
            transaction?.Dispose();
          }          
          return item;
        }
      );

      Field<ItemsMapType>(
        name: "item_edit",
        arguments: new QueryArguments(
          new QueryArgument<ItemsMapInput>() { Name = "item" }
        ),
        resolve: context =>
        {
          Items item = context.GetArgument<Items>("item");
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          try
          {
            DatabaseAccess.Items.Update(item);
            DatabaseAccess.SaveChanges();
            transaction?.Commit();
          }
          catch
          {
            transaction?.Rollback();
          }
          finally
          {
            transaction?.Dispose();
          }          
          return item;
        }
      );

      Field<ItemsMapType>(
        name: "item_find",
        arguments: new QueryArguments(
          new QueryArgument<GuidGraphType>() { Name = "id" }
        ),
        resolve: context =>
        {
          Guid id = context.GetArgument<Guid>("id");
          return DatabaseAccess.Items.AsQueryable().FirstOrDefault(x => x.Id == id);
        }
      );

      Field<ReturnDeletedType>(
        name: "item_delete",
        arguments: new QueryArguments(
          new QueryArgument<GuidGraphType>() { Name = "id" }
        ),
        resolve: context =>
        {
          Guid id = context.GetArgument<Guid>("id");
          bool status = false;
          Items item = DatabaseAccess.Items.Find(id);
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          try
          {
            if (item != null)
            {
              DatabaseAccess.Items.Remove(item);
              status = DatabaseAccess.SaveChanges() > 0;
              transaction?.Commit();
            }
          }
          catch
          {
            transaction?.Rollback();
          }
          finally
          {
            transaction?.Dispose();
          }
          
          return new DeletedType()
          {
            Description = status ? "Success" : "Error or Nothing",
            Status = status
          };
        }
      );
    }
  }
}
