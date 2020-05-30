using GabriellChen.SpaceApothecary.Events;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

public class NPCHandleClick : MonoBehaviour, IClickable
{
    public float rangeDistance;
    public Vector3Reference playerPositionReference;
    public GameEvent npcLeftClicked;
    public GameEvent npcRightClicked;

    private bool _isClickable;

    // Update is called once per frame
    void Update()
    {
        Vector2 juiceMachinePosition = transform.position;
        float distance =
            Vector2.Distance(juiceMachinePosition, playerPositionReference.Value);
        _isClickable = distance < rangeDistance;
    }

    public void OnLeftClick()
    {
        if (_isClickable)
            npcLeftClicked.Raise();
    }

    public void OnRightClick()
    {
        if (_isClickable)
            npcRightClicked.Raise();
    }
}
