using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        /// <summary>
        /// Atualiza um Produto existente.
        /// </summary>
        /// <param name="id">Identificador do produto a ser atualizado.</param>
        /// <param name="dto">Objeto com os dados para a atualização</param>
        /// <returns>Identificar do produto Atualizado.</returns>
        /// <exception cref="Exception">Erro ao atualizar o produto.</exception>
        public async Task<Guid> Update(Guid id, UpdateProductDTO dto)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);

                if (existingProduct == null)
                    throw new Exception("Produto não encontrado.");

                if (!existingProduct.IsActive)
                    throw new Exception("Produto está desativado e não pode ser atualizado.");

                if (existingProduct.Sku != dto.Sku && await _productRepository.SkuExistsAsync(dto.Sku))
                    throw new Exception("SKU já existe.");

                _mapper.Map(dto, existingProduct);

                existingProduct.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(existingProduct);

                return existingProduct.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto no banco de dados: " + ex.Message);
            }
        }
    }
}
