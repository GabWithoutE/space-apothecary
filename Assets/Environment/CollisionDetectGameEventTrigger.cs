using GabriellChen.SpaceApothecary.Events;
using GameCore.Collision;
using UnityEngine;

public class CollisionDetectGameEventTrigger : MonoBehaviour
{
    public DetectCollisionDelegate spikeCollisionCaster;
    public GameEvent spiked;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = spikeCollisionCaster.DetectCollisionRaycast(transform, false);
        if (GameCorePhysics2D.HasHit(hit))
            spiked.Raise();
    }
}
