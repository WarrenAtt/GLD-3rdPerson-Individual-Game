using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptiable object/Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite image;
    public ItemType type;
    public bool stackable;
    public int sellValue;
    public int quantity;
}

public enum ItemType
{
    Wood,
    Iron,
    Pickaxe,
    Axe
}
