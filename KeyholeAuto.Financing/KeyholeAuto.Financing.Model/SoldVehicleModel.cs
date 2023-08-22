namespace KeyholeAuto.Financing.Model
{
    public class SoldVehicleModel
    {
        public Guid Id { get; set; }

        public string VIN { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public short Year { get; set; }

        public int CurrentMileage { get; set; }

        public string Category { get; set; }
    }
}
