using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Collision
{
    [Serializable]
    public class RayPackage2D
    {
        public Vector2 OriginPosition;
        public Vector2 Direction;
        public float Distance;
    }
}
