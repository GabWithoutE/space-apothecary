using ControlInterpretationLayer.MonoBehaviours;
using GameCore.Variables.Primitives;
using UnityEngine;

namespace ControlInterpretationLayer
{
    [CreateAssetMenu(menuName = "InputInterpreter/HorizontalInputInputInterpreter")]
    public class HorizontalInputInterpreter : InputInterpreter
    {
        public IntReference upControl;
        public IntReference downControl;
        public IntReference leftControl;
        public IntReference rightControl;

        public IntVariable xDirection;
        public IntVariable yDirection;

        public override void Initialize() { }

        public override void Update()
        {
            if (leftControl.Value > 0 && rightControl.Value > 0)
                xDirection.SetValue(0);
            else if (leftControl.Value > 0)
                xDirection.SetValue(-1);
            else if (rightControl.Value > 0)
                xDirection.SetValue(1);
            else
                xDirection.SetValue(0);

            if (downControl.Value > 0 && upControl.Value > 0)
                yDirection.SetValue(0);
            else if (downControl.Value > 0)
                yDirection.SetValue(-1);
            else if (upControl.Value > 0)
                yDirection.SetValue(1);
            else
                yDirection.SetValue(0);
        }

        public override void FixedUpdate() { }
    }
}
