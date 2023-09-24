using UnityEngine.InputSystem;

namespace Runtime.InputControllerUtility
{
    public static class InputUtility
    {
        public static InputAction CreateInputAction(InputKeys inputKeys)
        {
            InputAction inputAction = new InputAction(inputKeys.actionName);
            foreach (string item in inputKeys.keys)
            {
                inputAction.AddBinding(item);
            }
            return inputAction;
        }
    }
}