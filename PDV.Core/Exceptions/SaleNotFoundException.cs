using System;

namespace PDV.Core.Exceptions
{
    public class SaleNotFoundException : Exception
    {
        public SaleNotFoundException(Guid saleId)
            : base($"Venda com ID {saleId} não encontrada.") { }
    }
}