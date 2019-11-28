using DataAccess;
using GraphQL.Types;
namespace QL
{
  public partial class QueryTypes : ObjectGraphType
  {
    public DatabaseAccess DatabaseAccess { get; }
    public QueryTypes(DatabaseAccess databaseAccess)
    {
      DatabaseAccess = databaseAccess;
      PeopleQueryType();
      PeopleDiversQueryType();
      StateQueryType();
      CountryQueryType();
      CarQueryType();
      ItemsQueryType();
    }   
  }
}
