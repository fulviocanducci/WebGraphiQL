﻿using GraphQL.Types;
namespace QL.ReturnTypes
{
  public class ReturnDeletedType : ObjectGraphType<DeletedType>
  {
    public ReturnDeletedType()
    {
      Field(x => x.Status).Name("status");
      Field(x => x.Description).Name("description");
      Field(x => x.Operation).Name("operation");
    }
  }
}
