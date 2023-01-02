using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameItem", menuName = "Game Items/Create Game item")]
public class ItemScriptableObject : ScriptableObject
{
    public string title;
    public Sprite icon;
    public int sellValue;
    public int quantity;
    public Type type; //stores either health or mana (not both at the same time)

    public enum Type
    {
        Wood,
        Iron,
        Pickaxe,
        Axe
    }
}

