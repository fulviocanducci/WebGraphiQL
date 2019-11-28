using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models;
using QL.InputTypes;
using QL.MapTypes;
using QL.ReturnTypes;
using System.Linq;
namespace QL
{
  public partial class QueryTypes
  {
    public void CarQueryType()
    {
      //List Of Car
      Field<ListGraphType<CarMapType>>(
        name: "cars",
        arguments: null,
        resolve: context =>
        {
          return DatabaseAccess.Car.AsNoTracking().ToList();
        }
      );
      //Add Car
      Field<CarMapType>(
        name: "car_add",
        arguments: new QueryArguments(new QueryArgument<CarMapInput>() { Name = "car" }),
        resolve: context =>
        {
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          Car car = context.GetArgument<Car>("car");
          try
          {       
            DatabaseAccess.Car.Add(car);
            DatabaseAccess.SaveChanges();
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
          return car;
        }
      );
      //Edit Car
      Field<CarMapType>(
        name: "car_edit",
        arguments: new QueryArguments(new QueryArgument<CarMapInput>() { Name = "car" }),
        resolve: context =>
        {
          Car car = context.GetArgument<Car>("car");
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          try
          {
            DatabaseAccess.Car.Update(car);
            DatabaseAccess.SaveChanges();
            transaction.Commit();
          }
          catch
          {
            transaction.Rollback();            
          }
          finally
          {
            transaction?.Dispose();
          }
          return car;
        }
      );
      //Delete Car
      Field<ReturnDeletedType>(
        name: "car_delete",
        arguments: new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" }),
        resolve: context =>
        {
          int id = context.GetArgument<int>("id");
          Car car = DatabaseAccess.Car.Find(id);
          bool status = false;
          IDbContextTransaction transaction = DatabaseAccess.Database.BeginTransaction();
          try
          {          
            if (car != null)
            {
              DatabaseAccess.Car.Remove(car);
              status = DatabaseAccess.SaveChanges() > 0;
              transaction.Commit();
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
      //Delete Car
      Field<CarMapType>(
        name: "car_find",
        arguments: new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" }),
        resolve: context =>
        {
          int id = context.GetArgument<int>("id");
          Car car = DatabaseAccess.Car.AsNoTracking().FirstOrDefault(x => x.Id == id);
          return car;
        }
      );
    }
  }
}
