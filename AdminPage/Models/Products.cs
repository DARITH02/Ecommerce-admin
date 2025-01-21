using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class Products
    {

        [Key]
        public int Id { get; set; }

        public string Pro_name { get; set; }


        public decimal Prices{ get; set; }

        public int Cate_id{ get; set; }

        public int Brand_id{ get; set; }

        public string Brand_name{ get; set;}

        public string Cate_name{ get; set;}

        public IFormFile ImgFile {get; set;}

        public string ImgName {get; set;}

        public string ImgNameUp { get; set; }

        public string ProductsCode { get; set; }

        public int Invetory{ get; set; }
        public int InvetoryCount{ get; set; }

    }
}