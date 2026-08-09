
using ProductManagment.Entity;

namespace ProductManagment.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(); // Lấy danh sách sản phẩm
        Task<Product> AddAsync(Product product);// Thêm sản phẩm mới
        Task<bool> DeleteAsync(int id);// Xóa sản phẩm bằng Id
        Task<Product?> UpdateAsync(int id, Product product); // Cập nhật sản phẩm bằng Id
        Task<Product?> GetByIdAsync(int id); // Lấy sản phẩm bằng Id
        Task<bool> ProductExistAsync(Product product); // Kiểm tra sản phẩm đã tồn tại chưa
        Task<(int TotalCount,List<Product> Items)> GetPagedAsync(int page, int pageSize,string keyword);
    }
}
