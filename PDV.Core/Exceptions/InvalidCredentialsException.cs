using System;

namespace PDV.Core.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base("Credenciais inválidas.") { }
    }
}