using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class Colors
    {
        [Key]
      public  int Id { get; set; }
      public string Color { get; set; }
    }
}
