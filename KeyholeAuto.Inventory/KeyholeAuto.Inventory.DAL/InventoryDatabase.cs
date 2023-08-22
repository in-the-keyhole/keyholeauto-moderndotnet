using KeyholeAuto.Inventory.Model;

namespace KeyholeAuto.Inventory.DAL
{
    public class InventoryDatabase
    {
        private readonly IDictionary<Guid, VehicleModel> inventoryDictionary = new Dictionary<Guid, VehicleModel>();
        
        public VehicleModel AddVehicleToInventory(VehicleModel vehicleModel)
        {
            if (inventoryDictionary.ContainsKey(vehicleModel.Id))
            {
                throw new Exception($"Vehicle {vehicleModel.Id} already known in inventory");
            }

            inventoryDictionary[vehicleModel.Id] = vehicleModel;

            return vehicleModel;
        }

        public IQueryable<VehicleModel> GetInventoryAsQueryable() =>
            inventoryDictionary.Values.AsQueryable();
    }
}
