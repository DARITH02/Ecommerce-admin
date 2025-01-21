using AdminPage.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;

namespace AdminPage.Data
{
    public class PcDbContext
    {
        private readonly string _dbContext;
        public PcDbContext(IConfiguration configuration) {
            _dbContext = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Computer> getPc()
        {
            List<Computer> pcs = new List<Computer>();

            using (SqlConnection connection = new SqlConnection(_dbContext)) {

                SqlCommand cmd = new SqlCommand("SELECT * FROM  detailPc AS D LEFT JOIN products AS P ON D.proId=P.id", connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read()) {

                    Computer pc = new Computer()
                    {
                        ProName = reader["pro_name"] .ToString(),
                        Processor = reader["processor"].ToString(),
                        Connectivity = reader["Connectivity"].ToString(),
                        Code= reader["Code"].ToString(),
                        Battery=Convert.ToInt16(reader["Battery"]),
                        Color= reader["color"].ToString(),
                        Display= reader["display"].ToString(),
                        Dis=Convert.ToInt16(reader["dis"]),
                        Graphic= reader["graphic"].ToString(),
                        InterfacePort= reader["InterfacePort"].ToString(),
                        Keyboard = reader["keyboard"].ToString(),
                        Os= reader["OS"].ToString(),
                        PcId=Convert.ToInt16(reader["pcId"]),
                        ProId=Convert.ToInt16(reader["proId"]),
                        Ram= reader["ram"].ToString(),
                        Rom= reader["rom"].ToString(),
                        Speaker= reader["Speaker"].ToString(),
                        Webcam= reader["Webcam"].ToString(),
                        Weight= reader["Weight"].ToString(),
                        CreateAt= reader["createAt"].ToString(),
                        UpdateAt= reader["updateAt"].ToString(),
                    };
                     pcs.Add(pc);  
                }
            }
            return pcs;
        }


        public List<Products> getProName()
        {
            List<Products> products = new List<Products>();
            using (SqlConnection con = new SqlConnection(_dbContext))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM products AS P LEFT JOIN category AS C ON C.cate_id=P.cate_id WHERE C.cate_name='Computers'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Products pc = new Products()
                    {
                        Id = Convert.ToInt16(reader["id"]),
                        Pro_name = reader["pro_name"].ToString()
                    };
                    products.Add(pc);
                }
            }
            return products;
        }





