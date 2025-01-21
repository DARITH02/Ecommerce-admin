
using AdminPage.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AdminPage.Data
{
    public class CountDbContext
    {
        private readonly string connectionString;

        public CountDbContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public int Count()
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString)) {
                SqlCommand cmd = new SqlCommand("SELECT SUM(inventory) FROM products", con);
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }
            return count;
        }

        public List<Brand> getBrand()
        {
            List<Brand> brands = new List<Brand>();

            using(SqlConnection con=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_brand ORDER BY id DESC", con);

                con.Open();
                //brands=(int)cmd.ExecuteScalar();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brand b = new Brand
                    {
                        Brand_name = reader["brand_name"].ToString()
                    };
                    brands.Add(b);
                }

            }
            return brands;
        }

        public List<int> coutBrand()
        {
            List<int> count = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT brand_id, COUNT(*) AS Count  FROM products GROUP BY brand_id ORDER BY brand_id DESC", con);

                con.Open();
                //count = (int)cmd.ExecuteScalar();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count.Add(Convert.ToInt32(reader["Count"]));
                }
              

            }

                return count;
        }
    }
}
