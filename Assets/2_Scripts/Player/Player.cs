using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    public AnimationHandler animationHandler;
    public Interaction interaction;
    public ItemDate itemData;
    public Transform ModelTransform;

    // Start is called before the first frame update
    void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        playerCondition = GetComponent<PlayerCondition>();
        interaction = GetComponent<Interaction>();
        animationHandler = GetComponent<AnimationHandler>();
        
    }

}