        public bool addNew(Computer computer) { 
                bool isInerted = false;

            DateTime Create = DateTime.Now;
            computer.CreateAt = Create.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime Update = DateTime.Now;
            computer.UpdateAt = Update.ToString("yyyy-MM-dd HH:mm:ss.fff");


            using (SqlConnection con = new SqlConnection(_dbContext)) {

                SqlCommand cmd = new SqlCommand(
            "INSERT INTO detailPc (ProId, Ram, Rom, Battery, Color, Weight, Connectivity, Os, Display, Processor, InterfacePort, Speaker, Webcam, Keyboard, Graphic, Dis, Code, CreateAt, UpdateAt) " +
            "VALUES (@ProId, @Ram, @Rom, @Battery, @Color, @Weight, @Connectivity, @Os, @Display, @Processor, @InterfacePort, @Speaker, @Webcam, @Keyboard, @Graphic, @Dis, @Code, @CreateAt, @UpdateAt)",con);


                cmd.Parameters.AddWithValue("@ProId", computer.ProId);
                cmd.Parameters.AddWithValue("@Ram", computer.Ram);
                cmd.Parameters.AddWithValue("@Rom", computer.Rom);
                cmd.Parameters.AddWithValue("@Battery", computer.Battery);
                cmd.Parameters.AddWithValue("@Color", computer.Color);
                cmd.Parameters.AddWithValue("@Weight", computer.Weight);
                cmd.Parameters.AddWithValue("@Connectivity", computer.Connectivity);
           
                cmd.Parameters.AddWithValue("@Os", computer.Os);
                cmd.Parameters.AddWithValue("@Display", computer.Display);
                cmd.Parameters.AddWithValue("@Processor", computer.Processor);       
                cmd.Parameters.AddWithValue("@InterfacePort", computer.InterfacePort);       
                cmd.Parameters.AddWithValue("@Speaker", computer.Speaker);       
                cmd.Parameters.AddWithValue("@Webcam", computer.Webcam);       
                cmd.Parameters.AddWithValue("@Keyboard", computer.Keyboard);       
                cmd.Parameters.AddWithValue("@Graphic", computer.Graphic);       
                cmd.Parameters.AddWithValue("@Dis", computer.Dis);       
                cmd.Parameters.AddWithValue("@Code", computer.Code);       
                cmd.Parameters.AddWithValue("@CreateAt", computer.CreateAt);       
                cmd.Parameters.AddWithValue("@UpdateAt", computer.UpdateAt);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isInerted = true;
                }
                catch (Exception ex) {
                    isInerted = false;
                }
            
            }
            return isInerted;
        }


        public Computer getById(int id)
        {

            Computer pcs = new Computer();

            using(SqlConnection con=new SqlConnection(_dbContext))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM  detailPc AS D LEFT JOIN products AS P ON D.pcId=P.id WHERE pcId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pcs = new Computer
                    {
                        ProName = reader["pro_name"].ToString() ,
                        Processor = reader["processor"].ToString(),
                        Connectivity = reader["Connectivity"].ToString(),
                        Code = reader["Code"].ToString(),
                        Battery = Convert.ToInt16(reader["Battery"]),
                        Color = reader["color"].ToString(),
                        Display = reader["display"].ToString(),
                        Dis = Convert.ToInt16(reader["dis"]),
                        Graphic = reader["graphic"].ToString(),
                        InterfacePort = reader["InterfacePort"].ToString(),
                        Keyboard = reader["keyboard"].ToString(),
                        Os = reader["OS"].ToString(),
                        PcId = Convert.ToInt16(reader["pcId"]),
                        ProId = Convert.ToInt16(reader["proId"]),
                        Ram = reader["ram"].ToString(),
                        Rom = reader["rom"].ToString(),
                        Speaker = reader["Speaker"].ToString(),
                        Webcam = reader["Webcam"].ToString(),
                        Weight = reader["Weight"].ToString(),
                        CreateAt = reader["createAt"].ToString(),
                        UpdateAt = reader["updateAt"].ToString(),
                    };
                }
            }
            return pcs;
        }


        public bool Update(Computer computer)
        {
            bool isUpdate = false;

            using(SqlConnection con=new SqlConnection(_dbContext))
            {
             
                DateTime Update = DateTime.Now;
                computer.UpdateAt = Update.ToString("yyyy-MM-dd HH:mm:ss.fff");



                SqlCommand cmd = new SqlCommand("UPDATE detailPc SET proId = @ProId,ram = @Ram,rom = @Rom,Battery = @Battery,color = @Color,Weight = @Weight," +
                    "Connectivity = @Connectivity,Os = @Os,display = @Display,processor = @Processor,interfaceport = @InterfacePort,Speaker = @Speaker,Webcam = @Webcam," +
                    "keyboard = @Keyboard,graphic = @Graphic,dis = @Dis ,Code= @Code, createAt = @CreateAt,updateAt = @UpdateAt WHERE pcId = @PcId", con);



                cmd.Parameters.AddWithValue("@PcId", computer.PcId);
                cmd.Parameters.AddWithValue("@ProId", computer.ProId);
                cmd.Parameters.AddWithValue("@Ram", computer.Ram);
                cmd.Parameters.AddWithValue("@Rom", computer.Rom);
                cmd.Parameters.AddWithValue("@Battery", computer.Battery);
                cmd.Parameters.AddWithValue("@Color", computer.Color);
                cmd.Parameters.AddWithValue("@Weight", computer.Weight);
                cmd.Parameters.AddWithValue("@Connectivity", computer.Connectivity);

                cmd.Parameters.AddWithValue("@Os", computer.Os);
                cmd.Parameters.AddWithValue("@Display", computer.Display);
                cmd.Parameters.AddWithValue("@Processor", computer.Processor);
                cmd.Parameters.AddWithValue("@InterfacePort", computer.InterfacePort);
                cmd.Parameters.AddWithValue("@Speaker", computer.Speaker);
                cmd.Parameters.AddWithValue("@Webcam", computer.Webcam);
                cmd.Parameters.AddWithValue("@Keyboard", computer.Keyboard);
                cmd.Parameters.AddWithValue("@Graphic", computer.Graphic);
                cmd.Parameters.AddWithValue("@Dis", computer.Dis);
                cmd.Parameters.AddWithValue("@Code", computer.Code);
                cmd.Parameters.AddWithValue("@CreateAt", computer.CreateAt);
                cmd.Parameters.AddWithValue("@UpdateAt", computer.UpdateAt);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isUpdate = true;
                }   catch(Exception ex)
                {
                    isUpdate = false;
                }

            }

            return isUpdate;
        }



        public bool Delete(int id)
        {
            bool isDelete = false;

            using(SqlConnection con=new SqlConnection(_dbContext))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM detailPc WHERE pcId=@Id",con);
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isDelete = true;
                }   catch(Exception ex)
                {
                    isDelete = false;
                }
            }

            return isDelete;

        }




    }
}
