using MiProyecto003.Services;
using MiProyecto003;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

public class ProductService : IProductService
{
    private readonly HttpClient httpClient;
    private readonly JsonSerializerOptions options;
    public ProductService(HttpClient httpClient)
    {

        this.httpClient = httpClient;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    }

    public async Task<List<Product>> GetProducts()
    {
        var response = await httpClient.GetAsync("v1/products");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Product>>(content, options);
        }
        else
        {
            // Manejar el error de la llamada a la API
            throw new Exception("No se pudieron obtener los productos.");
        }
    }

    public async Task DeleteProduct(int productId)
    {
        var response = await httpClient.DeleteAsync($"v1/products/{productId}");

        if (!response.IsSuccessStatusCode)
        {
            // Manejar el error de la llamada a la API para eliminar el producto
            throw new Exception("No se pudo eliminar el producto.");
        }
    }
    public async Task Add(Product product)
    {
        var response = await httpClient.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task<List<Product>?> Get()
    {
        var response = await httpClient.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Product>>(content, options);
    }

}
