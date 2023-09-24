using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.InputControllerUtility
{
    [Serializable]
    public class ActionModel
    {
        public ActionModel(string actionName, bool isActive, InputAction inputAction, UnityEvent<InputAction.CallbackContext> onAction)
        {
            this.actionName = actionName;
            this.isActive = isActive;
            this.inputAction = inputAction;
            this.onAction = onAction;
        }

        public string actionName;
        public bool isActive = true;
        public InputAction inputAction;
        public UnityEvent<InputAction.CallbackContext> onAction;
    }
}