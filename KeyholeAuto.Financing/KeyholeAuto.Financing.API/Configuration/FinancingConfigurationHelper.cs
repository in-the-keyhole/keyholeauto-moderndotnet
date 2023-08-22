using KeyholeAuto.Financing.Model;

namespace KeyholeAuto.Financing.API.Configuration
{
    public class FinancingConfigurationHelper
    {
        public static IEnumerable<VehicleSaleModel> GetVehicleSales(IConfiguration configuration)
        {
            return configuration.GetSection("Financing").GetSection("VehicleSales").GetChildren().Select(vehicleSaleConfigurationSection =>
            {
                var vehicleSaleConfigurationDictionary = vehicleSaleConfigurationSection.GetChildren().ToDictionary(k => k.Key, v => (object)v.Value);
                var soldVehicleConfigurationDictionary = vehicleSaleConfigurationSection.GetSection("SoldVehicle").GetChildren().ToDictionary(k => k.Key, v => (object)v.Value);

                return new VehicleSaleModel()
                {
                    SoldVehicle = new SoldVehicleModel()
                    {
                        Id = Guid.Parse((string)soldVehicleConfigurationDictionary["Id"]),
                        VIN = (string)soldVehicleConfigurationDictionary["VIN"],
                        Make = (string)soldVehicleConfigurationDictionary["Make"],
                        Model = (string)soldVehicleConfigurationDictionary["Model"],
                        Trim = (string)soldVehicleConfigurationDictionary["Trim"],
                        Year = short.Parse((string)soldVehicleConfigurationDictionary["Year"]),
                        CurrentMileage = int.Parse((string)soldVehicleConfigurationDictionary["CurrentMileage"]),
                        Category = (string)soldVehicleConfigurationDictionary["Category"]
                    },
                    SoldWhen = DateTime.Parse((string)vehicleSaleConfigurationDictionary["SoldWhen"]),
                    SoldBy = (string)vehicleSaleConfigurationDictionary["SoldBy"],
                    DownPayment = decimal.Parse((string)vehicleSaleConfigurationDictionary["DownPayment"]),
                    Principal = decimal.Parse((string)vehicleSaleConfigurationDictionary["Principal"]),
                    InterestRate = decimal.Parse((string)vehicleSaleConfigurationDictionary["InterestRate"]),
                    LoanDurationInMonths = short.Parse((string)vehicleSaleConfigurationDictionary["LoanDurationInMonths"]),
                    Commission = decimal.Parse((string)vehicleSaleConfigurationDictionary["Commission"])
                };
            });
        }
    }
}
