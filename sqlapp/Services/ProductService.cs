using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration _configuration;

        ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQlConnection"));

        }


        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _product_list = new List<Product>();

            string statement = "Select ProductID, ProductName, Quantity from products";

            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };
                    _product_list.Add(product);
                }
                conn.Close();
                return _product_list;
            }

        }

    }
}
