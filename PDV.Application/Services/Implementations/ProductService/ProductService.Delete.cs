using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        /// <summary>
        /// Desativa um Produto.
        /// </summary>
        /// <param name="id">Identificar do produto a ser desabilitado.</param>
        /// <returns>Valor booleano, True = Success Operation/False = Fail Operation.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);

                if (existingProduct == null)
                    throw new Exception("Produto não encontrado.");

                if (existingProduct.IsActive == false)
                    throw new Exception("Produto já desabilitado.");

                existingProduct.IsActive = false;
                existingProduct.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(existingProduct);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desativar o produto no banco de dados: " + ex.Message);
            }
        }
    }
}
