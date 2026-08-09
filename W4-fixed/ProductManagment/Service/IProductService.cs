using ProductManagment.DTOs.Request;
using ProductManagment.DTOs.Response;
using ProductManagment.DTOs;
using ProductManagment.Entity;

namespace ProductManagment.Service
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> CreateProductAsync(ProductCreateRequest request);
        Task<ProductResponse> UpdateProductAsync(int id,ProductUpdateRequest request);
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<bool> DeleteProductAsync(int id);
        Task<PagedResult<Product>> GetPagedAsync(ProductSearchRequest request);
    }
}
