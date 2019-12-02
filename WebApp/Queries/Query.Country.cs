using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.Models;
using WebApp.Types;

namespace WebApp.Queries
{
   public partial class Query
   {
      private void ConfigureTypeCountry(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("countries")
            .Type<ListType<CountryType>>()
            .Argument("load", x => { x.Type<BooleanType>(); x.DefaultValue(false); })
            .Resolver(context =>
            {
               bool load = context.Argument<bool>("load");
               QLContext qlContext = context.Service<QLContext>();
               return load
                  ? qlContext.Country.Include(x => x.State).ToList()
                  : qlContext.Country.ToList();
            });

         descriptor
            .Field("country_find")
            .Type<CountryType>()
            .Argument("id", x => { x.Type<IntType>(); x.DefaultValue(0); })
            .Argument("load", x => { x.Type<BooleanType>(); x.DefaultValue(false); })
            .Resolver(context =>
            {
               int id = context.Argument<int>("id");
               bool load = context.Argument<bool>("load");
               QLContext qlContext = context.Service<QLContext>();
               IQueryable<Country> query = qlContext.Country.Where(x => x.Id == id);
               return load
                  ? query.Include(x => x.State).FirstOrDefault()
                  : query.FirstOrDefault();
            });
      }
   }
}
