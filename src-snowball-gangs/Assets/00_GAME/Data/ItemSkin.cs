using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemSkin", order = 1)]
public class ItemSkin : ScriptableObject
{
    [SerializeField] public int numSorting;
    [SerializeField] public bool isFree;
    [SerializeField] public int Price;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Texture2D Icon;

    [SerializeField] public string AssetName;
    [SerializeField] public string ModelName;
}
