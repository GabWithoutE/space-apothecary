using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.Variables.Unity;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public CameraVariable camera;

    private Ray ray;

    void Start()
    {
        ray = new Ray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = camera.Value.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(
                ray,
                Mathf.Abs(camera.Value.transform.position.z * 2),
                LayerMask.GetMask("NPC"));

#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Abs(camera.Value.transform.position.z * 2), Color.red);
#endif

            if (!GameCorePhysics2D.HasHit(hit))
                return;

            IClickable clickable = hit.transform.GetComponent<IClickable>();

            if (clickable == null)
                return;

            clickable.OnLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ray = camera.Value.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(
                ray,
                Mathf.Abs(camera.Value.transform.position.z * 2),
                LayerMask.GetMask("NPC"));

#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Abs(camera.Value.transform.position.z * 2), Color.red);
#endif

            if (!GameCorePhysics2D.HasHit(hit))
                return;

            IClickable clickable = hit.transform.GetComponent<IClickable>();

            if (clickable == null)
                return;

            clickable.OnRightClick();
        }
    }
}
