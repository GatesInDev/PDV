using System;

namespace PDV.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
            : base($"Usuário '{username}' não encontrado.") { }
    }
}