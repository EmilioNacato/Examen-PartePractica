namespace MiProyecto003.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task DeleteProduct(int productId);
        Task Add(Product product);
    }
}
