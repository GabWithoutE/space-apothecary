using GabriellChen.SpaceApothecary.Events;
using GameCore.Sets;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Inventory/ConsumableItemInventory")]
public class ConsumableInventory : RuntimeSet<ConsumableItemReference>
{
    public GameEvent inventoryChangeEvent;

    public new void Add(ConsumableItemReference itemReference)
    {
        base.Add(itemReference);
        inventoryChangeEvent.Raise();
    }

    public new void Remove(ConsumableItemReference itemReference)
    {
        base.Remove(itemReference);
        inventoryChangeEvent.Raise();
    }
}
