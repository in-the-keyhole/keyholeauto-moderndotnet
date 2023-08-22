using KeyholeAuto.Inventory.Model;
using KeyholeAuto.Inventory.Service;

namespace KeyholeAuto.Inventory.API.GraphQL
{
    public class InventoryQuery
    {
        public IQueryable<VehicleModel> GetInventory([Service] InventoryService inventoryService) =>
            inventoryService.GetInventoryAsQueryable();
    }
}
