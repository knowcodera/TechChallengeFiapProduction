using ProductionApi.Data;
using ProductionApi.Repositories;
using ProductionApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductionContext>();

builder.Services.AddSingleton<RabbitMQConnection>();
builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

builder.Services.AddScoped<IProductionRepository, ProductionRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.Run();
}

app.Run("http://*:8580");
