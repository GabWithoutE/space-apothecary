using UnityEngine;

public abstract class MoveEntityDelegate : ScriptableObject
{
    // Moves the input entity
    public abstract void FixedMove(Transform entityTransform);
}
