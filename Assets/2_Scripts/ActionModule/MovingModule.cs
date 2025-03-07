using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Dierction
{
    Foward,
    Right
}


public class MovingModule : MonoBehaviour
{

    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    private Vector3 startPos;
    private int direction = 1;

    private void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // 이동
        transform.position += Vector3.forward * moveSpeed * direction * Time.deltaTime;

        // 이동 거리 초과 시 방향 전환
        if (Mathf.Abs(transform.position.z - startPos.z) >= moveDistance)
        {
            direction *= -1;
        }
    }

}
