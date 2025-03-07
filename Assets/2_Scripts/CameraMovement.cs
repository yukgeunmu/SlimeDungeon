using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CameraMovement : MonoBehaviour
{
    [Header("Look")]
    public Transform cameraContainer;
    public float minLook; // ī�޶� x�� �ּҰ�
    public float maxLook; // ī�޶� x�� �ִ밢
    private float camCurXRot; // ī�޶� ���� x�� ȸ����
    private float camCurYRot; // ī�޶� ���� y�� ȸ���� (�߰�)
    public float lookSensitivity; // ī�޶� ȸ�� �ΰ���


    public static bool isBool = false;
    private void Awake()
    {
        GameManager.Instance.CameraMovement = this;
    }


    public void CameraLook(Vector2 mouseDelta)
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);
        camCurYRot += mouseDelta.x * lookSensitivity;

        if (!isBool)
        {
            // ���� ȸ�� �ּҰ�, �ִ밪
            transform.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
            CharacterManager.Instance.Player.transform.Rotate(Vector3.up * mouseDelta.x * lookSensitivity);
            Debug.DrawRay(cameraContainer.position, new Vector3(cameraContainer.forward.x, 0f, cameraContainer.forward.z).normalized, Color.red);
        }
        else
        {
            cameraContainer.transform.localEulerAngles = new Vector3(-camCurXRot, camCurYRot, 0);
            Debug.DrawRay(cameraContainer.position, new Vector3(cameraContainer.forward.x, 0f, cameraContainer.forward.z).normalized, Color.red);
        }
 
    }

    public void ObservantCameraPosition()
    {
        if (!isBool)
        {
            transform.localPosition = new Vector3(0, 2f, -2f);
            transform.localEulerAngles = new Vector3(35f, 0, 0);
            isBool = true;
        }
        else
        {
            transform.localPosition = Vector3.zero;
            cameraContainer.transform.localEulerAngles = Vector3.zero;
            cameraContainer.transform.forward = CharacterManager.Instance.Player.playerController.Rig.forward;
            isBool = false;
        }

    }



}
