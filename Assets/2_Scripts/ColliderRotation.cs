using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRotation : MonoBehaviour
{
    public Transform modelTransform;

    private void Update()
    {
        float xRotation = modelTransform.localEulerAngles.x;

        if (Mathf.Approximately(xRotation, 270f) || Mathf.Approximately(xRotation, -90f))
        {
            this.gameObject.SetActive(true);
            CharacterManager.Instance.Player.playerController.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            this.gameObject.SetActive(false);
            CharacterManager.Instance.Player.playerController.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
