using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKill : MonoBehaviour
{
    public bool isKill = false;
    
    void Awake()
    {
        if(isKill)
        Destroy(gameObject);
    }
}
