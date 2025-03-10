using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour
{
    LayerMask layerMask;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
        }
    }

}
