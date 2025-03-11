using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationModule : MonoBehaviour
{
    public Rigidbody rb; // �÷��̾��� Rigidbody
    public float powerSpeed = 10f;
    public float maxLanchForce;
    private float launchForce = 0f; // �߻� ��
    public Vector3 launchDirection = new Vector3(0,1,1);
    private bool isOnPlatform = false; // �÷��� ���� �ִ��� ����


    private void Start()
    {
        maxLanchForce = GameManager.Instance.tootipUI.lanchGage.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlatform)
        {
            GameManager.Instance.tootipUI.lanchGage.gameObject.SetActive(true);
            // Ư�� Ű �Է� �� �߻�
            if (Input.GetMouseButton(0))
            {
                CharacterManager.Instance.Player.playerController.isAcceleration = true;
                launchForce += Time.deltaTime * powerSpeed;
                GameManager.Instance.tootipUI.lanchGage.Add(Time.deltaTime * powerSpeed);
                if (launchForce > maxLanchForce) launchForce = maxLanchForce;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                LaunchPlayer();
            }
        }
        else
        {
            launchForce = 0f;
            GameManager.Instance.tootipUI.lanchGage.gameObject.SetActive(false);
            GameManager.Instance.tootipUI.lanchGage.curValue = 0f;
        }
    }

    private void LaunchPlayer()
    {
        CharacterManager.Instance.Player.playerController.isAcceleration = true;
        rb.AddForce(launchDirection*launchForce, ForceMode.Impulse); // �� ���ϱ�
        isOnPlatform = false; // �÷������� ���
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false;
        }
    }
}
