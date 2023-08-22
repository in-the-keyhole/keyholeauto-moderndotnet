using KeyholeAuto.Financing.API.Configuration;
using KeyholeAuto.Financing.API.GraphQL;
using KeyholeAuto.Financing.DAL;
using KeyholeAuto.Financing.Service;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<FinancingDatabase>();
builder.Services.AddSingleton<FinancingService>();

// GraphQL
builder.Services.AddSingleton(
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("GraphQL-APIGateway-Redis")
    ));
builder.Services.AddGraphQLServer()
                .AddQueryType<FinancingQuery>()
                .InitializeOnStartup()
                .PublishSchemaDefinition(c => c.SetName("financing")
                                   .PublishToRedis("KeyholeAuto", sp => sp.GetRequiredService<ConnectionMultiplexer>()));

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
var vehicleSales = FinancingConfigurationHelper.GetVehicleSales(builder.Configuration);
var financingService = app.Services.GetService<FinancingService>();
foreach (var vehicleSale in vehicleSales)
{
    financingService.RecordSale(vehicleSale.SoldVehicle, vehicleSale.DownPayment, vehicleSale.Principal, vehicleSale.InterestRate, vehicleSale.LoanDurationInMonths, vehicleSale.Commission, vehicleSale.SoldBy, vehicleSale.SoldWhen);
}
#endif

app.Run();
