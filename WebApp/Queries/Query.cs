using HotChocolate.Types;
namespace WebApp.Queries
{
   public partial class Query : ObjectType
   {
      protected override void Configure(IObjectTypeDescriptor descriptor)
      {
         ConfigureTypeState(descriptor);
         ConfigureTypeCountry(descriptor);
         ConfigureTypeCar(descriptor);
      }
   }
}
