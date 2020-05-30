using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.Tags;
using GameCore.Variables.Primitives;
using UnityEngine;

public class MoveEntity : MonoBehaviour
{
    public MoveEntityDelegate MoveEntityDelegate;

    private Transform entityTransform;

    // Start is called before the first frame update
    void Start()
    {
        entityTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEntityDelegate.Move(entityTransform, 0);
    }
}
