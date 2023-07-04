using Application.Interfaces;
using Application.UseCases;
using Hosting.Net.Helpers;
using Infraestructure.Commands;
using Infraestructure.Persistence;
using Infraestructure.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

connectionString = ConnectionHelper.GetConnectionString(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IServiceComanda, ServiceComanda>();
builder.Services.AddScoped<IServiceMercaderia, ServiceMercaderia>();
builder.Services.AddScoped<IServiceFormaEntrega, ServiceFormaEntrega>();
builder.Services.AddScoped<IServiceTipoMercaderia, ServiceTipoMercaderia>();
builder.Services.AddScoped<IServiceComandaMercaderia, ServiceComandaMercaderia>();

builder.Services.AddScoped<IQueryComanda, QueryComanda>();
builder.Services.AddScoped<IQueryMercaderia, QueryMercaderia>();
builder.Services.AddScoped<IQueryFormaEntrega, QueryFormaEntrega>();
builder.Services.AddScoped<IQueryTipoMercaderia, QueryTipoMercaderia>();
builder.Services.AddScoped<IQueryComandaMercaderia, QueryComandaMercaderia>();

builder.Services.AddScoped<ICommandComanda, CommandComanda>();
builder.Services.AddScoped<ICommandMercaderia, CommandMercaderia>();
builder.Services.AddScoped<ICommandFormaEntrega, CommandFormaEntrega>();
builder.Services.AddScoped<ICommandTipoMercaderia, CommandTipoMercaderia>();
builder.Services.AddScoped<ICommandComandaMercaderia, CommandComandaMercaderia>();

builder.Services.AddScoped<IServiceValidateMercaderia,  ServiceValidateMercaderia>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

await PrepDB.PrepPopulation(app);


app.Run();
