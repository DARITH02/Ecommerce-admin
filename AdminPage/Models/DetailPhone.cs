using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class DetailPhone
    {
        [Key]
        public int Id { get; set; }

        public string ProName { get; set; }
        public string Brand { get; set; }

        public int ProId { get; set; }

        public int Discount { get; set; }

        public string Color { get; set; }

        public int Ram { get; set; }

        public int Rom { get; set; }

        public int Camera { get; set; }

        public int Bettery { get; set; }

        public int Display { get; set; }

        public string CreateAt { get; set; }

        public string UpdateAt { get; set; }
    }                                              
}
