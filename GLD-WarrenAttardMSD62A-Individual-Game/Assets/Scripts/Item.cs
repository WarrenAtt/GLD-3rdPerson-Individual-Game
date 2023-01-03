using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptiable object/Item")]
public class Item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public Vector2Int range = new Vector2Int(5, 4);
    public bool stackable;
}

public enum ItemType
{
    Wood,
    Iron,
    Pickaxe,
    Axe
}
