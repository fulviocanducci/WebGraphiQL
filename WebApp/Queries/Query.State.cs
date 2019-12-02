using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.Models;
using WebApp.Types;

namespace WebApp.Queries
{
   public partial class Query
   {
      private void ConfigureTypeState(IObjectTypeDescriptor descriptor)
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
      }
   }
}
