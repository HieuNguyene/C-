using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagment.DTOs.Request;
using ProductManagment.DTOs.Response;
using ProductManagment.Service;
using ProductManagment.DTOs;

namespace ProductManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAllAsync()
        {
            var products = await _service.GetAllProductsAsync();
            return products;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById([FromRoute] int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductCreateRequest request)
        {
            var product = await _service.CreateProductAsync(request);
            return product;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateById(int id, [FromBody] ProductUpdateRequest request)
        {
            var p = await _service.UpdateProductAsync(id, request);
            return Ok(p);

        }
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<ProductResponse>>> SearchAsync([FromQuery] ProductSearchRequest request)
        {
            var p = await _service.GetPagedAsync(request);

            return Ok(p);
        }
    }
}
