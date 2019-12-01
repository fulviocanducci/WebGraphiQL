using HotChocolate.Types;
using WebApp.Models;
namespace WebApp.Inputs
{   
   public class CarInput : InputObjectType<Car>
   {
      protected override void Configure(IInputObjectTypeDescriptor<Car> descriptor)
      {
         Name = "car_input";
         descriptor.Name("car_input");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Title).Name("title").Type<StringType>();
         descriptor.Field(x => x.Purchase).Name("purchase").Type<DateTimeType>();
         descriptor.Field(x => x.Value).Name("value").Type<DecimalType>();
         descriptor.Field(x => x.Active).Name("active").Type<BooleanType>();
      }
   }
}
