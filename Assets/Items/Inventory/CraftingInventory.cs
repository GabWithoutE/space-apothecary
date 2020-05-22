using GabriellChen.SpaceApothecary.Events;
using GameCore.Sets;
using UnityEngine;

namespace Items.Inventory
{
    [CreateAssetMenu(menuName = "Items/Inventory/CraftingItemInventory")]
    public class CraftingInventory : RuntimeSet<CraftingItemReference>
    {
        public bool useEvent = false;
        public GameEvent inventoryChangeEvent;

        public new void Add(CraftingItemReference itemReference)
        {
            base.Add(itemReference);
            if (useEvent)
                inventoryChangeEvent.Raise();
        }

        public new void Remove(CraftingItemReference itemReference)
        {
            base.Remove(itemReference);
            if (useEvent)
                inventoryChangeEvent.Raise();
        }
    }
}
