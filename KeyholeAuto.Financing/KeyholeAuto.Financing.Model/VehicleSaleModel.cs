namespace KeyholeAuto.Financing.Model
{
    public class VehicleSaleModel
    {
        public Guid Id { get; set; }

        public SoldVehicleModel SoldVehicle { get; set; }

        public DateTime SoldWhen { get; set; }

        public string SoldBy { get; set; }

        public decimal DownPayment { get; set; }

        public decimal Principal { get; set; }

        public decimal InterestRate { get; set; }

        public short LoanDurationInMonths { get; set; }

        public decimal Commission { get; set; }
    }
}
