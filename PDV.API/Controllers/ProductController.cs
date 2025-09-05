using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        #region GET Requisition

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

        #endregion

        #region POST Requisition

        [HttpPost]
        public async Task<ActionResult<CreateProductDTO>> PostProduct(CreateProductDTO dto)
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

        #endregion

        #region PUT/PATCH Requisition

        #endregion

        #region DELETE Requisition

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromId([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) 
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

    }
}
