using KeyholeAuto.Financing.Model;

namespace KeyholeAuto.Financing.DAL
{
    public class FinancingDatabase
    {

        private readonly IDictionary<Guid, VehicleSaleModel> saleHistory = new Dictionary<Guid, VehicleSaleModel>();

        public VehicleSaleModel RecordSale(SoldVehicleModel soldVehicleModel, decimal downPayment, decimal principal, decimal interestRate, short loanDurationInMonths, decimal commission, string soldBy, DateTime soldWhen)
        {
            var vehicleSale = new VehicleSaleModel()
            {
                Id = Guid.NewGuid(),
                SoldVehicle = soldVehicleModel,
                DownPayment = downPayment,
                Principal = principal,
                InterestRate = interestRate,
                LoanDurationInMonths = loanDurationInMonths,
                Commission = commission,
                SoldBy = soldBy,
                SoldWhen = soldWhen
            };

            saleHistory[vehicleSale.Id] = vehicleSale;

            return vehicleSale;
        }

        public IQueryable<VehicleSaleModel> GetVehicleSalesAsQueryable() => saleHistory.Values.AsQueryable();
    }
}
