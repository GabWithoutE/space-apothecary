using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItems : MonoBehaviour
{
    public ConsumableInventory ConsumableItemInventory;

    private Image[] _slotImages;

    void Start()
    {
        _slotImages = GetComponentsInChildren<Image>();
        DisplayConsumableItems();
    }

    public void DisplayConsumableItems()
    {
        for (int i = 0; i < ConsumableItemInventory.Items.Count; i++)
        {
            _slotImages[i].sprite = ConsumableItemInventory.Items[i].Item.Sprite;
        }
    }

}
