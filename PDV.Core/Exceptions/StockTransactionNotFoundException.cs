using System;

namespace PDV.Core.Exceptions
{
    public class StockTransactionNotFoundException : Exception
    {
        public StockTransactionNotFoundException(Guid id)
            : base($"Transação de estoque com ID {id} não encontrada.") { }
    }
}