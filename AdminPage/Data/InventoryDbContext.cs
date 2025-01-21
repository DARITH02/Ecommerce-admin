using AdminPage.Models;
using Microsoft.Data.SqlClient;

namespace AdminPage.Data
{
    public class InventoryDbContext
    {

        private readonly string _dbConnectionString;

        public InventoryDbContext(IConfiguration configuration)
        {
            _dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        }


        public List<Inventory> getInventories() { 
        
                List<Inventory> listInventory = new List<Inventory>();

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM inventory",connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read()) {

                    Inventory item = new Inventory
                    {
                        Id = Convert.ToInt16(reader["invetryID"]) ,
                        Qty = Convert.ToInt16(reader["qty"]) ,

                    };
                    listInventory.Add(item);
                }

            }
                return listInventory;
        }



    }
}
