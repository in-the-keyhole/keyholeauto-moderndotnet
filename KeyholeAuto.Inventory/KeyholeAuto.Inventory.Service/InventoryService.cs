using KeyholeAuto.Inventory.DAL;
using KeyholeAuto.Inventory.Model;

namespace KeyholeAuto.Inventory.Service
{
    public class InventoryService
    {
        private readonly InventoryDatabase inventoryDatabase;

        public InventoryService(InventoryDatabase inventoryDatabase)
        {
            this.inventoryDatabase = inventoryDatabase;
        }

        public VehicleModel AddVehicleToInventory(VehicleModel vehicleModel)
        {
            return inventoryDatabase.AddVehicleToInventory(vehicleModel);
        }

        public IQueryable<VehicleModel> GetInventoryAsQueryable()
        {
            return inventoryDatabase.GetInventoryAsQueryable();
        }
    }
}
