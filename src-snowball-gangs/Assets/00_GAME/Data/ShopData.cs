using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShopData", order = 1)]

public class ShopData : ScriptableObject
{
    [SerializeField]
    public List<ItemSkin> itemSkins = new List<ItemSkin>();
}
