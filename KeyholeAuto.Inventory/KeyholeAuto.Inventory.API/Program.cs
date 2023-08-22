using KeyholeAuto.Inventory.API.Configuration;
using KeyholeAuto.Inventory.API.GraphQL;
using KeyholeAuto.Inventory.DAL;
using KeyholeAuto.Inventory.Service;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<InventoryDatabase>();
builder.Services.AddSingleton<InventoryService>();

// GraphQL
var graphQLServer = builder.Services.AddGraphQLServer().AddQueryType<InventoryQuery>();

var redisConnectionString = builder.Configuration.GetConnectionString("GraphQL-APIGateway-Redis");
if (!string.IsNullOrEmpty(redisConnectionString))
{
    builder.Services.AddSingleton(ConnectionMultiplexer.Connect(redisConnectionString));

    graphQLServer.InitializeOnStartup()
                 .PublishSchemaDefinition(c => c.SetName("inventory")
                     .PublishToRedis("KeyholeAuto", sp => sp.GetRequiredService<ConnectionMultiplexer>()));
}

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

app.MapGraphQL("/graphql");

#if DEBUG
var vehicles = InventoryConfigurationHelper.GetVehicles(builder.Configuration);
var inventoryService = app.Services.GetService<InventoryService>();
foreach (var vehicle in vehicles)
{
    inventoryService.AddVehicleToInventory(vehicle);
}
#endif

app.Run();
