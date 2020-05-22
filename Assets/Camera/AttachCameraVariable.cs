using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Unity;
using UnityEngine;

public class AttachCameraVariable : MonoBehaviour
{
    public CameraVariable cameraVariable;

    void Start()
    {
        cameraVariable.SetValue(GetComponent<Camera>());
    }
}
