using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.InputControllerUtility
{
    public class InputController : MonoBehaviour
    {
        public bool autoEnable = true;
        public bool autoDisable = true;

        [SerializeField] public List<ActionModel> dataList;
        private List<ActionModel> actionDataList;
        private InputActionMap actionMap;

        private void Awake()
        {
            actionMap = new InputActionMap();
            actionDataList = new List<ActionModel>();
            InitialList(dataList);
        }


        private void OnEnable()
        {
            if (autoEnable)
            {
                EnableAllInputAction();
            }
        }

        private void OnDisable()
        {
            if (autoDisable)
            {
                DisableAllInputAction();
            }
        }

        private void InitialList(List<ActionModel> models)
        {
            foreach (ActionModel actionData in models)
            {
                AddInputAction(actionData);
            }
        }

        public bool AddInputAction(ActionModel actionModel)
        {
            bool isFind = IsExistInputAction(actionModel.actionName);
            if (!isFind)
            {
                actionDataList.Add(actionModel);
                actionModel.inputAction.performed += context => { actionModel.onAction?.Invoke(context); };
                actionModel.inputAction.Rename(actionModel.actionName);
                if (actionModel.isActive)
                    actionModel.inputAction.Enable();
                actionMap.AddAction(actionModel.inputAction.name);
            }
            else
            {
                Debug.LogError("Action is exist");
            }

            return isFind;
        }

        public void RemoveInputAction(string actionName)
        {
            bool isFind = IsExistInputAction(actionName);
            if (isFind)
            {
                ActionModel data = actionDataList.First(x => x.actionName == actionName);
                actionDataList.Remove(data);
            }
            else
            {
                Debug.LogError("Action is exist");
            }
        }

        public void EnableInputAction(string actionName)
        {
            bool isFind = IsExistInputAction(actionName);
            if (isFind)
            {
                ActionModel data = actionDataList.First(x => x.actionName == actionName);
                data.inputAction.Enable();
            }
            else
            {
                Debug.LogError("Action is exist");
            }
        }

        public void DisableInputAction(string actionName)
        {
            bool isFind = IsExistInputAction(actionName);
            if (isFind)
            {
                ActionModel data = actionDataList.First(x => x.actionName == actionName);
                data.inputAction.Disable();
            }
            else
            {
                Debug.LogError("Action is exist");
            }
        }

        public bool IsExistInputAction(string actionName)
        {
            return actionDataList.Exists(x => x.actionName == actionName);
        }

        public void DisableAllInputAction()
        {
            foreach (var action in actionDataList)
            {
                action.inputAction.Disable();
            }
        }

        public void EnableAllInputAction()
        {
            foreach (var action in actionDataList)
            {
                action.inputAction.Enable();
            }
        }


        public List<InputAction> GetAllInputActions()
        {
            List<InputAction> list = new List<InputAction>();
            foreach (var action in actionDataList)
            {
                list.Add(action.inputAction);
            }

            return list;
        }


        public InputAction GetInputActionByName(string actionName)
        {
            bool isFind = IsExistInputAction(actionName);
            if (isFind)
            {
                ActionModel data = actionDataList.First(x => x.actionName == actionName);
                return data.inputAction;
            }
            else
            {
                return null;
            }
        }

        public List<InputBinding> GetAllInputBindingByActionName(string actionName)
        {
            bool isFind = IsExistInputAction(actionName);
            if (isFind)
            {
                ActionModel data = actionDataList.First(x => x.actionName == actionName);
                return data.inputAction.bindings.ToList();
            }
            else
            {
                return null;
            }
        }

        public Tuple<InputBinding, bool> GetInputBindingById(InputAction inputAction, Guid Id)
        {
            foreach (var item in inputAction.bindings)
            {
                if (item.id == Id)
                {
                    return new Tuple<InputBinding, bool>(item, true);
                }
            }
            return new Tuple<InputBinding, bool>(new InputBinding(), false);
        }

        public void SetInputBinding(InputAction inputAction, InputBinding inputBinding)
        {
            for (int i = 0; i < inputAction.bindings.Count; i++)
            {
                var item = inputAction.bindings[i];
                if (inputAction.bindings[i].id == inputBinding.id)
                {
                    inputAction.ApplyBindingOverride(i, inputBinding);
                }
            }
        }
    }
}