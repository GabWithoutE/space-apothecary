using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    // TODO: Revisit for redesign, quick hacks to get things working
    //     Nice thing to consider would be for attacks to be scriptable objects
    //     so that they can be configured/diversified easiliy
    public GameObject upAttack;
    public GameObject downAttack;
    public GameObject leftAttack;
    public GameObject rightAttack;

    public BoolReference primaryAttacking;
    public Vector3Reference primaryAttackDirection;

    // Update is called once per frame
    void Update()
    {
        if (primaryAttacking.Value)
        {
            if (primaryAttackDirection.Value.x == -1)
                leftAttack.SetActive(true);
            else if (primaryAttackDirection.Value.x == 1)
                rightAttack.SetActive(true);
            else if (primaryAttackDirection.Value.y == -1)
                // TODO: Bug allows player to go through walls, caused by the attack being detected by the wall collision detector
                downAttack.SetActive(true);
            else if (primaryAttackDirection.Value.y == 1)
                upAttack.SetActive(true);
            return;
        }

        leftAttack.SetActive(false);
        rightAttack.SetActive(false);
        upAttack.SetActive(false);
        downAttack.SetActive(false);
    }
}
