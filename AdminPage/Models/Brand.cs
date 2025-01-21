using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
       
        public string Brand_name { get; set; }
        public string Brand_description { get; set; }
        public IFormFile Img { get; set; }
        public string Img_name { get; set; }
        public string ImgOld {  get; set; }
        public string Create_at { get; set; }
        public string Update_at{ get; set; }
    
    }
  
  
}
