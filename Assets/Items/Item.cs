using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.InterpolatedMovement;
using GameCore.Sets;
using GameCore.Variables.Primitives;
using UnityEditor;
using UnityEngine;

// Items that can only be collected but not be used in platforming gameplay
public abstract class Item : ScriptableObject
{
    public FloatReference interpolationFactor;
    public GameObject emptyGameObject;
    public Vector3 Size;
    public Vector3 RelativeSpawnPosition;
    public Sprite Sprite;
    public DetectCollisionDelegate CollisionDelegate;

    public MoveEntityDelegate ItemMovementDelegate;

    // Spawns the item...
    public void Spawn(Transform spawnLocation)
    {
        GameObject item = Instantiate(
            emptyGameObject,
            spawnLocation.position + RelativeSpawnPosition,
            Quaternion.identity
        );
        // item.transform.position = spawnTransform.position;
        item.transform.localScale = Size;

        SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite;

        GetPickedUpByPlayerComponent mainComponent = item.AddComponent<GetPickedUpByPlayerComponent>();
        mainComponent.Item = this;

        MoveEntity moveEntityBehaviour = item.AddComponent<MoveEntity>();
        moveEntityBehaviour.MoveEntityDelegate = ItemMovementDelegate;


        InterpolatedTransform it = item.AddComponent<InterpolatedTransform>();
        it.interpolationFactor = interpolationFactor;

        item.AddComponent<InterpolatedTransformUpdater>();
    }

    // Adds item to the inventory that is given as a parameter.
    public abstract void GetPickedUp(Transform pickerUpper);
}
