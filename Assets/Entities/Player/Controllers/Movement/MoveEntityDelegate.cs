using UnityEngine;

public abstract class MoveEntityDelegate : ScriptableObject
{
    // Moves the input entity
    public abstract void Move(Transform entityTransform, float timeModifier);
}
