using System;

namespace PDV.Core.Exceptions
{
    public class StockNotFoundException : Exception
    {
        public StockNotFoundException(Guid productId)
            : base($"Estoque não encontrado para o produto {productId}.") { }
    }
}