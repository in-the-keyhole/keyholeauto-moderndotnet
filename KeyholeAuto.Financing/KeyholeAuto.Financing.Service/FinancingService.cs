using KeyholeAuto.Financing.DAL;
using KeyholeAuto.Financing.Model;

namespace KeyholeAuto.Financing.Service
{
    public class FinancingService
    {
        private readonly FinancingDatabase financingDatabase;

        public FinancingService(FinancingDatabase financingDatabase)
        {
            this.financingDatabase = financingDatabase;
        }

        public VehicleSaleModel RecordSale(SoldVehicleModel soldVehicleModel, decimal downPayment, decimal principal, decimal interestRate, short loanDurationInMonths, decimal commission, string soldBy, DateTime soldWhen)
        {
            return financingDatabase.RecordSale(soldVehicleModel, downPayment, principal, interestRate, loanDurationInMonths, commission, soldBy, soldWhen);
        }

        public IQueryable<VehicleSaleModel> GetVehicleSalesAsQueryable() =>
            financingDatabase.GetVehicleSalesAsQueryable();

    }
}
