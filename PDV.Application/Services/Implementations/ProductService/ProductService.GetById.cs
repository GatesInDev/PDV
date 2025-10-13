using PDV.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        /// <summary>
        /// Retorna um Produto pelo ID.
        /// </summary>
        /// <param name="id">Identificador unico do Produto.</param>
        /// <returns>O Objeto do produto especifico completo.</returns>
        /// <exception cref="Exception">Não foi possivel encontrar o produto.</exception>
        public async Task<ProductDetailsDTO> GetById(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                    throw new Exception("Produto não encontrado.");

                var map = _mapper.Map<ProductDetailsDTO>(product);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o produto: " + ex.Message);
            }
        }
    }
}
