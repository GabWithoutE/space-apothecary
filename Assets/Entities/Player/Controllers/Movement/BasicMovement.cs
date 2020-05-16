using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public IntReference XDirection;
    public IntReference YDirection;

    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(XDirection, YDirection, 0);
        transform.position += move * Time.deltaTime;
    }
}
