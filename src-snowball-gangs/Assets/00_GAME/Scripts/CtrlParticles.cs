using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;

public class CtrlParticles : MonoBehaviour
{
    public static CtrlParticles Instance;
    
    [SerializeField]
    private Transform SnowImpactPrefab;
    
    [SerializeField]
    private Transform SnowBallDecalPrefab;

    public void PlayPlayerSnowImpact(Vector3 pos, Vector3 direction)
    {
        NightPool.Spawn(SnowImpactPrefab, pos, Quaternion.Euler(direction));
    }

    public void SpawnDecalSnowBall(Vector3 pos)
    {
        pos.y += 0.1F;
        NightPool.Spawn(SnowBallDecalPrefab, pos, Quaternion.Euler(new Vector3(89F,0F,0F)));
    }
    

    private void Awake()
    {
        Instance = this;
    }
}
