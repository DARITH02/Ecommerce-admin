using AdminPage.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace AdminPage.Data
{
    public class DbContext
    {
        private readonly string _dbConnectionString;
        public DbContext(IConfiguration configuration)
        {
            _dbConnectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public Brand Find_items(int id)
        {
           Brand res= new Brand();
            string query = $"SELECT * FROM tbl_brand WHERE id=@Id";
            using (SqlConnection con = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read()){
                        res = new Brand()
                        {
                          Id = reader.GetInt32("id")   ,
                          Brand_name = reader["brand_name"].ToString(),
                          Brand_description= reader["brand_desction"].ToString() ,
                          Img_name= reader["img"].ToString()   ,
                          Create_at= reader["create_at"].ToString(),
                          Update_at= reader["update_at"].ToString(),
                        };
                    }
                }
                return res;
            }


         /*   DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(_dbConnectionString)) { 
            SqlCommand cmd = new SqlCommand("SELECT * FROM @Tbl WHERE Id=@Id",connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Tbl", tbl);

                 SqlDataAdapter reader = cmd.ExecuteReader();
                connection.Open();

                

            
            
            
            }  */                                                  

        }                            


        public List<Brand> selectBrand() {
            List<Brand> listBrand = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_brand ORDER BY id DESC", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    Brand brands = new Brand
                    {
                        Id=Convert.ToInt32(reader["id"]),
                        Brand_name=reader["brand_name"].ToString(),
                        Brand_description=reader["brand_desction"].ToString(),
                        Img_name=reader["img"].ToString(),
                    };
                    listBrand.Add(brands);
                }
            };
            return listBrand;
        }

        //insert data brand


        public bool insertBrand(Brand brand)
        {
            bool inserted = false;
            DateTime Cteate = DateTime.Now;
            brand.Create_at = Cteate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime Update=DateTime.Now;
            brand.Update_at = Update.ToString("yyyy-MM-dd HH:mm:ss.fff");



            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlCommand insertCmd = new SqlCommand("INSERT INTO tbl_brand VALUES (@Brand_name, @Brand_desc, @Img,@Create_At, @Update_At)", conn);

                insertCmd.Parameters.AddWithValue("@Brand_name", brand.Brand_name);
                insertCmd.Parameters.AddWithValue("@Brand_desc", brand.Brand_description);
                insertCmd.Parameters.AddWithValue("@Img", brand.Img_name);
                insertCmd.Parameters.AddWithValue("@Create_At", brand.Create_at);
                insertCmd.Parameters.AddWithValue("@Update_At", brand.Update_at);

                try
                {

                    conn.Open();
                    insertCmd.ExecuteNonQuery();
                    inserted = true;
                }
                catch (Exception ex) {
                    inserted = false;
                
                }
            }
            return inserted;
        }



        public bool updateBrand(Brand brand)
        {
            bool isUpdate = false;

            DateTime update=DateTime.Now;
            brand.Update_at = update.ToString("yyyy-MM-dd HH:mm:ss.fff");

            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlCommand updateCmd = new SqlCommand("UPDATE tbl_brand SET brand_name=@Brand_name,img=@Img, brand_desction=@Brand_desc,create_at=@Create,update_at=@Update WHERE id=@Id", conn);
                updateCmd.Parameters.AddWithValue("@Brand_name", brand.Brand_name);
                updateCmd.Parameters.AddWithValue("@Brand_desc", brand.Brand_description);
                updateCmd.Parameters.AddWithValue("@Id", brand.Id);
                updateCmd.Parameters.AddWithValue("@Create", brand.Create_at);
                updateCmd.Parameters.AddWithValue("@Update", brand.Update_at);
                updateCmd.Parameters.AddWithValue("@Img", brand.Img_name);
;

                try
                {
                    conn.Open();
                   updateCmd.ExecuteNonQuery();
         isUpdate = true;

                }
                catch (Exception ex) { isUpdate= false; }

            }
            return isUpdate;
        }

        //public bool deleteBrand(int id) {
        //    bool delete = false;
        //    using (SqlConnection conn = new SqlConnection(_dbConnectionString)) {
        //        SqlCommand sqlCommand = new SqlCommand("DELETE FROM Brand_Tbl WHERE Id=@Id",conn);

        //        sqlCommand.Parameters.AddWithValue("@Id", id);
        //        try {
        //            conn.Open();
        //         int row = sqlCommand.ExecuteNonQuery();
        //            if (row > 0) {
        //            delete = true;
        //            }
        //        }catch (Exception ex) 
        //        { delete = false; }
        //    }
        //     return delete;
        //}





        //Category Data Service


        public List<Categories> getCateAll()
        {
            List<Categories> listCategory = new List<Categories>();
            using (SqlConnection con = new SqlConnection(_dbConnectionString)) {
                SqlCommand Cmd = new SqlCommand("SELECT * FROM category",con);
            con.Open(); 
                SqlDataReader rdrd=Cmd.ExecuteReader();
                while (rdrd.Read()) {

                    Categories categories = new Categories()
                    {
                        Id = Convert.ToInt32(rdrd["cate_id"]),
                        CategoriesName = rdrd["cate_name"].ToString()
                    };
                 listCategory.Add(categories);
                }
            }
            return listCategory;
        }

        public bool insertCate(Categories categories) {
            bool insert = false;
            using (SqlConnection connection = new SqlConnection(_dbConnectionString)) {
                SqlCommand cmd = new SqlCommand("INSERT INTO category VALUES (@Cate_name)",connection);
                cmd.Parameters.AddWithValue("@Cate_name", categories.CategoriesName);
                try
                {
                    connection.Open();
                   int row= cmd.ExecuteNonQuery();
                    if (row > 0) {
                    insert = true;
                    
                    }
                }
                catch (Exception ex)
                    
                {            insert = false;
                    Console.WriteLine(ex.Message);
                }
            }  return insert;
        }

        public Categories Find_cate(int id) {
            Categories find_id = new Categories();

            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM category WHERE cate_id=@Id",conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader rdrd = cmd.ExecuteReader();
                if (rdrd.Read())
                {
                     find_id= new Categories
                    {
                        Id = Convert.ToInt32(rdrd["cate_id"]),
                        CategoriesName =rdrd["cate_name"].ToString(),
                    };
                }
            }
            return find_id;
        }

        public bool updateCate(Categories cate) {
            bool isUpdate = false;

            using(SqlConnection conn =new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE category SET cate_name=@cate_name WHERE cate_id=@Id",conn);
                cmd.Parameters.AddWithValue("@cate_name",cate.CategoriesName);
                cmd.Parameters.AddWithValue("@Id",cate.Id);
                try{
                conn.Open();
                    cmd.ExecuteNonQuery();
                    isUpdate = true;

                }  catch(Exception ex)
                {       
                    isUpdate = false;
                } 
            }
            return isUpdate;
        }

        public bool Delete_cate(int id)
        {
            bool isDelete = false;
            using (SqlConnection con = new SqlConnection(_dbConnectionString)) {
                SqlCommand cmd = new SqlCommand("DELETE FROM category WHERE cate_id=@id",con);
                cmd.Parameters.AddWithValue("@Id",id);
                try
                {
                   con.Open();
                    cmd.ExecuteNonQuery();
                    isDelete = true;
                }
                catch (Exception ex) { 
                isDelete = false;
                }
            }
            return isDelete;
        }

        //Script Product 

