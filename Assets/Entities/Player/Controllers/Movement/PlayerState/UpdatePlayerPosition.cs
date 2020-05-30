using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

public class UpdatePlayerPosition : MonoBehaviour
{
    public Vector3Variable playerPosition;

    void Update()
    {
        playerPosition.SetValue(transform.position);
    }

}
