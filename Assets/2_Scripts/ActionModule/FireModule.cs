using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModule : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float fireDamage;
    public float maxDistance;
    public LayerMask layerMask;
    public ParticleSystem particleSystem;

    private void Start()
    {
        var collsion = particleSystem.collision;
        collsion.enabled = true;
        collsion.type = ParticleSystemCollisionType.World;
        collsion.collidesWith = layerMask; // �浹�� ���̾� ����
    }
    private void Update()
    {

        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.deltaTime;
            OnFire();
        }

    }

    private void OnFire()
    {
        Ray ray = new Ray(transform.position, Vector3.up);

        if (Physics.Raycast(ray, maxDistance, layerMask))
        {
            particleSystem.gameObject.SetActive(true);

        }
        else
        {
            particleSystem.gameObject.SetActive(false);
        }
    }

    // ��ƼŬ�� �÷��̾�� �浹�� �� ü�� ����
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�浹��");
            CharacterManager.Instance.Player.playerCondition.uiCondition.health.Substract(fireDamage * Time.deltaTime);
        }
    }



}
