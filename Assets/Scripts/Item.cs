using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject 
{
    public ItemType Type;
    public Sprite Sprite;
}

public enum ItemType
{
    Spring,
    Ramp,
    Bomb
}
