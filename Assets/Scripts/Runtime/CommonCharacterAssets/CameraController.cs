using Runtime.InputControllerUtility;
using Runtime.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.CommonCharacterAssets
{
    [RequireComponent(typeof(InputController))]
    public class CameraController : MonoBehaviour
    {
        public Vector2 look;

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        private const float _threshold = 0.01f;

        private InputController _inputController;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputController = GetComponent<InputController>();
            CinemachineCameraTarget = transform.GetChild(0).gameObject;
        }

        public void SetLock(InputAction.CallbackContext context)
        {
            look = context.ReadValue<Vector2>();
        }

        public void test()
        {
            
        }

        private void Start()
        {
            UIManager.Instance.dialoguePopUp.OnShow += UiEnable;
            UIManager.Instance.dialoguePopUp.OnHide += UiDisable;
            
            UIManager.Instance.pausePopUp.OnShow += UiEnable;
            UIManager.Instance.pausePopUp.OnHide += UiDisable;
            
            UIManager.Instance.cataloguePopUp.OnShow += UiEnable;
            UIManager.Instance.cataloguePopUp.OnHide += UiDisable;
            
            UIManager.Instance.helpPopUp.OnShow += UiEnable;
            UIManager.Instance.helpPopUp.OnHide += UiDisable;
        }

        private void Update()
        {
            CameraRotation();
        }

        public bool LockCameraPosition = false;

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = 1.0f;

                _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void UiDisable()
        {
            _inputController.EnableAllInputAction();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UiEnable()
        {
            _inputController.DisableAllInputAction();
            look = Vector2.zero;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}