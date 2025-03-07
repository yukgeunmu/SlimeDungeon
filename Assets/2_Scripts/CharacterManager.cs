using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{

    public Player _player;

    public Player Player
    {
        get => _player;
        set => _player = value;
    }

}
