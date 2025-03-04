using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed; // 이동 속도
    public float jumpPower; // 점프 파워
    private Vector2 curMovementInput; // 현재 이동 방향
    public LayerMask groundLayMask; // 땅에서만 점프하게 설정

    [Header("Look")]
    public Transform cameraContainer; // 카메라 컨테이너 오브젝트
    public float minLook; // 카메라 x축 최소각
    public float maxLook; // 카메라 x축 최대각
    private float camCurXRot; // 카메라 현재 x축 회전각
    public float lookSensitivity; // 카메라 회전 민감도
    private Vector2 mouseDelta; // 마우스 이동값

    private Rigidbody _rigidbody; // 플레이어 rigidbody


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        cameraContainer = transform.Find("CameraContainer");
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
        CameraLook();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook); // 상하 회전 최소값, 최대값
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 상하 회전

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 좌우 회전
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
            if (Physics.Raycast(rays[i], 0.1f, groundLayMask))
                return true;
        }

        return false;
    }
}




