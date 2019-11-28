using GraphQL.Types;
using Models;
namespace QL.InputTypes
{
  public class CarMapInput : InputObjectGraphType<Car>
  {
    public CarMapInput()
    {
      //Name = "car";
      Field(x => x.Id).Name("id");
      Field(x => x.Title).Name("title");
      Field(x => x.Purchase).Name("purchase");
      Field(x => x.Value).Name("value");
      Field(x => x.Active).Name("active");
    }
  }
}
