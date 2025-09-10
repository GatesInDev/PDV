using AutoMapper;
using PDV.Application.DTOs.Product;
using PDV.Core.Entities;
using System.Threading.Tasks;

public class ProductService : IProductService
    {
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _repository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDetailsDTO> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new Exception("Produto não encontrado.");
        }

        return _mapper.Map<ProductDetailsDTO>(product);
    }

    public async Task<Guid> CreateAsync(CreateProductDTO dto)
    {
        if (await _repository.SkuExistsAsync(dto.Sku))
        {
            throw new Exception("SKU já existe.");
        }

        var product = _mapper.Map<Product>(dto);

        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;

        try
        {
            await _repository.AddAsync(product);
            return product.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar o produto no banco de dados.", ex);
        }

    }

}
