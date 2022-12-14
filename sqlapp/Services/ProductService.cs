using sqlapp.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnection()
        {

            return new SqlConnection(_configuration["SQLConnection"]);
        }

        public List<Product> GetProductsOrig()
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


        public async Task<List<Product>> GetProducts()
        {
            String FunctionURL = "https://appfunction2022w.azurewebsites.net/api/GetProducts?code=x0FRbJBBT8-yQCICxEhV8uIT3sd8jUZZFAwHbs_Ee5mlAzFuIYGadw==";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(FunctionURL);
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(content);
            }
        }

    }
}
