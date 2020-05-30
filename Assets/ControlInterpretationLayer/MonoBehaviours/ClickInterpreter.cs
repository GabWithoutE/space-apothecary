using GameCore.Collision;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

namespace ControlInterpretationLayer
{
    public class ClickInterpreter : MonoBehaviour
    {
        public IntReference primaryAbilityControl;
        public IntReference secondaryAbilityControl;
        public IntReference tertiaryAbilityControl;

        public Vector3Reference secondDirectionLocation;
        public CameraVariable cameraVariable;

        public BoolVariable usePrimaryAbility;
        public BoolVariable useSecondaryAbility;
        public BoolVariable useTertiaryAbility;
        public BoolVariable interactWithNPC;

        void Update()
        {
            // can use primary ability if not clicking on an NPC
            if (primaryAbilityControl.Value == -1)
                if (!LeftClickOnNPC())
                    usePrimaryAbility.SetValue(true);

            // undo using primary ability if letting go or not clicking button
            if (primaryAbilityControl.Value == 1 || primaryAbilityControl.Value == 0)
                usePrimaryAbility.SetValue(false);

            // on down click can trigger NPC Interaction...
            if (secondaryAbilityControl.Value == -1)
                if (!RightClickOnNPC())
                    useSecondaryAbility.SetValue(true);


            // letting go can trigger ability if not currently NPC interaction
            // TODO: Solve how to deal with letting go for attacking...
            // if (secondaryAbilityControl.Value == 1)
                // if (!interactWithNPC.Value)
                // {
                    // Debug.Log("reaches");
                    // useSecondaryAbility.SetValue(true);
                // }

            // after using ability on letting go, return the ability usage variable to false
            if (secondaryAbilityControl.Value == 0 || secondaryAbilityControl.Value == 1)
                useSecondaryAbility.SetValue(false);
        }

        private RaycastHit2D CastRayFromMouseToWorld()
        {
            Ray ray = cameraVariable.Value.ScreenPointToRay(secondDirectionLocation.Value);
            return Physics2D.GetRayIntersection(
                ray,
                Mathf.Abs(cameraVariable.Value.transform.position.z * 2),
                LayerMask.GetMask("NPC")
            );
        }

        private bool LeftClickOnNPC()
        {
            RaycastHit2D hit = CastRayFromMouseToWorld();

            if (!GameCorePhysics2D.HasHit(hit))
            {
                return false;
            }

            IClickable clickable = hit.transform.GetComponent<IClickable>();

            if (clickable == null)
                return false;

            clickable.OnLeftClick();
            return true;
        }

        private bool RightClickOnNPC()
        {
            RaycastHit2D hit = CastRayFromMouseToWorld();

            if(!GameCorePhysics2D.HasHit(hit))
                return false;

            IClickable clickable = hit.transform.GetComponent<IClickable>();

            if (clickable == null)
                return false;

            clickable.OnRightClick();
            return true;
        }
    }
}
