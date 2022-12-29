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

    public void OnAnimationEndAttack()
    {
        Debug.Log("AnimationEvent : OnAnimationEndAttack : ");
        ActionAnimationEndAttack?.Invoke();
    }
    
    public void OnAnimationEndDie()
    {
        Debug.Log("AnimationEvent : OnAnimationEndDie : ");
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
}
