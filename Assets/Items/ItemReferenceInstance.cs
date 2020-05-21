using System;
using System.Runtime.Serialization;
using UnityEngine;

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
