using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

public class RespawnPlayerMethodHook : MonoBehaviour
{
    public Vector3Reference respawnPosition;
    public BoolReference dashing;

    public void RespawnPlayer()
    {
        transform.position = respawnPosition;
    }

    public void StationaryPlanted()
    {
        if (dashing.Value)
            return;

        transform.position = respawnPosition;
    }
}
