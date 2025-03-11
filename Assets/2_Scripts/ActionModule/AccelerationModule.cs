using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationModule : MonoBehaviour
{
    public Rigidbody rb; // 플레이어의 Rigidbody
    public float powerSpeed = 10f;
    public float maxLanchForce;
    private float launchForce = 0f; // 발사 힘
    public Vector3 launchDirection = new Vector3(0,1,1);
    private bool isOnPlatform = false; // 플랫폼 위에 있는지 여부


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
            // 특정 키 입력 시 발사
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
        rb.AddForce(launchDirection*launchForce, ForceMode.Impulse); // 힘 가하기
        isOnPlatform = false; // 플랫폼에서 벗어남
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
