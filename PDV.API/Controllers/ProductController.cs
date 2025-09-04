using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDV.Application.DTOs.Product;
using PDV.Core.Entities;
using PDV.Infrastructure.Data;
using System.Runtime.CompilerServices;

namespace PDV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var productExists = await _context.Products.AnyAsync();
            if (!productExists)
            {
                return BadRequest("Não existem produtos registrados.");
            }

            var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();

            var productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDTO>> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            var dto = _mapper.Map<ProductDetailsDTO>(product);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest($"Categoria com ID {dto.CategoryId} não existe.");
            }

            var product = _mapper.Map<Product>(dto);

            product.CreatedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

    }
}
