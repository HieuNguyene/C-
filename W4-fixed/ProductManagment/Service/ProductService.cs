using ProductManagment.DTOs;
using ProductManagment.DTOs.Request;
using ProductManagment.DTOs.Response;
using ProductManagment.Entity;
using ProductManagment.Repository;
using System.Collections.Generic;

namespace ProductManagment.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger  )
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        //Tạo sản phẩm 
        public async Task<ProductResponse> CreateProductAsync(ProductCreateRequest request)
        {
            _logger.LogInformation("Tạo sản phẩm mới");
            var p = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };
            if ( await _productRepository.ProductExistAsync(p))
            {
                throw new InvalidOperationException("Sản phầm này đã tồn tại!");
            }
            var saveProduct = await _productRepository.AddAsync(p);
            _logger.LogInformation("Đã tạo thành công {Name}",saveProduct.Name);
            return new ProductResponse()
            {
                Id = saveProduct.Id,
                Name = saveProduct.Name,
                Description=saveProduct.Description,
                Price=saveProduct.Price,
            };
        }

        // Xóa sản phẩm
        public async Task<bool> DeleteProductAsync(int id)
        {
            Product? p = await _productRepository.GetByIdAsync(id);
            if (p== null)
            {
                _logger.LogWarning("Không tìm thấy sản phẩm có {id}",id);
                throw new KeyNotFoundException($"Sản phẩm có {id} không tồn tại");
            }
            _logger.LogInformation($"Delete {id}");
            return  await _productRepository.DeleteAsync(id);
        }

        // Lấy toàn bộ danh sách sản phẩm
        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            _logger.LogInformation("Lấy danh sách sản phẩm");
            var products = await _productRepository.GetAllAsync();
            var response = new List<ProductResponse>();
            _logger.LogInformation("Lấy danh sách sản phẩm thành công");
            return products.Select(p => new ProductResponse()
            {
                Id =p.Id,
                Name=p.Name,
                Description=p.Description,
                Price=p.Price,
            }).ToList();
        }
        // Lấy sản phẩm bằng Id
        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var result  = await _productRepository.GetByIdAsync(id);
            if(result == null)
            {
                throw new KeyNotFoundException("Sản phẩm không tồn tại");
            }
            return new ProductResponse()
            {
                Id =result.Id,
                Name=result.Name,
                Description=result.Description,
                Price=result.Price,
            };
        }

        public async Task<ProductResponse> UpdateProductAsync(int id,ProductUpdateRequest request)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) {
                throw new KeyNotFoundException("Sản phẩm không tồn tại");
            }
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            var updateProduct = await _productRepository.UpdateAsync(id,product);
            if(updateProduct == null)
            {
                throw new InvalidOperationException("Cập nhật sản phẩm không thành công!");
            }
            return new ProductResponse()
            {
                Id = updateProduct.Id,
                Name = updateProduct.Name,
                Description = updateProduct.Description,
                Price = updateProduct.Price,
            };
        }
        public async Task<PagedResult<Product>> GetPagedAsync(ProductSearchRequest request)
        {
            if (request.PageIndex < 1) request.PageIndex = 1;
            if(request.PageSize >100 || request.PageSize<1) request.PageSize =10;

            var result = await _productRepository.GetPagedAsync(request.PageIndex,request.PageSize,request.KeyWord);
            

            return new PagedResult<Product>
            {
                TotalItems = result.TotalCount,
                Items = result.Items,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)request.PageSize)
            };
        }
    }
}
