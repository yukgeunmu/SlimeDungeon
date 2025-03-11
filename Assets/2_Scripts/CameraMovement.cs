using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public enum CameraType { FpCamera, TpCamera};

public class CameraMovement : MonoBehaviour
{
    [Header("Look")]

    public Camera tpCamera;
    public Camera fpCamera;
    public Transform tpCameraRig;
    public Transform fpCameraRoot;
    public Transform fpCameraRig;
    public GameObject tpCameraObject;
    public GameObject fpCameraObject;
    public float minLook; // 카메라 x축 최소각
    public float maxLook; // 카메라 x축 최대각
    private float camCurXRot; // 카메라 현재 x축 회전각
    private float camCurYRot; // 카메라 현재 y축 회전각 (추가)
    public float lookSensitivity; // 카메라 회전 민감도
    public CameraType initialCamera;
    public static bool isCurrentFp = true;

    private void Awake()
    {
        GameManager.Instance.CameraMovement = this;
    }


    public void CameraLook(Vector2 mouseDelta)
    { 
        if(isCurrentFp)
        {
            CameraSetting();
            FPCameraLook(mouseDelta);
        }
        else
        {
            CameraSetting();
            TPCameraLook(mouseDelta);
            RotateFRRoot();
        }

    }

    public void FPCameraLook(Vector2 mouseDelta)
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);


        // 상하 회전 최소값, 최대값
        fpCameraRig.transform.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 좌우 회전
        fpCameraRoot.transform.Rotate(Vector3.up * mouseDelta.x * lookSensitivity);
    }


    public void TPCameraLook(Vector2 mouseDelta)
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);
        camCurYRot += mouseDelta.x * lookSensitivity;

        // 상하 좌우 회전
        tpCameraRig.transform.localEulerAngles = new(-camCurXRot, camCurYRot, 0);

    }

    private void RotateFRRoot()
    {
        Vector3 dir = tpCameraRig.TransformDirection(CharacterManager.Instance.Player.playerController.MoveDirection());

        if (dir == Vector3.zero) return;

        float currentY = fpCameraRoot.localEulerAngles.y;
        float nextY = Quaternion.LookRotation(dir, Vector3.up).eulerAngles.y;

        if (nextY - currentY > 180f) nextY -= 360f;
        else if (currentY - nextY > 180f) nextY += 360f;

        fpCameraRoot.eulerAngles = Vector3.up * Mathf.Lerp(currentY, nextY, 0.1f);

    }


    public void CameraSetting()
    {
        isCurrentFp = initialCamera == CameraType.FpCamera;
        fpCameraObject.SetActive(isCurrentFp);
        tpCameraObject.SetActive(!isCurrentFp);
    }

}
