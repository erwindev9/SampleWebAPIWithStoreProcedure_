using SampleWebAPIWithStoreProcedure_.Models;

namespace SampleWebAPIWithStoreProcedure_.Interfaces
{
    public interface IProduct
    {
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task DeleteProduct(int id);

    }
}
