using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using WebApp.Inputs;
using WebApp.Models;
using WebApp.Types;

namespace WebApp.Queries
{
   public class Query : ObjectType
   {
      protected override void Configure(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("states")
            .Type<ListType<StateType>>()
            .Argument("load", x => { x.Type<BooleanType>(); x.DefaultValue(false); })
            .Resolver(context =>
            {
               bool load = context.Argument<bool>("load");
               QLContext qlContext = context.Service<QLContext>();
               return load
                  ? qlContext.State.Include(x => x.Country).ToList()
                  : qlContext.State.ToList();
            });

         descriptor
            .Field("state_find")
            .Type<StateType>()
            .Argument("id", x => { x.Type<IntType>(); x.DefaultValue(0); })
            .Argument("load", x => { x.Type<BooleanType>(); x.DefaultValue(false); })
            .Resolver(context =>
            {
               int id = context.Argument<int>("id");
               bool load = context.Argument<bool>("load");
               QLContext qlContext = context.Service<QLContext>();
               IQueryable<State> query = qlContext.State.Where(x => x.Id == id);
               return load
                  ? query.Include(x => x.Country).FirstOrDefault()
                  : query.FirstOrDefault();
            });


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
      }
   }
}
