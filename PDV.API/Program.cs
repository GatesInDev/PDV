using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório
using PDV.Application.Services.Interfaces; // Para ter acesso as interfaces de serviço
using PDV.Application.Services.Implementations; // Para ter acesso as implementações de serviço
using PDV.Infrastructure.Data;  // Para ter acesso ao AppDbContext
using PDV.Infrastructure.Repositories; // Para ter acesso as implementações de repositório
using PDV.Application.Mappings; // Para ter acesso ao AutoMapper profiles

using Microsoft.EntityFrameworkCore; // Para ter acesso ao DbContext e DbSet

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockTransactionService, StockTransactionService>();
builder.Services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ICashService, CashService>();
builder.Services.AddScoped<ICashSessionRepository, CashSessionRepository>();


builder.Services.AddAutoMapper(typeof(StockProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}     

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
