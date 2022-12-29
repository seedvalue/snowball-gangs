using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;

public class CtrlGround : MonoBehaviour
{
  //  public static CtrlGround Instance;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CtrlGround : OnCollisionEnter " + collision.transform.tag);
        if (!collision.transform.CompareTag("Ball")) return;
        CtrlSound.Instance.PlaySnowballGrounded();
        var pos = collision.GetContact(0).point;
        var direction = collision.GetContact(0).normal;
        CtrlParticles.Instance.PlayPlayerSnowImpact(pos, direction);
        //CtrlParticles.Instance.SpawnDecalSnowBall(pos);
        CtrlFootTrace.Instance.ShowFootTraceLeft(pos, new Vector3(90F,0F,0F));


        CtrlSnowBall.Instance.KillSnowBall(collision.gameObject);
       
       
    }
    

    
        
    
    
    private void Awake()
    {
     //   Instance = this;
    }
}
