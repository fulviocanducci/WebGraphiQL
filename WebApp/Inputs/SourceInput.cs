using HotChocolate.Types;
using WebApp.Models;
namespace WebApp.Inputs
{
   public class SourceInput: InputObjectType<Source>
   {
      protected override void Configure(IInputObjectTypeDescriptor<Source> descriptor)
      {
         Name = "source_input";
         descriptor.Name("source_input");
         descriptor.Field(x => x.Id).Name("id").Type<UuidType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.Value).Name("value").Type<DecimalType>();
         descriptor.Field(x => x.Created).Name("created").Type<DateTimeType>();
         descriptor.Field(x => x.Active).Name("active").Type<BooleanType>();
      }
   }
}
