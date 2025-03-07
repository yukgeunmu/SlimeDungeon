using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CameraMovement : MonoBehaviour
{
    [Header("Look")]
    public Transform cameraContainer;
    public float minLook; // 카메라 x축 최소각
    public float maxLook; // 카메라 x축 최대각
    private float camCurXRot; // 카메라 현재 x축 회전각
    private float camCurYRot; // 카메라 현재 y축 회전각 (추가)
    public float lookSensitivity; // 카메라 회전 민감도


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
            // 상하 회전 최소값, 최대값
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
