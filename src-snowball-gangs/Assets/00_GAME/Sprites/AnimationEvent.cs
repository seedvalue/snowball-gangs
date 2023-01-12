using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{

    public Action ActionAnimationEndAttack;
    public Action ActionAnimationEndDie;


    public Action ActionFootDownLeft;
    public Action ActionFootDownRight;


    private bool isEditor = false;
    private void CheckEditor()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.LinuxEditor)
        {
            isEditor = true;
        }
    }


    public void OnAnimationEndAttack()
    {
       if(!isEditor) Debug.Log("AnimationEvent : OnAnimationEndAttack : ");
        ActionAnimationEndAttack?.Invoke();
    }
    
    public void OnAnimationEndDie()
    {
        if (!isEditor) Debug.Log("AnimationEvent : OnAnimationEndDie : ");
        ActionAnimationEndDie?.Invoke();
    }

    public void OnFootDownLeft()
    {
        ActionFootDownLeft?.Invoke();
    }

    public void OnFootDownRight()
    {
        ActionFootDownRight?.Invoke();
    }

   

    private void Awake()
    {
        //CheckEditor();
        isEditor = true;
    }
}
