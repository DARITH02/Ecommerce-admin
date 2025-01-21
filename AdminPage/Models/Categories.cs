using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string CategoriesName { get; set; } 


    }
}
