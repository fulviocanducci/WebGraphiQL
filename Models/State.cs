using System.Collections.Generic;
namespace Models
{
  public class State
  {
    public int Id { get; set; }
    public string Uf { get; set; }
    public virtual ICollection<Country> Country { get; set; }
  }
}
