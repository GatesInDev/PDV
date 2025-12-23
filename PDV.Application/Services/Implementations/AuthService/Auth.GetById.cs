using PDV.Application.DTOs.User;
using AutoMapper;

namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {
        /// <summary>
        /// Retorna um usuário pelo seu ID. Somente Administrador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Objeto com os dados do usuário.</returns>
        /// <exception cref="Exception">Usuário não encontrado.</exception>
        public async Task<UserDetailsDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _authRepository.GetByIdAsync(id);

                if (user == null)
                    throw new Exception("Usuário não encontrado.");

                var map = _mapper.Map<UserDetailsDTO>(user);

                if (map == null)
                    throw new Exception("Erro ao mapear o usuário.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encontrar o usuário no banco de dados.", ex);
            }
        }
    }
}