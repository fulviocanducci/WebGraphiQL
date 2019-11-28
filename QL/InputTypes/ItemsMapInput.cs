using GraphQL.Types;
using Models;

namespace QL.InputTypes
{
  public class ItemsMapInput : InputObjectGraphType<Items>
  {
    public ItemsMapInput()
    {
      Field<GuidGraphType>().Name("id").DefaultValue(null);
      Field<StringGraphType>().Name("title");
      Field<DateTimeGraphType>().Name("updated").DefaultValue(null);
    }
  }
}
