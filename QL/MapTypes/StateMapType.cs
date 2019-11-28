using GraphQL.Types;
using Models;

namespace QL.MapTypes
{
   public class StateMapType : ObjectGraphType<State>
   {
      public StateMapType()
      {
         Name = "state";
         Field(x => x.Id).Name("id");
         Field(x => x.Uf).Name("uf");
         Field<ListGraphType<CountryMapType>>().Name("country");
      }
   }
}
