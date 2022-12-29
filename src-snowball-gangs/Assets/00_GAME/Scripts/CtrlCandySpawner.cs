using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTC.Global.Pool;


public class CtrlCandySpawner : MonoBehaviour
{
    public static CtrlCandySpawner Instance;


    [SerializeField] private Transform _candyPrefab;


    public void SpawnCandy(Vector3 pos)
    {
        NightPool.Spawn(_candyPrefab,pos);
    }

    public void OnTweenEnd (Candy candy)
    {

        CtrlGame.Instance.OnCandyGet();
        NightPool.Despawn(candy.gameObject);
    }

    public void OnTweenEndEnemyGeted(Candy candy)
    {

        CtrlGame.Instance.OnCandyGetEnemy();
        // CtrlGame.Instance.OnCandyGet();
        NightPool.Despawn(candy.gameObject);
    }


    private void Awake()
    {
        Instance = this;
    }
}
