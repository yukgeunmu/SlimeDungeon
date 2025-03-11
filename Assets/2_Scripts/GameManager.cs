using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CameraMovement cameraMovement;
    public CameraMovement CameraMovement
    {
        get => cameraMovement;
        set => cameraMovement = value;
    }

    public TootipUI tootipUI;
    public TootipUI TootipUI
    {
        get => tootipUI;
        set => tootipUI = value;
    }


}
