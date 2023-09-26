using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.UI
{
    public class BackManager : MonoBehaviour
    {
        private static BackManager _instance;

        public static BackManager Instance
        {
            get
            {
                if (!_instance) _instance = new GameObject("BackManager").AddComponent<BackManager>();
                return _instance;
            }
        }

        private Stack<Action> actions = new Stack<Action>();
        public Action SetAppExitPopUp;
        public bool HaveAction => actions.Count > 0;
        public int ActionCount => actions.Count;
        public bool allowBack = true;

        public Action AddBack
        {
            set => actions.Push(value);
        }

        public void RemoveAllBacks() => actions.Clear();

        public void RemoveLastBack()
        {
            if (actions.Count > 0)
            {
                actions.Pop();
            }
        }

        public Action ReplaceLastBack
        {
            set
            {
                RemoveLastBack();
                AddBack = value;
            }
        }

        public void ApplyBack()
        {
            if (!allowBack) return;

            if (actions.Count == 0)
            {
                return;
            }

            if (actions.Count <= 0) return;
            var removedItem = actions.Pop();
            removedItem?.Invoke();
        }

        public void ApplyBackAll()
        {
            if (actions.Count == 0) return;

            while (actions.Count > 0)
                ApplyBack();
        }
    }
}