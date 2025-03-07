using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Pad,
    Consumalbe,
    Equipable
}

public enum ConsumableType
{
    Health,
    Stamina,
    Speed
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemDate : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string descroption;
    public ItemType type;

    [Header("Consumalbe")]
    public ItemDataConsumable[] consumables;
}
