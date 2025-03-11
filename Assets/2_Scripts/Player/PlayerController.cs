using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed; 
    public float startSpeed;
    public float dashSpeed; 
    public float jumpPower;
    public float wallSlidingSpeed;
    private Vector2 curMovementInput; 
    public LayerMask groundLayMask;
    private float lastDashTime = 0f;  
    private float doubleTapThreshold = 0.3f;

    private Vector3 platformVelocity = Vector3.zero; // 발판의 속도 저장 변수

    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    private bool canDash = true;

    [HideInInspector] public bool isAcceleration = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {
        if (!isAcceleration) Move();
        CharacterManager.Instance.Player.interaction.WallInteraction(_rigidbody, wallSlidingSpeed);
        if (isGround()) isAcceleration = false;
    }

    private void LateUpdate()
    {
        GameManager.Instance.cameraMovement.CameraLook(mouseDelta);
    }

    private void Move()
    {

        Vector3 dir = MoveDirection();
        Vector3 worldMove;

        if (CameraMovement.isCurrentFp) worldMove = GameManager.Instance.cameraMovement.fpCameraRoot.TransformDirection(dir);
        else worldMove = GameManager.Instance.cameraMovement.tpCameraRig.TransformDirection(dir);

        worldMove *= moveSpeed;
        worldMove.y = _rigidbody.velocity.y;

        _rigidbody.velocity = worldMove + platformVelocity;


    }


    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGround())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            CharacterManager.Instance.Player.playerCondition.uiCondition.stamina.Substract(10);
        }
    }

    bool isGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position +(transform.forward*0.2f) + (transform.up *0.01f), Vector3.down),
            new Ray(transform.position +(-transform.forward*0.2f) + (transform.up *0.01f), Vector3.down),
            new Ray(transform.position +(transform.right*0.2f) + (transform.up *0.01f), Vector3.down),
            new Ray(transform.position +(-transform.right*0.2f) + (transform.up *0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {

            if (Physics.Raycast(rays[i], 0.5f, groundLayMask))
            {
                return true;
            }

        }

        return false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (Time.time - lastDashTime < doubleTapThreshold)
            {
                Dash();
            }
        
            lastDashTime = Time.time;
        }
    }

    public void OnObservant(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(GameManager.Instance.cameraMovement.initialCamera == CameraType.FpCamera)
            {

                GameManager.Instance.cameraMovement.initialCamera = CameraType.TpCamera;
            }
            else
            {
                GameManager.Instance.cameraMovement.initialCamera = CameraType.FpCamera;
            }
        }
    }

    private void Dash()
    {
        if (canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }


    private IEnumerator DashCoroutine()
    {
        if(CharacterManager.Instance.Player.playerCondition.uiCondition.stamina.curValue > 30)
        {
            CharacterManager.Instance.Player.playerCondition.uiCondition.stamina.Substract(30);
            canDash = false;
            float originalSpeed = moveSpeed;
            moveSpeed = dashSpeed;
            yield return new WaitForSeconds(0.2f); 
            moveSpeed = originalSpeed; 
            yield return new WaitForSeconds(0.5f); 
            canDash = true;
        }

    }

    public void AddSpeed(float value, float duration)
    {
        StartCoroutine(SpeedBoost(value, duration));
    }

    private IEnumerator SpeedBoost(float value, float duration)
    {
        moveSpeed += value; 

        yield return new WaitForSeconds(duration); 

        
        float velocity = 0f;
        while (moveSpeed > startSpeed + 0.1f) 
        {
            moveSpeed = Mathf.SmoothDamp(moveSpeed, startSpeed, ref velocity, 1.5f); 
            yield return null;
        }

        moveSpeed = startSpeed;
    }

    public Vector3 MoveDirection()
    {
        Vector3 direction;

       return direction = transform.forward* curMovementInput.y + transform.right * curMovementInput.x;
    }

    public void PlatformSpeed(Vector3 platformSpeed)
    {
        platformVelocity = platformSpeed;
    }

    public void OnActiveCusor(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }



}




