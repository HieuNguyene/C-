
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagment.Entity;
using System.Data;

namespace ProductManagment.Repository
{
    public class MockProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new List<Product>();
        static MockProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Dell XPS 15", Description = "Laptop văn phòng mỏng nhẹ", Price = 1500 },
                new Product { Id = 2, Name = "iPhone 15 Pro Max", Description = "Điện thoại Apple 256GB", Price = 1200 },
                new Product { Id = 3, Name = "Bàn phím cơ Epomaker", Description = "Bàn phím cơ không dây layout 75%", Price = 85 },
                new Product { Id = 4, Name = "Router Wifi 6 TP-Link", Description = "Bộ định tuyến không dây tốc độ cao", Price = 60 },
                new Product { Id = 5, Name = "Màn hình LG UltraGear", Description = "Màn hình Gaming 27 inch 144Hz", Price = 350 },
                new Product { Id = 6, Name = "Tai nghe Sony WH-1000XM5", Description = "Tai nghe chụp tai chống ồn", Price = 300 },
                new Product { Id = 7, Name = "Chuột Logitech MX Master 3S", Description = "Chuột công thái học cho Coder", Price = 99 },
                new Product { Id = 8, Name = "MacBook Pro M3", Description = "Laptop Apple 14 inch", Price = 2000 },
                new Product { Id = 9, Name = "Samsung Galaxy S24 Ultra", Description = "Điện thoại Android Flagship", Price = 1300 },
                new Product { Id = 10, Name = "iPad Air 5", Description = "Máy tính bảng Apple", Price = 600 },
                new Product { Id = 11, Name = "Bàn nâng hạ thông minh", Description = "Bàn làm việc đứng bảo vệ cột sống", Price = 400 },
                new Product { Id = 12, Name = "Switch mạng Cisco 8 cổng", Description = "Thiết bị chia mạng tốc độ Gigabit", Price = 120 }
            };

            // 3. Cập nhật lại ID hiện tại.
            // Vì chúng ta đã có 12 sản phẩm, nên ID của sản phẩm được thêm mới tiếp theo phải là 13
            _currentId = 13;
        }
        private static int _currentId = 1;

        // Tạo mới sản phẩm
        public async Task<Product> AddAsync(Product product)
        {
            await Task.Delay(1000);// Giả delay
            product.Id = _currentId++;
            _products.Add(product);
            return product;
        }
        
        // Lấy toàn bộ sản phẩm
        public async Task<List<Product>> GetAllAsync()
        {
            await Task.Delay(1000);// Giả delay

            return _products.ToList();
        }

        // Xóa sản phẩm bằng Id
        public async Task<bool> DeleteAsync(int id) {
            await Task.Delay(1000);// Giả delay
            Product? product = _products.FirstOrDefault(p => p.Id==id);
            if (product == null) {
                return false;
            }
            return _products.Remove(product);
        }

        // Cập nhật sản phẩm bằng Id
        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            await Task.Delay(1000);// Giả delay
            Product? result = _products.FirstOrDefault(p =>p.Id==id);
            if (result == null)
            {
                return null;
            }
            result.Name = product.Name;
            result.Description = product.Description;
            result.Price = product.Price;
            return result;
        }

        // Lấy sản phẩm bằng Id
        public async Task<Product?> GetByIdAsync(int id)
        {
            await Task.Delay(1000);// Giả delay
            return _products.FirstOrDefault(P => P.Id == id);
        }
        // Kiểm tra sản phẩm tồn tại chưa
        public async Task<bool> ProductExistAsync(Product product)
        {
            await Task.Delay(1000);// Giả delay
            return _products.Any(p => p.Name.Equals(product.Name,StringComparison.OrdinalIgnoreCase));
        }
       public async Task<(int TotalCount,List<Product> Items)> GetPagedAsync(int pageIndex, int pageSize,string keyword)
        {
            var query =  _products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword));
            }
            int totalCount =  query.Count();

            var items =  query
                .Skip((pageIndex - 1) * pageSize) 
                .Take(pageSize)
                .ToList();
            return (totalCount, items);
        }
    }
}
