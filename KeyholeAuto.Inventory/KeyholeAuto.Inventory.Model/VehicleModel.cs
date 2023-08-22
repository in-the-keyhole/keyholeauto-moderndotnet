namespace KeyholeAuto.Inventory.Model
{
    public class VehicleModel
    {
        public Guid Id { get; set; }

        public string VIN { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public short Year { get; set; }

        public bool? CleanCarFax { get; set; }

        public int? CurrentMileage
        {
            get
            {
                return MileageReadouts.OrderByDescending(x => x.Key).Select(x => x.Value).FirstOrDefault();
            }
        }

        public IDictionary<DateTime, int> MileageReadouts { get; set; }

        public string Category { get; set; }
    }
}
