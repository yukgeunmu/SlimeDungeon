using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed; 
    public float startSpeed;
    public float dashSpeed; 
    public float jumpPower; 
    private Vector2 curMovementInput; 
    public LayerMask groundLayMask;
    private float lastDashTime = 0f;  
    private float doubleTapThreshold = 0.3f;

    public Transform Rig;

    private Vector2 mouseDelta; 

    private Rigidbody _rigidbody;

    public Action UseItem;

    public bool canDash = true;


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
        Move();
    }

    private void LateUpdate()
    {
        GameManager.Instance.CameraMovement.CameraLook(mouseDelta);
    }

    private void Move()
    {
        if(curMovementInput.magnitude != 0)
        {
            Transform cameraDir = GameManager.Instance.cameraMovement.cameraContainer.transform;
            Vector3 looForward = new Vector3(cameraDir.forward.x, 0f, cameraDir.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraDir.right.x, 0f, cameraDir.right.z).normalized;
            Vector3 dir = looForward * curMovementInput.y + lookRight * curMovementInput.x;

            dir *= moveSpeed;
            dir.y = _rigidbody.velocity.y;

            if(CameraMovement.isBool)  Rig.transform.forward = dir;

            _rigidbody.velocity = dir;
        }
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

            if (Physics.Raycast(rays[i], 1f, groundLayMask))
                return true;
        }

        return false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
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
            GameManager.Instance.cameraMovement.ObservantCameraPosition();
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
}




