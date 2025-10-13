using System;

namespace PDV.Core.Exceptions
{
    public class StockAlreadyExistsException : Exception
    {
        public StockAlreadyExistsException(Guid productId)
            : base($"Já existe estoque para o produto {productId}.") { }
    }
}