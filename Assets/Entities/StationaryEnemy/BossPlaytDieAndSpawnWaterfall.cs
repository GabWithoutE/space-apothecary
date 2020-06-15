using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using UnityEngine;

public class BossPlaytDieAndSpawnWaterfall : MonoBehaviour
{
    public DetectCollisionDelegate slashDetector;
    // Waterfall starts disabled, and is enabled when the boss plant dies.
    public GameObject waterfallToEnable;

    // TODO: create event subscriptions for variables based on if the value changes
    // Going to have to do this weird thing to ensure no double hits are being registered
    // TODO: Fix Breaking patterns here to get something done quickly.

    private bool isHit;
    public int hits = 4;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = slashDetector.DetectCollisionRaycast(transform, false);
        if (GameCorePhysics2D.HasHit(hit))
        {
            if (!isHit)
            {
                Debug.Log("sliced");
                isHit = true;
                hits -= 1;
            }

            if (hits == 0)
                EnableWaterfallDestroyBoss();

            return;
        }

        isHit = false;
    }

    private void EnableWaterfallDestroyBoss()
    {
        waterfallToEnable.SetActive(true);
        Destroy(gameObject);
    }
}
