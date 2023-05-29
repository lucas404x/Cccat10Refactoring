using Cccat10RefactoringAPI.Infra;
using Cccat10RefactoringAPI.Infra.Repositories;
using Cccat10RefactoringDomain.Repositories;
using Dapper;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

SQLHelper.ConnectionString = builder.Configuration.GetConnectionString("DataSource")!;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
// builder.Services.AddSqlite()

// Make Dapper convert String to GUID correctly.
SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
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

public class SqliteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        if (value is Guid)
        {
            return (Guid)value;
        }
        return new Guid((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value.ToString();
    }
}