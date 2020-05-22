using System.Collections;
using System.Collections.Generic;
using GabriellChen.SpaceApothecary.Events;
using GameCore.Variables.Primitives;
using UnityEngine;

public class UIVisibilityManagementComponent : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void ToggleCanvasGroup()
    {
        _canvasGroup.alpha = _canvasGroup.alpha == 0 ? 1f : 0f;
        _canvasGroup.interactable = !_canvasGroup.interactable;
        _canvasGroup.blocksRaycasts = !_canvasGroup.blocksRaycasts;
    }
}
