using GameCore.InterpolatedMovement;
using UnityEngine;

namespace Items.CraftingItems
{
    [CreateAssetMenu(menuName = "Items/CraftingItem")]
    public class CraftingItem : Item
    {
        public override void GetPickedUp(Transform pickerUpper)
        {
            pickerUpper.GetComponent<PlayerInventoryReferences>().CraftingItemRuntimeSet.Add(new CraftingItemReference(this));
        }
    }
}
