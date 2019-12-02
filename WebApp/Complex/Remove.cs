namespace WebApp.Complex
{
   public class Remove
   {
      public Remove(int count)
      {
         Status = count > 0;
         Description = Status ? "Success" : "Does not exist or failure";
      }
      public bool Status { get; }
      public int Count { get; }
      public string Description { get; }
      public static Remove Create(int count)
      {
         return new Remove(count);
      }
   }
}
