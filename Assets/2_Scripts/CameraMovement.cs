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
    public float minLook; // ī�޶� x�� �ּҰ�
    public float maxLook; // ī�޶� x�� �ִ밢
    private float camCurXRot; // ī�޶� ���� x�� ȸ����
    private float camCurYRot; // ī�޶� ���� y�� ȸ���� (�߰�)
    public float lookSensitivity; // ī�޶� ȸ�� �ΰ���
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


        // ���� ȸ�� �ּҰ�, �ִ밪
        fpCameraRig.transform.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // �¿� ȸ��
        fpCameraRoot.transform.Rotate(Vector3.up * mouseDelta.x * lookSensitivity);
    }


    public void TPCameraLook(Vector2 mouseDelta)
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);
        camCurYRot += mouseDelta.x * lookSensitivity;

        // ���� �¿� ȸ��
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
