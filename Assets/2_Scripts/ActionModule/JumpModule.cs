using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpModule : MonoBehaviour
{
    public float JumpPower;
    public LayerMask layerMask;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

}
