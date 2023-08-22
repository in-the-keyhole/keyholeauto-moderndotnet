namespace KeyholeAuto.APIGateway.API.Configuration
{
    public class APIGatewayConfigurationHelper
    {
        public static IDictionary<string, Uri> GetLocalDevelopmentGraphQLConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection("GraphQL").GetSection("Endpoints").GetChildren().ToDictionary(k => k.Key, v => new Uri(v.Value));
        }
    }
}
