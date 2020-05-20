using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Sets;
using UnityEngine;

// Items that can be collected, and used in platforming gameplay
public abstract class ConsumableItem : Item
{
    public override void Spawn(Transform spawnLocation)
    {
        GameObject item = Instantiate(emptyGameObject, spawnLocation.position, Quaternion.identity);
        item.transform.position = spawnLocation.position;
        item.transform.localScale = Size;

        BoxCollider2D collider = item.AddComponent<BoxCollider2D>();
        // TODO: something about this offsetting issue...
        collider.offset = new Vector2(0, .5f);

        SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite;

        GetPickedUpByPlayerComponent mainComponent = item.AddComponent<GetPickedUpByPlayerComponent>();
        mainComponent.Item = this;

    }

    public override void GetPickedUp(Transform pickerUpper)
    {
        pickerUpper.GetComponent<PlayerInventoryReferences>().ConsumableItemRuntimeSet.Add(this);
    }

    // Consumes the item, and applies the consumption to the object that is consuming it
    public abstract void ConsumeItem(GameObject consumer);
}
