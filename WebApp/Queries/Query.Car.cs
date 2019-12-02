using HotChocolate.Types;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using WebApp.Complex;
using WebApp.Inputs;
using WebApp.Models;
using WebApp.Types;

namespace WebApp.Queries
{
   public partial class Query
   {
      private void ConfigureTypeCar(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("cars")
            .Type<ListType<CarType>>()
            .Resolver(context =>
            {
               QLContext qlContext = context.Service<QLContext>();
               return qlContext.Car.ToList();
            });

         descriptor
           .Field("car_add")
           .Type<CarType>()
           .Argument("input", x => { x.Type<CarInput>(); })
           .Resolver(context =>
           {
              Car car = context.Argument<Car>("input");
              QLContext qlContext = context.Service<QLContext>();
              IDbContextTransaction transaction = qlContext.Database.BeginTransaction();
              try
              {
                 qlContext.Car.Add(car);
                 qlContext.SaveChanges();
                 transaction.Commit();
              }
              catch (System.Exception)
              {
                 transaction.Rollback();
              }
              finally
              {
                 transaction.Dispose();
              }
              return car;
           });

         descriptor
           .Field("car_edit")
           .Type<CarType>()
           .Argument("input", x => { x.Type<CarInput>(); })
           .Resolver(context =>
           {
              Car car = context.Argument<Car>("input");
              QLContext qlContext = context.Service<QLContext>();
              IDbContextTransaction transaction = qlContext.Database.BeginTransaction();
              try
              {
                 qlContext.Car.Update(car);
                 qlContext.SaveChanges();
                 transaction.Commit();
              }
              catch (System.Exception)
              {
                 transaction.Rollback();
              }
              finally
              {
                 transaction.Dispose();
              }
              return car;
           });

         descriptor
           .Field("car_find")
           .Type<CarType>()
           .Argument("id", x => { x.Type<IntType>(); x.DefaultValue(0); })
           .Resolver(context =>
           {
              int id = context.Argument<int>("id");
              QLContext qlContext = context.Service<QLContext>();
              return qlContext.Car.Find(id);
           });

         descriptor
           .Field("car_remove")
           .Type<RemoveType>()
           .Argument("id", x => { x.Type<IntType>(); x.DefaultValue(0); })
           .Resolver(context =>
           {
              int id = context.Argument<int>("id");
              QLContext qlContext = context.Service<QLContext>();
              IDbContextTransaction transaction = qlContext.Database.BeginTransaction();
              int count = 0;
              try
              {
                 qlContext.Car.Remove(qlContext.Car.Find(id));
                 count = qlContext.SaveChanges();
                 transaction.Commit();
              }
              catch
              {
                 transaction.Rollback();
              }
              finally
              {
                 transaction.Dispose();
              }
              return Remove.Create(count);
           });
      }
   }
}
