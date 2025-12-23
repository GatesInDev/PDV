namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {
        /// <summary>
        /// Deleta um usuário. Somente Administrador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        /// <exception cref="Exception">Usuário não encontrado ou erro ao deletar.</exception>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var user = await _authRepository.GetByIdAsync(id);

                if (user == null)
                    throw new Exception("Usuário não encontrado.");

                await _authRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar usuário: {ex.Message}");
            }
        }
    }
}