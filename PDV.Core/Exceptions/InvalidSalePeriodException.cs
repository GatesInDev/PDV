using System;

namespace PDV.Core.Exceptions
{
    public class InvalidSalePeriodException : Exception
    {
        public InvalidSalePeriodException(DateTime start, DateTime end)
            : base($"A data inicial {start} não deve ser maior que a final {end}.") { }
    }
}