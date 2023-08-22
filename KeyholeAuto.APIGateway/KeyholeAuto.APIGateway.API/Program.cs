using KeyholeAuto.APIGateway.API.Configuration;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// GraphQL Endpoints
builder.Services.AddSingleton(
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("GraphQL-APIGateway-Redis")
    ));
var graphQLServer = builder.Services.AddGraphQLServer()
                                    .AddRemoteSchemasFromRedis("KeyholeAuto", sp => sp.GetRequiredService<ConnectionMultiplexer>());

#if DEBUG
var endpointURIs = APIGatewayConfigurationHelper.GetLocalDevelopmentGraphQLConfiguration(builder.Configuration);
#endif
foreach (var keyValuePair in endpointURIs)
{
    builder.Services.AddHttpClient(keyValuePair.Key, c => c.BaseAddress = keyValuePair.Value);
    //graphQLServer = graphQLServer.AddRemoteSchema(keyValuePair.Key);
}

graphQLServer = graphQLServer.AddTypeExtensionsFromFile("./InventoryStitching.graphql");

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

app.Run();
