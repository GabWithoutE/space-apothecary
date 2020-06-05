using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.InterpolatedMovement;
using GameCore.Sets;
using GameCore.Variables.Primitives;
using UnityEngine;

// Items that can be collected, and used in platforming gameplay
public abstract class ConsumableItem : Item
{
    public override void GetPickedUp(Transform pickerUpper)
    {
        pickerUpper.GetComponent<PlayerInventoryReferences>().ConsumableItemRuntimeSet.Add(new ConsumableItemReference(this));
    }

    // Consumes the item, and applies the consumption to the object that is consuming it
    public abstract void ConsumeItem(GameObject consumer);
}
