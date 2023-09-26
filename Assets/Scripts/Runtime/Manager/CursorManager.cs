using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Runtime.Manager
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager cursorManager;

        private void Awake()
        {
            if (cursorManager == null)
            {
                cursorManager = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        int UILayer;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            UILayer = LayerMask.NameToLayer("UI");
        }

        public void Lock(InputAction.CallbackContext context)
        {
            Cursor.lockState = IsPointerOverUIElement() ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void UnLock(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.None;
        }


        //Returns 'true' if we touched or hovering on Unity UI element.
        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }


        //Returns 'true' if we touched or hovering on Unity UI element.
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == UILayer)
                    return true;
            }

            return false;
        }


        //Gets all event system raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Mouse.current.position.ReadValue();
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
    }
}