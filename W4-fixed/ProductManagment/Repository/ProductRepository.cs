using Microsoft.EntityFrameworkCore;
using ProductManagment.Data;
using ProductManagment.Entity;

namespace ProductManagment.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // Tạo mới sản phẩm
        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        
        // Lấy toàn bộ sản phẩm
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Xóa sản phẩm bằng Id
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        // Cập nhật sản phẩm bằng Id
        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }
            
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        // Lấy sản phẩm bằng Id
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Kiểm tra sản phẩm tồn tại chưa
        public async Task<bool> ProductExistAsync(Product product)
        {
            return await _context.Products.AnyAsync(p => p.Name.ToLower() == product.Name.ToLower());
        }

        // Phân trang và tìm kiếm sản phẩm
        public async Task<(int TotalCount, List<Product> Items)> GetPagedAsync(int pageIndex, int pageSize, string keyword)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword));
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalCount, items);
        }
    }
}