//        public List<Products> getAllProducts()
//        {
//            List<Products> pro = new List<Products>();

//            using(SqlConnection con=new SqlConnection(_dbConnectionString))
//            {
//                SqlCommand cmd = new SqlCommand("SELECT * FROM products AS P LEFT JOIN tbl_brand AS B ON B.id=P.brand_id LEFT JOIN category AS C ON C.cate_id=P.cate_id", con);

//                con.Open();

//                SqlDataReader rdr = cmd.ExecuteReader();

//                while (rdr.Read())
//                {
//                    Products product = new Products
//                    {
//                        Id = Convert.ToInt16(rdr["id"]),
//                        Pro_name = rdr["pro_name"].ToString(),
//                        Qty = Convert.ToInt16(rdr["qty"]),
//                        Prices = Convert.ToDecimal(rdr["price"]),
//                        Cate_id = Convert.ToInt16(rdr["cate_id"]),
//                        Brand_id = rdr["brand_id"] as short? ?? 0,
//                        Brand_name = rdr["brand_name"].ToString(),
//                        Cate_name = rdr["cate_name"].ToString(),
//                    };
//                    pro.Add(product);
//                }
//            }
//            return pro;
//        }

//public bool addProducts(Products products)
//        {
//            bool isInsert=false;
//            using (SqlConnection connection = new SqlConnection(_dbConnectionString)) {
//                SqlCommand cmd = new SqlCommand("INSERT INTO products VALUES(@Pro_name,@Qty,@Prices,@Cate_id,@Brand_id,@Img)",connection);
//                cmd.Parameters.AddWithValue("@Pro_name",products.Pro_name);
//                cmd.Parameters.AddWithValue("@Qty",products.Qty);
//                cmd.Parameters.AddWithValue("@Prices",products.Prices);
//                cmd.Parameters.AddWithValue("@Cate_id",products.Cate_id);
//                cmd.Parameters.AddWithValue("@Brand_id",products.Brand_id);
//                cmd.Parameters.AddWithValue("@Img",products.ImgName);
//                try { 
//                    connection.Open();
//                    cmd.ExecuteNonQuery();
//                isInsert = true;
                
//                } catch (Exception ex)
//                { isInsert = false; }
//            }
//            return isInsert;
//        }


//        public Products findProduct(int id)
//        {
//            Products product = new Products();


//            using(SqlConnection con=new SqlConnection(_dbConnectionString))
//            {
//                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE id=@Id", con);
//                cmd.Parameters.AddWithValue("@Id", id);

//                    con.Open();
//                    SqlDataReader rdr= cmd.ExecuteReader();
           
//                    if (rdr.Read()) {
//                        product  = new Products
//                        {
//                            Id = Convert.ToInt16(rdr["id"]),
//                            Pro_name = rdr["pro_name"].ToString(),
//                            Qty = Convert.ToInt16(rdr["qty"]),
//                            Prices = Convert.ToDecimal(rdr["price"]),
//                            Cate_id = Convert.ToInt16(rdr["cate_id"]),
//                            Brand_id = rdr["brand_id"] as short? ?? 0,
//                            Brand_name = rdr["brand_name"].ToString(),
//                            Cate_name = rdr["cate_name"].ToString(),
//                        };
//                    };
//            }

//            return product;
//        }
    }
}
