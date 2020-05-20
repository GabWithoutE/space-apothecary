using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItemSpawner : MonoBehaviour
{
    public Item Item;

    void Start()
    {
        Item.Spawn(GetComponent<Transform>());
        Object.DestroyImmediate(gameObject);
    }
}
