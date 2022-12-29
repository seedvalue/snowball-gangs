using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;
using Random = UnityEngine.Random;


public class CtrlCharSpawner : MonoBehaviour
{
    public static CtrlCharSpawner Instance;

    public GameObject enemyPrefab;
    public GameObject playersPrefab;
    
    
    public void SpawnPlayers(int count)
    {
        float minBetweenEnemy = 1.5F;
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.z = -7 + Random.Range(1F,-1F);
            pos.y = 1;
            pos.x =  i * minBetweenEnemy - minBetweenEnemy* count/2F;
            Quaternion rot = Quaternion.Euler(new Vector3(0F, 0, 0F));
            NightPool.Spawn(playersPrefab, pos, rot);
        }
        
    }
    
    
    public void SpawnEnemies(int count)
    {
        float minBetweenEnemy = 1.5F;
        for (int i = 0; i < count; i++)
        {
          Vector3 pos = Vector3.zero;
          pos.z = 14 + Random.Range(1F,-1F);
          pos.y = 1;
          pos.x =  i * minBetweenEnemy - minBetweenEnemy* count/2F;
          Quaternion rot = Quaternion.Euler(new Vector3(0F, 180F, 0F));
          NightPool.Spawn(enemyPrefab, pos, rot);
        }
        
    }
    
    
    private void Awake()
    {
        Instance = this;
    }
}
