using Canducci.GraphQLQuery.CustomTypes;
using HotChocolate.Types;
using WebApp.Models;

namespace WebApp.Types
{
   public class SourceType: ObjectType<Source>
   {
      protected override void Configure(IObjectTypeDescriptor<Source> descriptor)
      {
         Name = "source_type";
         descriptor.Name("source_type");
         descriptor.Field(x => x.Id).Name("id").Type<UuidType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.Value).Name("value").Type<DecimalType>();
         descriptor.Field(x => x.Created).Name("created").Type<DateTimeType>();
         descriptor.Field(x => x.Active).Name("active").Type<BooleanType>();
         descriptor.Field(x => x.Time).Name("time").Type<TimeSpanType>();
      }
   }
}
