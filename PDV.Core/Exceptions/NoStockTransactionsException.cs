using System;

namespace PDV.Core.Exceptions
{
    public class NoStockTransactionsException : Exception
    {
        public NoStockTransactionsException()
            : base("Sem transações de estoque.") { }
    }
}