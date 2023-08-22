using KeyholeAuto.Financing.Model;
using KeyholeAuto.Financing.Service;
using System.Linq;

namespace KeyholeAuto.Financing.API.GraphQL
{
    public class VehicleSaleHistoryBatchLoader : BatchDataLoader<string, VehicleSaleHistoryModel>
    {
        private readonly FinancingService financingService;

        public VehicleSaleHistoryBatchLoader(FinancingService financingService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            this.financingService = financingService;
        }

        protected override async Task<IReadOnlyDictionary<string, VehicleSaleHistoryModel>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var vehicleSalesByVin = financingService.GetVehicleSalesAsQueryable().Where(vs => keys.Contains(vs.SoldVehicle.VIN)).GroupBy(g => g.SoldVehicle.VIN).ToDictionary(k => k.Key, v => v);

            return keys.ToDictionary(k => k, v =>
            {
                if (vehicleSalesByVin.ContainsKey(v))
                {
                    return new VehicleSaleHistoryModel()
                    {
                        VehicleSales = vehicleSalesByVin[v].Select(x => x)
                    };
                }
                else
                {
                    return new VehicleSaleHistoryModel()
                    {
                        VehicleSales = new List<VehicleSaleModel>()
                    };
                }
            });
        }
    }
}
