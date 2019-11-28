using GraphQL.Types;
using Models;
namespace QL.MapTypes
{
  public class CarMapType : ObjectGraphType<Car>
  {
    public CarMapType()
    {
      //Name = "car";
      Field(x => x.Id).Name("id");
      Field(x => x.Title).Name("title");
      //Field<DateTimeGraphType>(name: "purchase", );
      Field(typeof(DateTimeGraphType), "purchase");
      Field(x => x.Value).Name("value");
      Field(x => x.Active).Name("active");
    }
  }
}
