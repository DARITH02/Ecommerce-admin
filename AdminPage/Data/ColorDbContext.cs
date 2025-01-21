using AdminPage.Models;
using Microsoft.Data.SqlClient;
using System.Drawing;

namespace AdminPage.Data
{
    public class ColorDbContext
    {
        private readonly string _connectionString;
        public ColorDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        //public List<Colors> getColors(Colors colors) {
        //List<Colors> colorList= new List<Colors>();
        
        //    using(SqlConnection conn=new SqlConnection(_connectionString))
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ") ;
        //    }
        
        //    return colorList;
        
        //}




    }
}
