using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;


    public void WallMove(bool isWall)
    {
        animator.SetBool("WallMove", isWall);
    }

}
