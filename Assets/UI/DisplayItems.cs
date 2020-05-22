using System.Collections;
using System.Collections.Generic;
using Items.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItems : MonoBehaviour
{
    public CraftingInventory craftingInventory;

    private Image[] _slotImages;

    void Start()
    {
        _slotImages = GetComponentsInChildren<Image>();
        DisplayConsumableItems();
    }

    public void DisplayConsumableItems()
    {
        for (int i = 0; i < _slotImages.Length; i++)
        {
            if (i < craftingInventory.Items.Count)
            {
                _slotImages[i].sprite = craftingInventory.Items[i].Item.Sprite;
                continue;
            }

            _slotImages[i].sprite = null;
        }
    }

}
