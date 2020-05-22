using System;
using Items.CraftingItems;

public abstract class ItemReference<T>
{
    public T Item;
}

[Serializable]
public class ConsumableItemReference : ItemReference<ConsumableItem>
{
    public ConsumableItemReference(ConsumableItem item)
    {
        Item = item;
    }
}

[Serializable]
public class CraftingItemReference : ItemReference<CraftingItem>
{
    public CraftingItemReference(CraftingItem item)
    {
        Item = item;
    }
}
