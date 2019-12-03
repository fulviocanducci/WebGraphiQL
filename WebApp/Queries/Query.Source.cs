using HotChocolate.Types;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using WebApp.Complex;
using WebApp.Inputs;
using WebApp.Models;
using WebApp.Types;

namespace WebApp.Queries
{
   public partial class Query
   {
      private void ConfigureTypeSource(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("sources")
            .Type<ListType<SourceType>>()
            .Resolver(context =>
            {
               QLContext qlContext = context.Service<QLContext>();
               return qlContext.Source.ToList();
            });

         descriptor
           .Field("source_add")
           .Type<SourceType>()
           .Argument("input", x => { x.Type<SourceInput>(); })
           .Resolver(context =>
           {
              Source source = context.Argument<Source>("input");
              QLContext qlContext = context.Service<QLContext>();
              IDbContextTransaction transaction = qlContext.Database.BeginTransaction();
              try
              {
                 qlContext.Source.Add(source);
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
              return source;
           });


         descriptor
           .Field("source_param_add")
           .Type<SourceType>()
           .Argument("id", x => { x.Type<UuidType>(); x.DefaultValue(null); })
           .Argument("name", x => { x.Type<IntType>(); x.DefaultValue(null); })
           .Argument("value", x => { x.Type<DecimalType>(); x.DefaultValue(null); })
           .Argument("created", x => { x.Type<DateTimeType>(); x.DefaultValue(null); })
           .Argument("active", x => { x.Type<BooleanType>(); x.DefaultValue(null); })
           .Resolver(context =>
           {
              Guid? id = context.Argument<Guid?>("id");
              string name = context.Argument<string>("name");
              decimal? value = context.Argument<decimal?>("value");
              DateTime? created = context.Argument<DateTime?>("created");
              bool? active = context.Argument<bool?>("active");
              Source source = new Source()
              {
                 Id = id,
                 Name = name,
                 Value = value,
                 Created = created,
                 Active = active
              };
              QLContext qlContext = context.Service<QLContext>();
              IDbContextTransaction transaction = qlContext.Database.BeginTransaction();
              try
              {
                 qlContext.Source.Add(source);
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
              return source;
           });


         descriptor
           .Field("source_edit")
           .Type<SourceType>()
           .Argument("input", x => { x.Type<SourceInput>(); })
           .Resolver(context =>
           {
              Source source = context.Argument<Source>("input");
              QLContext qlContext = context.Service<QLContext>();
              IDbContextTransaction transaction = qlContext.Database.BeginTransaction();
              try
              {
                 qlContext.Source.Update(source);
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
              return source;
           });

         descriptor
           .Field("source_find")
           .Type<SourceType>()
           .Argument("id", x => { x.Type<IntType>(); x.DefaultValue(0); })
           .Resolver(context =>
           {
              int id = context.Argument<int>("id");
              QLContext qlContext = context.Service<QLContext>();
              return qlContext.Car.Find(id);
           });

         descriptor
           .Field("source_remove")
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
                 qlContext.Source.Remove(qlContext.Source.Find(id));
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
