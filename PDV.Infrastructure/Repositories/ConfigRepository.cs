using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly IDbContextFactory<AppDbContext> _context;
        public ConfigRepository(IDbContextFactory<AppDbContext> context)
        {
            _context = context;
        }

        public async Task<Config> GetConfigAsync()
        {
            using var context = await _context.CreateDbContextAsync();

            return await context.Configs.FirstOrDefaultAsync();
        }

        public async Task SaveConfigAsync(Config configEntrada)
        {
            using var context = await _context.CreateDbContextAsync();

            var configExistente = await context.Configs.FirstOrDefaultAsync();

            if (configExistente == null)
            {
                context.Configs.Add(configEntrada);
            }
            else
            {
                configEntrada.Id = configExistente.Id;
                context.Entry(configExistente).CurrentValues.SetValues(configEntrada);
            }

            await context.SaveChangesAsync();
        }
    }
}
