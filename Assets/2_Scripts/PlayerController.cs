using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed; // �̵� �ӵ�
    public float jumpPower; // ���� �Ŀ�
    private Vector2 curMovementInput; // ���� �̵� ����
    public LayerMask groundLayMask; // �������� �����ϰ� ����

    [Header("Look")]
    public Transform cameraContainer; // ī�޶� �����̳� ������Ʈ
    public float minLook; // ī�޶� x�� �ּҰ�
    public float maxLook; // ī�޶� x�� �ִ밢
    private float camCurXRot; // ī�޶� ���� x�� ȸ����
    public float lookSensitivity; // ī�޶� ȸ�� �ΰ���
    private Vector2 mouseDelta; // ���콺 �̵���

    private Rigidbody _rigidbody; // �÷��̾� rigidbody


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
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook); // ���� ȸ�� �ּҰ�, �ִ밪
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // ���� ȸ��

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // �¿� ȸ��
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




