using PDV.Application.DTOs.User;

namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {
        /// <summary>
        /// Atualiza um usuário existente. Somente Administrador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="updateUserDto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        /// <exception cref="Exception">Usuário não encontrado ou erro ao atualizar.</exception>
        public async Task UpdateAsync(Guid id, UpdateUserDTO updateUserDto)
        {
            try
            {
                var user = await _authRepository.GetByIdAsync(id);

                if (user == null)
                    throw new Exception("Usuário não encontrado.");

                // Verificar se o novo username já existe (se foi alterado)
                if (user.Username != updateUserDto.Username && 
                    await _authRepository.UsernameExistsAsync(updateUserDto.Username))
                    throw new Exception("Usuário com este nome de usuário já existe.");

                user.Username = updateUserDto.Username;
                user.Role = updateUserDto.Role;

                // Apenas atualizar senha se uma nova foi fornecida
                if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
                    user.Password = updateUserDto.Password;

                await _authRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar usuário: {ex.Message}");
            }
        }
    }
}