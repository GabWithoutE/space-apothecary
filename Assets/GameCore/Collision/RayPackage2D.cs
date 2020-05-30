using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace GameCore.Collision
{
    [Serializable]
    public class RayPackage2D
    {
        public Vector2 OriginPosition;
        public Vector2 Direction;
        public float Distance;

        public RayPackage2D FlipAcrossDirectionAxis()
        {
            RayPackage2D rayPackage2D = new RayPackage2D();
            rayPackage2D.OriginPosition = new Vector2(OriginPosition.x, OriginPosition.y);
            rayPackage2D.Direction = -Direction;
            rayPackage2D.Distance = Distance;

            if (rayPackage2D.Direction.x != 0)
                rayPackage2D.OriginPosition.x = -OriginPosition.x;

            if (rayPackage2D.Direction.y != 0)
                rayPackage2D.OriginPosition.y = -OriginPosition.y;

            return rayPackage2D;
        }
    }
}
