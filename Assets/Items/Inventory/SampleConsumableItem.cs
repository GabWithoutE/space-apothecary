using GameCore.Sets;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ConsumableItems/SampleConsumableItem")]
public class SampleConsumableItem : ConsumableItem
{
    public override void ConsumeItem(GameObject consumer)
    {
        throw new System.NotImplementedException();
    }
}
