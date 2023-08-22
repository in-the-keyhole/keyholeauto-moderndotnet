using KeyholeAuto.Financing.Model;

namespace KeyholeAuto.Financing.API.GraphQL
{
    public class FinancingQuery
    {
        public async Task<VehicleSaleHistoryModel> GetVehicleSaleHistory(string vin, VehicleSaleHistoryBatchLoader vehicleSaleHistoryBatchLoader) =>
            await vehicleSaleHistoryBatchLoader.LoadAsync(vin);
    }
}
