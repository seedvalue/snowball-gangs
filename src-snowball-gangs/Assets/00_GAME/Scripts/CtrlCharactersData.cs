using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCharactersData : MonoBehaviour
{
    public static CtrlCharactersData Instance;

    public bool IsShowTargets = true;


    public List<EnemyData> enemies = new List<EnemyData>();
    public List<PlayerData> humanPlayers = new List<PlayerData>();



    private void RefreshGameStatus()
    {
        int enemyesLive = GetEnemiesLive();
        int playersLive = GetPlayersLive();
        
        if(enemyesLive == 0 || playersLive == 0)
        {
            CtrlGame.Instance.OnSomeWin(playersLive, enemyesLive);
        }
    }


    private int GetPlayersLive()
    {
        int live = 0;
        foreach (PlayerData e in humanPlayers)
        {
            if (e != null)
            {
                if (e.IsDied == false)
                {
                    live++;
                }
            }
        }
        return live;
    }

    private int GetEnemiesLive()
    {
        int live = 0;
        foreach (EnemyData e in enemies)
        {
            if (e != null)
            {
                if (e.IsDied == false)
                {
                    live++;
                }
            }
        }
        return live;
    }


    public void OnDiePlayer(Character val)
    {
        foreach (PlayerData e in humanPlayers)
        {
            if(e != null)
            {
                if(e.playerTransform == val.transform)
                {
                    e.IsDied = true;
                    RefreshGameStatus();
                    return;
                }
            }
        }
        
    }

    public void OnDieEnemy(Character val)
    {
        foreach (EnemyData e in enemies)
        {
            if (e != null)
            {
                if (e.enemyTransform == val.transform)
                {
                    e.IsDied = true;
                    RefreshGameStatus();
                    return;
                }
            }
        }
       
    }


    public void CleanAll()
    {
        foreach(var item in enemies)
        {
            if(item.enemyTransform)
            Destroy(item.enemyTransform.gameObject);
        }

        foreach (var item in humanPlayers)
        {
            if(item.playerTransform)
            Destroy(item.playerTransform.gameObject);
        }

        enemies.Clear();
        humanPlayers.Clear();
        enemies = new List<EnemyData>();
        humanPlayers = new List<PlayerData>();  
    }


    public void RegistrateEnemy(GameObject enemy)
    {
        EnemyData enemyData = new EnemyData();
        enemyData.enemyTransform = enemy.transform;
        enemyData.targetTransform =  GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        enemyData.targetTransform.localScale = Vector3.one * 0.2F;
        if (!IsShowTargets)
        {
            enemyData.targetTransform.GetComponent<MeshRenderer>().enabled = false;
        }
        enemyData.targetTransform.name = "EnemyTarget_" + enemy.name;
        enemyData.targetTransform.position = enemy.transform.position;
        enemyData.enemyBrain = enemy.transform.GetComponent<EnemyBrain>();
        enemyData.enemyBrain.targetPos = enemyData.targetTransform;
        enemies.Add(enemyData);
    }

    public void RegistratePlayer(GameObject player)
    {
        PlayerData playerData = new PlayerData();
        playerData.playerTransform = player.transform;
        humanPlayers.Add(playerData);
    }



    private float refreshTimeEverySec = 1F;

    private void RefreshTargets()
    {
       for(int i = 0; i< enemies.Count; i++)
        {
            var freePlayer = GetFreePlayer();
            if(freePlayer !=null)
            {
                enemies[i].player = freePlayer.playerTransform;
                enemies[i].targetTransform.position = freePlayer.playerTransform.position;
                freePlayer.assignedEnemy = enemies[i].enemyTransform;
            }
        }


       //refresh cubes
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].targetTransform.position = enemies[i].player.position;
        }


        //   var freeEnemy = GetFreeEnemy();
        //  var freePlayer = GetFreePlayer();

        /*
        foreach(var enemy in enemies) 
        {
           if (enemy.player)
            {
                //alreadyHave player target
                continue;
            }
            Transform player = GetTargetFromPlayer(enemy.enemyTransform);
            enemy.player = player;
            player.

            Vector3 minimalPlayerPos = player.position;
            minimalPlayerPos.z = enemy.enemyTransform.position.z;
            enemy.targetTransform.position = minimalPlayerPos;
        }*/
    }


    private Transform GetTargetFromPlayer(Transform enemyTransform)
    {
        float distance = float.MaxValue;
        Transform minimalPlayer = null;

        foreach (var player in humanPlayers)
        {
            if (player.assignedEnemy != null) continue;
            minimalPlayer = player.playerTransform;
            /*
            float curDist = Vector3.Distance(enemyTransform.position, player.playerTransform.position);
            if (curDist < distance)
            {
                distance = curDist;
                minimalPlayer = player.playerTransform;
            }*/
        }

            return minimalPlayer;
    }


    private PlayerData GetFreePlayer()
    {
        foreach (var player in humanPlayers)
        {
            if (player.assignedEnemy != null) continue;
            else return player;
        }
        return null;
    }


    private EnemyData GetFreeEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.player != null) continue;
            else return enemy;
        }
        return null;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("RefreshTargets", refreshTimeEverySec, refreshTimeEverySec);
    }



}

[Serializable]
public class EnemyData
{
    public bool IsDied = false;
    public Transform enemyTransform;
    public Transform targetTransform;
    public EnemyBrain enemyBrain;
    public Transform player;
}

[Serializable]
public class PlayerData
{
    public bool IsDied = false;
    public Transform playerTransform;
    public Transform assignedEnemy;

}