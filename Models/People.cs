using System;
using GraphQL.Types;

namespace Models
{
  public class People
  {
    #region constructors
    public People()
    {
    }
    public People(string name)
      :this(name, DateTime.Now, true)
    {
    }

    public People(string name, bool active)
      :this(name, DateTime.Now, active)
    {
    }
    public People(string name, DateTime created)
      :this(name, created, true)
    {
    }
    public People(string name, DateTime created, bool active)
    {
      Name = name;
      Created = created;
      Active = active;
    }
    #endregion
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public bool Active { get; set; }

    public static implicit operator ObjectGraphType<object>(People v)
    {
      throw new NotImplementedException();
    }
  }
}
