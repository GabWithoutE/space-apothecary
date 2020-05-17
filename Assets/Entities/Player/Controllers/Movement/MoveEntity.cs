using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.Tags;
using GameCore.Variables.Primitives;
using UnityEngine;

public class MoveEntity : MonoBehaviour
{
    public MoveEntityDelegate moveEntityDelegate;

    private Transform transform;

    public List<TagObject> HitTags = new List<TagObject>();

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // moveEntityDelegate.Move(transform);

        // Calling get component is expensive, so potentially using the original raycast could be better, or using layers.
        //     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100, LayerMask.GetMask("Raycast"));
        // TODO: Design physics architecture.
        Debug.DrawRay(transform.position, Vector2.down * 10, Color.red);

        RaycastHit2D hit =
            GameCorePhysics2D.RayCast(
                transform.position,
                Vector2.down,
                10,
                LayerMask.GetMask("Raycast"),
                HitTags
            );
    }
}
