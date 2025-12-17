using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CategoryService
    {
        public async Task Delete(int id)
        {
            var existingCategory = await _repository.GetByIdAsync(id);
            if (existingCategory == null) 
                throw new Exception("Categoria não encontrada.");

            existingCategory.IsActive = false;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existingCategory);
        }
    }
}
