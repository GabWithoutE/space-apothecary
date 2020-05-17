using System.Collections;
using System.Collections.Generic;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

public class PlayerXAxisRunning : MonoBehaviour
{
    public FloatModulator XMovementSpeed;
    public IntVariable XDirection;

    private float totalTimeRun = 0;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (XDirection.Value != 0)
        {
            totalTimeRun += Time.fixedDeltaTime;
            playerTransform.position += new Vector3(XDirection.Value * XMovementSpeed.Output(totalTimeRun), 0, 0);
        }
        else
        {
            totalTimeRun = 0;
        }
    }
}
