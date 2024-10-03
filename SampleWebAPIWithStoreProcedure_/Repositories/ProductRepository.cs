using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using SampleWebAPIWithStoreProcedure_.Interfaces;
using SampleWebAPIWithStoreProcedure_.Models;
using System.Data;

namespace SampleWebAPIWithStoreProcedure_.Repositories
{
    public class ProductRepository(IConfiguration config) : IProduct
    {

        private string connectionString = config.GetConnectionString("Default")!;

        public async Task DeleteProduct(int id)
        {
            using var connection = new SqlConnection(connectionString); 
            using var command = new SqlCommand("DeleteProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = new List<Product>();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("GetProducts", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                products.Add(new Product 
                { 
                
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                });
            }
            return products;

        }

        public async Task<Product> GetProductById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("GetProductById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            Product product_ = new();
            while (await reader.ReadAsync()) 
            {
                product_ = new()
                { 
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                };
            }
            return product_;
        }

        public async Task InsertProduct(Product product)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("InsertProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("UpdateProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
