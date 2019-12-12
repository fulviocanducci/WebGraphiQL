using Canducci.GraphQLQuery.CustomTypes;
using HotChocolate.Types;
using WebApp.Models;

namespace WebApp.Types
{
   public class CarType : ObjectType<Car>
   {
      protected override void Configure(IObjectTypeDescriptor<Car> descriptor)
      {
         Name = "car_type";
         descriptor.Name("car_type");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Title).Name("title").Type<StringType>();
         descriptor.Field(x => x.Purchase).Name("purchase").Type<DateTimeType>();
         descriptor.Field(x => x.Value).Name("value").Type<DecimalType>();
         descriptor.Field(x => x.Active).Name("active").Type<BooleanType>();
         descriptor.Field(x => x.Time).Name("time").Type<TimeSpanType>();
      }
   }
}
