using AdminPage.Models;
using Microsoft.Data.SqlClient;

namespace AdminPage.Data
{
    public class ProductsDbContext
    {
        private readonly string _dbConnectionString;
        public ProductsDbContext(IConfiguration configuration)
        {
            _dbConnectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public List<Products> getCondition()
        {
            List<Products> listPro = new List<Products>();
            using (SqlConnection con = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM products AS P LEFT JOIN category AS C ON C.cate_id=P.cate_id WHERE C.cate_name='Mobile'", con);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Products product = new Products
                    {
                        Id = Convert.ToInt16(rdr["id"]),
                        Pro_name = rdr["pro_name"].ToString(),
                    };
                    listPro.Add(product);
                }
            }
                return listPro;
        }

        public List<Products> getAllProducts()
        {
            List<Products> pro = new List<Products>();

            using (SqlConnection con = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM products AS P LEFT JOIN tbl_brand AS B ON B.id=P.brand_id LEFT JOIN category AS C ON C.cate_id=P.cate_id", con);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Products product = new Products
                    {
                        Id = Convert.ToInt16(rdr["id"]),
                        Pro_name = rdr["pro_name"].ToString(),
                      
                        Prices = Convert.ToDecimal(rdr["price"]),
                        Cate_id = Convert.ToInt16(rdr["cate_id"]),
                        Brand_id = Convert.ToInt16(rdr["brand_id"]??  0),
                        Brand_name = rdr["brand_name"].ToString(),
                        Cate_name = rdr["cate_name"].ToString(),
                        ImgName = rdr["img"].ToString(),
                        ProductsCode = rdr["proCode"].ToString(),
                        Invetory =Convert.ToInt16(rdr["inventory"]),

                    };
                    pro.Add(product);
                }
            }
            return pro;
        }

        public bool addProducts(Products products)
        {
            bool isInsert = false;
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO products VALUES(@Pro_name,@Prices,@Cate_id,@Brand_id,@Img,@ProductCode,@Inventory)", connection);
                cmd.Parameters.AddWithValue("@Pro_name", products.Pro_name);
                cmd.Parameters.AddWithValue("@Prices", products.Prices);
                cmd.Parameters.AddWithValue("@Cate_id", products.Cate_id);
                cmd.Parameters.AddWithValue("@Brand_id", products.Brand_id);
                cmd.Parameters.AddWithValue("@Img", products.ImgName);
                cmd.Parameters.AddWithValue("@ProductCode", products.ProductsCode);
                cmd.Parameters.AddWithValue("@Inventory", products.Invetory);
             
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    isInsert = true;

                }
                catch (Exception ex)
                { isInsert = false; }
            }
            return isInsert;
        }


        public Products findProduct(int id)
        {
            Products product = new Products();


            using (SqlConnection con = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM products AS P LEFT JOIN tbl_brand AS B ON B.id=P.brand_id LEFT JOIN category AS C ON C.cate_id=P.cate_id LEFT JOIN inventory AS I ON I.invetryID=P.inventory WHERE P.id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    product = new Products
                    {
                        Id = Convert.ToInt16(rdr["id"]),
                        Pro_name = rdr["pro_name"].ToString(),
                        Prices = Convert.ToDecimal(rdr["price"]),
                        Cate_id = Convert.ToInt16(rdr["cate_id"]),
                        Brand_id = Convert.ToInt16(rdr["brand_id"] ?? 0),
                        ImgName = rdr["img"].ToString(),
                        Brand_name = rdr["brand_name"].ToString(),
                        Cate_name = rdr["cate_name"].ToString(),
                        ProductsCode = rdr["proCode"].ToString(),
                        Invetory = Convert.ToInt16(rdr["inventory"]),
                        InvetoryCount = Convert.ToInt16(rdr["qty"]),
                    };
                };
            }
            return product;
        }


        public bool update(Products products)
        {
            bool isUpdate = false;

            using (SqlConnection con = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE products SET pro_name=@Pro_name,price=@Prices,cate_id=@Cate_id,brand_id=@Brand_id,img=@Img,proCode=@ProductCode,inventory=@Inventory WHERE id=@Id", con);
                cmd.Parameters.AddWithValue("@Pro_name", products.Pro_name);
                cmd.Parameters.AddWithValue("@Prices", products.Prices);
                cmd.Parameters.AddWithValue("@Cate_id", products.Cate_id);
                cmd.Parameters.AddWithValue("@Brand_id", products.Brand_id);
                cmd.Parameters.AddWithValue("@Img", products.ImgName);
                cmd.Parameters.AddWithValue("@ProductCode", products.ProductsCode);
                cmd.Parameters.AddWithValue("@Inventory", products.Invetory);

                cmd.Parameters.AddWithValue("@Id", products.Id);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isUpdate = true;
                }   
                catch(Exception e)
                {
                    isUpdate = false;
                }
            }
            return isUpdate;

        }


        public bool Delete(int id)
        {
            bool isDel = false;
            using(SqlConnection conn=new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    isDel = true;
                }catch(Exception e)
                {
                    isDel = false;
                }
            }

            return isDel;
        }


    }
}
