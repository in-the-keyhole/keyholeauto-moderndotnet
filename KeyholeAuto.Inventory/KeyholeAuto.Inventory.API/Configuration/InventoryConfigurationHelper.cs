using KeyholeAuto.Inventory.Model;

namespace KeyholeAuto.Inventory.API.Configuration
{
    public class InventoryConfigurationHelper
    {
        public static IEnumerable<VehicleModel> GetVehicles(IConfiguration configuration)
        {
            return configuration.GetSection("Inventory").GetSection("Vehicles").GetChildren().Select(vehicleConfigurationSection =>
            {
                var vehicleConfigurationDictionary = vehicleConfigurationSection.GetChildren().ToDictionary(k => k.Key, v => (object)v.Value);

                return new VehicleModel()
                {
                    Id = Guid.Parse((string)vehicleConfigurationDictionary["Id"]),
                    VIN = (string)vehicleConfigurationDictionary["VIN"],
                    Make = (string)vehicleConfigurationDictionary["Make"],
                    Model = (string)vehicleConfigurationDictionary["Model"],
                    Trim = (string)vehicleConfigurationDictionary["Trim"],
                    Year = short.Parse((string)vehicleConfigurationDictionary["Year"]),
                    MileageReadouts = vehicleConfigurationSection.GetSection("MileageReadouts").GetChildren().ToDictionary(k => DateTime.Parse(k.Key), v => int.Parse(v.Value)),
                    Category = (string)vehicleConfigurationDictionary["Category"]
                };
            });
        }
    }
}
