using Microsoft.Extensions.DependencyInjection;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register the DistributedTransactionService
builder.Services.AddScoped<DistributedTransactionService>();


var apiSettings = builder.Configuration.GetSection("ApiSettings");

builder.Services.AddHttpClient("ProductServiceClient", client =>
{
    client.BaseAddress = new Uri(apiSettings.GetValue<string>("ProductServiceBaseUrl") ?? "");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("X-API-KEY", apiSettings.GetValue<string>("ProductServiceApiKey") ?? "");
});

builder.Services.AddHttpClient("InventoryServiceClient", client =>
{
    client.BaseAddress = new Uri(apiSettings.GetValue<string>("InventoryServiceBaseUrl") ?? "");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("X-API-KEY", apiSettings.GetValue<string>("InventoryServiceApiKey") ?? "");
});



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
