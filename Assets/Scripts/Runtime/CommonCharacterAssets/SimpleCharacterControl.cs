using System.Collections.Generic;
using Runtime.InputControllerUtility;
using Runtime.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.CommonCharacterAssets
{
    public class SimpleCharacterControl : Character
    {
        [SerializeField] private float m_turnSpeed = 200;
        [SerializeField] private float m_jumpForce = 4;

        private bool isAttack;


        [SerializeField] private Animator m_animator;
        [SerializeField] private Rigidbody m_rigidBody;


        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;
        private readonly float m_backwardsWalkScale = 0.16f;
        private readonly float m_backwardRunScale = 0.66f;

        private bool m_wasGrounded;
        private Vector3 m_currentDirection = Vector3.zero;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;
        private bool m_jumpInput = false;

        private bool m_isGrounded;

        private List<Collider> m_collisions = new List<Collider>();


        public Collider hammerCollider;

        public Vector2 move;

        private bool leftShift;

        private InputController _inputController;


        private void Awake()
        {
            m_animator=  gameObject.GetComponentInChildren<Animator>();
            _inputController = GetComponent<InputController>();
        }

        public void UiDisable()
        {
            _inputController.EnableAllInputAction();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UiEnable()
        {
            _inputController.DisableAllInputAction();
            move = Vector2.zero;
            Cursor.lockState = CursorLockMode.None;
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
        
        
            m_animator.SetBool("Grounded", true);
        }

        public float groundDistance = 1;

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > groundDistance)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }

                    m_isGrounded = true;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true;
                    break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }

                if (m_collisions.Count == 0)
                {
                    m_isGrounded = false;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }

            if (m_collisions.Count == 0)
            {
                m_isGrounded = false;
            }
        }


        private void FixedUpdate()
        {
     

            if (m_isGrounded && isAttack)
            {
                return;
            }
            DirectUpdate();
 

            m_wasGrounded = m_isGrounded;
            m_jumpInput = false;
        }

        public void AttackInput(InputAction.CallbackContext callbackContext)
        {
            if (isAttack) return;
            m_animator.SetTrigger("Attack");
            Debug.Log("Anim");
        }

        private void TankUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            bool walk = Input.GetKey(KeyCode.LeftShift);

            if (v < 0)
            {
                if (walk)
                {
                    v *= m_backwardRunScale;
                }
                else
                {
                    v *= m_backwardsWalkScale;
                }
            }
            else if (walk)
            {
                v *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * characterSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            // JumpingAndLanding();
        }

        private void DirectUpdate()
        {
            float v = move.y;
            float h = move.x;

            Transform camera = Camera.main.transform;

            // if (!leftShift)
            // {
            //     v *= m_walkScale;
            //     h *= m_walkScale;
            // }


            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * characterSpeed * Time.deltaTime;

                m_animator.SetFloat("MoveSpeed", direction.magnitude);
            }

            // JumpingAndLanding();
        }

        private void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }

            if (!m_wasGrounded && m_isGrounded)
            {
                m_animator.SetTrigger("Land");
            }

            if (!m_isGrounded && m_wasGrounded)
            {
                m_animator.SetTrigger("Jump");
            }
        }

        public void AttackStart()
        {
            hammerCollider.enabled = true;
            isAttack = true;
            Debug.Log("on");
        }

        public void AttackEnd()
        {
            hammerCollider.enabled = false;
            isAttack = false;
            Debug.Log("Off");
        }

        public void SetMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }


        public void SetLeftShift(InputAction.CallbackContext context)
        {
            // leftShift = !leftShift;
        }

        public void SetJump(InputAction.CallbackContext context)
        {
            // if (!m_jumpInput)
            // {
            //     m_jumpInput = true;
            // }
        }
    }
}