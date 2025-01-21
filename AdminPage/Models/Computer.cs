using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class Computer
    {

        [Key]
        public int PcId { get; set; } // Primary Key (Identity)

        public string ProName { get; set; }

        public int ProId { get; set; } // Foreign Key

      
        public int Dis { get; set; }

       
        public string Color { get; set; }

        public string Ram { get; set; }

    
        public string Rom { get; set; }

        
        public string Processor { get; set; }

        
        public string Display { get; set; }


        public string Graphic { get; set; }

     
        public string Keyboard { get; set; }

        public string Webcam { get; set; }

     
        public string Connectivity { get; set; }

     
        public string InterfacePort { get; set; }

       
        public string Speaker { get; set; }

     
        public float Battery { get; set; }

      
        public string Weight { get; set; }

        public string Os { get; set; }

    
        public string Code { get; set; } // Unique code

        public string CreateAt { get; set; }

       
        public string UpdateAt { get; set; }


}
}
