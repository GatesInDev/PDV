using PDV.Core.Entities; // Para ter acesso a entidade Category
using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório
using PDV.Application.DTOs.Category; // Para ter acesso as DTOs de Category
using PDV.Application.Services.Interfaces;  // Para ter acesso a interface ICategoryService

using AutoMapper; // Para ter acesso ao AutoMapper

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações relacionadas a categorias.
    /// </summary>
    public partial class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de categoria.
        /// </summary>
        /// <param name="categoryRepository">Repositório de categoria.</param>
        /// <param name="mapper">Acesso ao AutoMapper</param>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = categoryRepository;
            _mapper = mapper;
        }
    }
}

