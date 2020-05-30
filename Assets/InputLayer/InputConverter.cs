using UnityEngine;

namespace InputLayer
{
    public static class InputConverter
    {
        // 1 is up, 0 is neutral, -1 is down
        //     -2 is pressed.
        public static int InputToIntValue(KeyCode key)
        {
            if (Input.GetKeyDown(key))
                return -1;
            if (Input.GetKeyUp(key))
                return 1;
            if (Input.GetKey(key))
                return -2;
            return 0;
        }

    }
}
