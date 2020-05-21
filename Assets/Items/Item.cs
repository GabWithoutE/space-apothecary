using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.Sets;
using UnityEditor;
using UnityEngine;

// Items that can only be collected but not be used in platforming gameplay
public abstract class Item : ScriptableObject
{
    public GameObject emptyGameObject;
    public Vector3 Size;
    public Sprite Sprite;
    public DetectCollisionDelegate CollisionDelegate;

    public MoveEntityDelegate ItemMovementDelegate;

    // Spawns the item...
    public abstract void Spawn(Transform spawnLocation);

    // Adds item to the inventory that is given as a parameter.
    public abstract void GetPickedUp(Transform pickerUpper);
}
