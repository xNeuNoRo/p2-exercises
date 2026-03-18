using Repository.Context;
using Repository.Interfaces;
using Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<BibliotecaContext>(builder.Configuration.GetConnectionString("AppConnection"));

//Configurando el servicio Repositorio generico
builder.Services.AddScoped(typeof(IRepository<>),typeof(RepositoryServices<>));

//Configurando los servicios normales
//builder.Services.AddScoped<IAutor,AutorServices>();
//builder.Services.AddScoped<ICategoria, CategoriaServices>();

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
