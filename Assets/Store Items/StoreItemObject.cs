using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreItem", menuName = "Store Item Object")]
public class StoreItemObject : ScriptableObject
{
    public string itemName;
    public int itemPrice;
    public Sprite itemImage;
    public bool isBonus;
    public bool isBonusMulti;
}
