using AdminPage.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace AdminPage.Data
{
    public class DetailPhoneDbContext
    {
        private readonly string _connectionString;

        public DetailPhoneDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public List<DetailPhone> getDetailPhones()
        {
            List<DetailPhone> items = new List<DetailPhone>();
            using (SqlConnection con = new SqlConnection(_connectionString)) { 
                SqlCommand cmd = new SqlCommand("SELECT * FROM detail_mobile AS D LEFT JOIN products AS P ON P.id=D.proID", con);
                   
            
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    var item = new DetailPhone()
                    {
                        Id = Convert.ToInt32(reader["detailID"]),
                        ProId = Convert.ToInt32(reader["proID"]),
                        Color = reader["color"].ToString(),
                        Ram =Convert.ToInt16(reader["ram"])     ,
                        Rom = Convert.ToInt16(reader["rom"]  ),
                        Camera = Convert.ToInt16(reader["camera"]),
                        Display = Convert.ToInt16(reader["display"] ),
                        Discount =Convert.ToInt32(reader["discount"]),
                        Bettery = Convert.ToInt16(reader["battery"]),
                        CreateAt =reader["CreatedAt"].ToString(),
                        UpdateAt =reader["UpdatedAt"].ToString(),
                        ProName =reader["pro_name"].ToString(),
                    };
                       items.Add(item);
                }
            }
            return items;
        }

        public bool infoPhone(DetailPhone phones) {
            bool isInsert = false;
            DateTime UpdateAt = DateTime.Now;
            phones.UpdateAt= UpdateAt.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime Create = DateTime.Now;
            phones.CreateAt = Create.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime currentTimestamp = DateTime.Now;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO detail_mobile VALUES(@ProId,@Dis,@Color,@Ram,@Rom,@Camera,@Bettery,@Display,@CreateAt,@UpdateAt)", con);

                cmd.Parameters.AddWithValue("@Id", phones.Id);
                cmd.Parameters.AddWithValue("@ProId", phones.ProId);
                cmd.Parameters.AddWithValue("@Dis", phones.Discount);
                cmd.Parameters.AddWithValue("@Color", phones.Color);
                cmd.Parameters.AddWithValue("@Display", phones.Display);
                cmd.Parameters.AddWithValue("@Ram", phones.Ram);
                cmd.Parameters.AddWithValue("@Rom", phones.Rom);
                cmd.Parameters.AddWithValue("@Camera", phones.Camera);
                cmd.Parameters.AddWithValue("@Bettery", phones.Bettery);
                cmd.Parameters.AddWithValue("@CreateAt",phones.CreateAt);
                cmd.Parameters.AddWithValue("@UpdateAt", phones.UpdateAt);
        
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isInsert = true;
                }
                catch
                (Exception ex)
                {
                    isInsert = false;
                }
            }
            return isInsert;
        }


        public DetailPhone findId(int id)
        {
            DetailPhone find = new DetailPhone();
            using(SqlConnection con=new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM detail_mobile AS D LEFT JOIN products AS P ON P.id=D.proID WHERE D.detailID=@Id",con);

                    cmd.Parameters.AddWithValue ("@Id", id);

                try { 
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) { 
                          find =new DetailPhone{
                          Id = Convert.ToInt16(reader["detailID"]),
                          ProId = Convert.ToInt16(reader["proID"]),
                          Discount = Convert.ToInt16(reader["discount"]),
                          Display = Convert.ToInt16(reader["display"]),
                         Ram= Convert.ToInt16(reader["ram"]),
                          Rom= Convert.ToInt16(reader["rom"]),
                          Camera= Convert.ToInt16(reader["camera"]),
                          Bettery= Convert.ToInt16(reader["battery"]),
                          Color= reader["color"].ToString(),
                          ProName= reader["pro_name"].ToString(),
                          UpdateAt= reader["UpdatedAt"].ToString(),
                              CreateAt = reader["CreatedAt"].ToString(),
                          };
                    }
                
                }
                catch (Exception ex) { }

            }

            return find;
        }





        public bool Update(DetailPhone phones)
        {
            bool isInsert = false;
            DateTime UpdateAt = DateTime.Now;
            phones.UpdateAt = UpdateAt.ToString("yyyy-MM-dd HH:mm:ss.fff");
         
            DateTime currentTimestamp = DateTime.Now;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE detail_mobile SET proID=@ProId,discount=@Dis,color=@Color,ram=@Ram,rom=@Rom,camera=@Camera,battery=@Bettery,display=@Display,UpdatedAt=@UpdateAt,CreatedAt=@CreateAt WHERE detailID=@Id", con);

                cmd.Parameters.AddWithValue("@Id", phones.Id);
                cmd.Parameters.AddWithValue("@ProId", phones.ProId);
                cmd.Parameters.AddWithValue("@Dis", phones.Discount);
                cmd.Parameters.AddWithValue("@Color", phones.Color);
                cmd.Parameters.AddWithValue("@Display", phones.Display);
                cmd.Parameters.AddWithValue("@Ram", phones.Ram);
                cmd.Parameters.AddWithValue("@Rom", phones.Rom);
                cmd.Parameters.AddWithValue("@Camera", phones.Camera);
                cmd.Parameters.AddWithValue("@Bettery", phones.Bettery);
                cmd.Parameters.AddWithValue("@CreateAt", phones.CreateAt);
                cmd.Parameters.AddWithValue("@UpdateAt", phones.UpdateAt);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isInsert = true;
                }
                catch
                (Exception ex)
                {
                    isInsert = false;
                }
            }
            return isInsert;
        }

        public bool Delete(int id)
        {
            bool isDelete = false;

            using(SqlConnection con=new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM detail_mobile WHERE detailID=@Id",con);
                cmd.Parameters.AddWithValue("@Id", id);

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


        public List<DetailPhone> search(string proName) {
                   List<DetailPhone> items = new List<DetailPhone>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM detail_mobile AS D LEFT JOIN products AS P ON P.id=D.proID WHERE P.pro_name LIKE @ProName", con);

                cmd.Parameters.AddWithValue("@ProName","%"+proName+"%");
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new DetailPhone()
                    {
                        Id = Convert.ToInt32(reader["detailID"]),
                        ProId = Convert.ToInt32(reader["proID"]),
                        Color = reader["color"].ToString(),
                        Ram = Convert.ToInt16(reader["ram"]),
                        Rom = Convert.ToInt16(reader["rom"]),
                        Camera = Convert.ToInt16(reader["camera"]),
                        Display = Convert.ToInt16(reader["display"]),
                        Discount = Convert.ToInt32(reader["discount"]),
                        Bettery = Convert.ToInt16(reader["battery"]),
                        CreateAt = reader["CreatedAt"].ToString(),
                        UpdateAt = reader["UpdatedAt"].ToString(),
                        ProName = reader["pro_name"].ToString(),
                    };
                    items.Add(item);
                }
            }
            return items;
        }


        public List<DetailPhone> searchByBrand(string brand)
        {
            List<DetailPhone> items = new List<DetailPhone>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM detail_mobile AS D LEFT JOIN products AS P ON D.proID=P.id LEFT JOIN tbl_brand AS B ON B.id=P.brand_id WHERE B.brand_name LIKE @Brand", con);

                cmd.Parameters.AddWithValue("@Brand", "%"+brand+"%");

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new DetailPhone()
                    {
                        Id = Convert.ToInt32(reader["detailID"]),
                        ProId = Convert.ToInt32(reader["proID"]),
                        Color = reader["color"].ToString(),
                        Ram = Convert.ToInt16(reader["ram"]),
                        Rom = Convert.ToInt16(reader["rom"]),
                        Camera = Convert.ToInt16(reader["camera"]),
                        Display = Convert.ToInt16(reader["display"]),
                        Discount = Convert.ToInt32(reader["discount"]),
                        Bettery = Convert.ToInt16(reader["battery"]),
                        CreateAt = reader["CreatedAt"].ToString(),
                        UpdateAt = reader["UpdatedAt"].ToString(),
                        ProName = reader["pro_name"].ToString(),
                    };
                    items.Add(item);
                }

            }



                return items;
        }

    }
}
