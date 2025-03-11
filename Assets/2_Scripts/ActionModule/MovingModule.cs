using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    Vertical,
    Horizontal,
    Depth,
    UpperLeft_LowerRight,
    UpperRight_LowerLeft

}


public class MovingModule : MonoBehaviour
{
    public Rigidbody rigid;
    public float monveSpeed;
    public float moveDistance;
    public bool startdirection;
    private int TansDir = 1;
    private int direction = 1;
    private Vector3 startPos;
    private Vector3 moveVec;
    private Vector3 dirVec;

    public Direction myDir;

    private Vector3 previousPosition; // 발판의 이전 위치
    public Vector3 platformVelocity { get; private set; } // 발판의 속도

    private bool onPlatform = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        startPos = transform.position;
        if(startdirection)
        {
            TansDir = -1;
        }
    }

    private void Start()
    {
       switch(myDir)
        {
            case Direction.Vertical:
                dirVec = Vector3.forward*TansDir;
                break;
            case Direction.Horizontal:
                dirVec = Vector3.right * TansDir;
                break;
            case Direction.Depth:
                dirVec = Vector3.up * TansDir;
                break;
            case Direction.UpperLeft_LowerRight:
                dirVec = new Vector3(1, 0, -1).normalized * TansDir;
                break;
            case Direction.UpperRight_LowerLeft:
                dirVec = new Vector3(1, 0, 1).normalized * TansDir;
                break;

        }
    }

    private void FixedUpdate()
    {
        float movedDistance = Vector3.Distance(transform.position, startPos);

     
        if (movedDistance >= moveDistance)
        {
            direction *= -1; // 방향 반전
            startPos = transform.position; // 새로운 시작점 업데이트
        }

        moveVec = dirVec * monveSpeed * direction * Time.fixedDeltaTime;

        Vector3 currentVelocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
        previousPosition = transform.position;

        rigid.MovePosition(transform.position + moveVec);

        if (onPlatform)
        {
            CharacterManager.Instance.Player.playerController.PlatformSpeed(currentVelocity);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlatform = false;
            CharacterManager.Instance.Player.playerController.PlatformSpeed(Vector3.zero);
        }
    }

}
