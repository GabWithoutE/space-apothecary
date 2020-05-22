using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using UnityEngine;

public class UpdatePlayerPosition : MonoBehaviour
{
    public FloatVariable playerXPosition;
    public FloatVariable playerYPosition;

    void Update()
    {
        var position = transform.position;
        playerXPosition.SetValue(position.x);
        playerYPosition.SetValue(position.y);
    }

}
