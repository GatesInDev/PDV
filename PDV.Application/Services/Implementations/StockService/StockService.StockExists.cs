namespace PDV.Application.Services.Implementations
{
    public partial class StockService
    {
        /// <summary>
        /// Verifica se o estoque existe para o produto.
        /// </summary>
        /// <param name="productId">Produto cujo estoque será validado.</param>
        /// <returns>True/False</returns>
        public Task<bool> StockExists(Guid productId)
        {
            return _repository.StockExistsAsync(productId);
        }
    }
}
