using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Web.Contexts
{
   public class GraphQLUserContext : Dictionary<string, object>
   {
      public ClaimsPrincipal User { get; set; }
   }
}
