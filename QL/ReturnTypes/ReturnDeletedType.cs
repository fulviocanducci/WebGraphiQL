using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL.ReturnTypes
{
  public class ReturnDeletedType : ObjectGraphType<DeletedType>
  {
    public ReturnDeletedType()
    {
      Field(x => x.Status).Name("status");
      Field(x => x.Description).Name("description");
    }
  }
}
