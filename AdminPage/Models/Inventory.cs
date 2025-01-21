using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class Inventory
    {
        [Key]

        public int Id { get; set; }
        public int Qty { get; set; }
    }
}
