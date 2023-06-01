using Cccat10RefactoringAPI.Infra;
using Cccat10RefactoringAPI.Infra.Repositories;
using Cccat10RefactoringAPI.Infra.TypeHandlers;
using Cccat10RefactoringDomain.Repositories;
using Dapper;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

SQLHelper.ConnectionString = builder.Configuration.GetConnectionString("DataSource")!;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Configure Custom Type Handlers for Dapper
SqlMapper.AddTypeHandler(new CPFTypeHandler());
SqlMapper.AddTypeHandler(new SQLiteGuidTypeHandler());
SqlMapper.RemoveTypeMap(typeof(Guid));
SqlMapper.RemoveTypeMap(typeof(Guid?));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
