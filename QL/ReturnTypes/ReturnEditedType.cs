using GraphQL.Types;
namespace QL.ReturnTypes
{
  public class ReturnEditedType : ObjectGraphType<EditedType>
  {
    public ReturnEditedType()
    {
      Field(x => x.Status).Name("status");
      Field(x => x.Description).Name("description");
      Field(x => x.Operation).Name("operation");
    }
  }
}

