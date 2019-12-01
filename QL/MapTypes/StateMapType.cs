using GraphQL.Types;
using Models;
//{"query":"{states{...s}},fragment s on state{id,uf}"}
namespace QL.MapTypes
{
   public class StateMapType : ObjectGraphType<State>
   {
      public StateMapType()
      {
         Name = "state";
         Field(x => x.Id).Name("id");
         Field(x => x.Uf).Name("uf");
         Field(typeof(ListGraphType<CountryMapType>), name: "country");
      }
   }

   //public class ContryMapListType : ListGraphType<CountryMapType> { }
}
