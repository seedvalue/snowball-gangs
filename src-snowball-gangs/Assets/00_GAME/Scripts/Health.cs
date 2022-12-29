using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetHealth(float health)
    {
        if(slider != null)
        slider.value = health;
    }
}
