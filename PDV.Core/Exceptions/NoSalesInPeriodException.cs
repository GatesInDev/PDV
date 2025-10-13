using System;

namespace PDV.Core.Exceptions
{
    public class NoSalesInPeriodException : Exception
    {
        public NoSalesInPeriodException(DateTime start, DateTime end)
            : base($"Não há vendas entre o período de {start} e {end}.") { }
    }
}