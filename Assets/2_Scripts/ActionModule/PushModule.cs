using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushModule : MonoBehaviour
{
    public Rigidbody rigid;
    public float fower;
    public Direction direcntion;
    public float maxDistacnce;
    public LayerMask layerMask;
    private Vector3 pushDirection;


    private void Awake()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        OnPushMoudle(direcntion);
    }


    public void OnPushMoudle(Direction direction)
    {
        switch(direction)
        {
            case Direction.Horizontal:
                pushDirection = transform.right;
                break;
            case Direction.Vertical:
                pushDirection = transform.forward;
                break;

        }

        Ray ray = new Ray(transform.position, pushDirection);

        if(Physics.Raycast(ray, maxDistacnce, layerMask))
        {
            rigid.AddForce(pushDirection*fower, ForceMode.Acceleration);
        }

    }
}

