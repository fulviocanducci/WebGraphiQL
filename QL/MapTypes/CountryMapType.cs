using GraphQL.Types;
using Models;
namespace QL.MapTypes
{
  public class CountryMapType : ObjectGraphType<Country>
  {
    public CountryMapType()
    {
      Name = "country";
      Field(x => x.Id).Name("id");
      Field(x => x.Name).Name("name");
      Field(x => x.StateId).Name("stateId");
      Field<StateMapType>().Name("state");
    }
  }
}
