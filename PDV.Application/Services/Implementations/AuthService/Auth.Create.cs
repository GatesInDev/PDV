using PDV.Application.DTOs.User;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {
        /// <summary>
        /// Cria um novo usuário. Somente Administrador.
        /// </summary>
        /// <param name="createUserDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do usuário criado.</returns>
        /// <exception cref="Exception">Erro ao criar o usuário.</exception>
        public async Task<Guid> CreateAsync(CreateUserDTO createUserDto)
        {
            try
            {
                if (await _authRepository.UsernameExistsAsync(createUserDto.Username))
                    throw new Exception("Usuário com este nome de usuário já existe.");

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Username = createUserDto.Username,
                    Password = createUserDto.Password,
                    Role = createUserDto.Role
                };

                await _authRepository.CreateAsync(user);

                return user.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}");
            }
        }
    }
}