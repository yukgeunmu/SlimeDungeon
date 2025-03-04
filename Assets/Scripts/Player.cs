using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCondition playerCondition;

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        playerCondition = GetComponent<PlayerCondition>();
        
    }

}
