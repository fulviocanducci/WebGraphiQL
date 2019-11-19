using GraphQL.Types;
using Models;
namespace QL.InputTypes
{  
  public class PeopleMapInput : InputObjectGraphType<People>
  {
    public PeopleMapInput()
    {
      Name = "people";
      Field(x => x.Id).Name("id");
      Field(x => x.Name).Name("name");
      Field(x => x.Created).Name("created");
      Field(x => x.Active).Name("active");
    }
  }
}
