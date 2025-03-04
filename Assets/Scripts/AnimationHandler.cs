using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveAnimation(Vector2 obj)
    {
        animator.SetBool("Walk", obj.magnitude > 0.5f);
    }

    public void JumpAnimation()
    {
        animator.SetBool("Jump", true);
    }

}
