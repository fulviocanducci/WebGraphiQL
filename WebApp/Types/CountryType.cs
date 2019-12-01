using HotChocolate.Types;
using WebApp.Models;

namespace WebApp.Types
{
   public class CountryType : ObjectType<Country>
   {
      protected override void Configure(IObjectTypeDescriptor<Country> descriptor)
      {
         Name = "country_type";
         descriptor.Name("country_type");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.StateId).Name("stateId").Type<IntType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.State).Name("state").Type<StateType>();
      }
   }
}
