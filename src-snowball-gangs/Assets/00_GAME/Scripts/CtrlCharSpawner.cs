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
    
    private ShopData _shopData;
    
    public void SpawnPlayers(int count, string skinName)
    {
        Debug.Log("CtrlCharSpawner : SpawnPlayers : count = " + count + " " + skinName);
        float minBetweenEnemy = 1.5F;
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.z = -7 + Random.Range(1F,-1F);
            pos.y = 1;
            pos.x =  i * minBetweenEnemy - minBetweenEnemy* count/2F;
            Quaternion rot = Quaternion.Euler(new Vector3(0F, 0, 0F));
            var spawned = NightPool.Spawn(playersPrefab, pos, rot);
            SetPlayerSkin(spawned, skinName);
        }
    }
    
    
    public void SpawnEnemies(int count, string skinName)
    {
        Debug.Log("CtrlCharSpawner : SpawnEnemies : count = " + count + " " + skinName);
        float minBetweenEnemy = 1.5F;
        for (int i = 0; i < count; i++)
        {
          Vector3 pos = Vector3.zero;
          pos.z = 14 + Random.Range(1F,-1F);
          pos.y = 1;
          pos.x =  i * minBetweenEnemy - minBetweenEnemy* count/2F;
          Quaternion rot = Quaternion.Euler(new Vector3(0F, 180F, 0F));
            var spawned = NightPool.Spawn(enemyPrefab, pos, rot);
            SetPlayerSkin(spawned, skinName);
        }
    }
    

    private void SetPlayerSkin(GameObject spawnedChar, string name)
    {
        CharSkinSwitcher switcher = spawnedChar.transform.GetComponent<CharSkinSwitcher>();
        if (switcher != null) {
            switcher.SelectSkin(name);
        }
    }


    private void Awake()
    {
        Instance = this;
    }
}
