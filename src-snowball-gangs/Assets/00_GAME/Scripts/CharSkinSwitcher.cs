using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSkinSwitcher : MonoBehaviour
{
    [SerializeField]
    private List<SkinSwitcherOne> _skins;
    
    public void SelectSkin(string skinName)
    {
        foreach (var one in _skins) 
        {
            if(one.name == skinName)
            {
                one.Pivot.SetActive(true);
               // return;
            }
            else one.Pivot.SetActive(false);
        }
    }
}

[Serializable]
public class SkinSwitcherOne 
{
    public string name;
    public GameObject Pivot;
}
