using GraphQL.Types;
using Models;

namespace QL.MapTypes
{
  public class ItemsMapType : ObjectGraphType<Items>
  {
    public ItemsMapType()
    {
      Field(x => x.Id).Name("id");
      Field(x => x.Title).Name("title");
      Field<DateTimeGraphType>().Name("updated").DefaultValue(null);
    }
  }
}
