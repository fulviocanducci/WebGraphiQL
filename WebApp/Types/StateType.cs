using HotChocolate.Types;
using WebApp.Models;

namespace WebApp.Types
{
   public class StateType : ObjectType<State>
   {
      protected override void Configure(IObjectTypeDescriptor<State> descriptor)
      {
         Name = "state_type";
         descriptor.Name("state_type");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Uf).Name("uf").Type<StringType>();
         descriptor.Field(x => x.Country).Name("country").Type<ListType<CountryType>>();
      }
   }
}
