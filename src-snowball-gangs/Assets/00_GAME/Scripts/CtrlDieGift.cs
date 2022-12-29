using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlDieGift : MonoBehaviour
{
    public static CtrlDieGift Instance;

    [SerializeField] private Transform _dieGiftPrefab;



    public bool isTestSpawn = false;


    public void SpawnGift(Vector3 pos)
    {
      //  Vector3 pos = new Vector3(Random.Range(1, 10), 0.5F, Random.Range(1, 10));
        Instantiate(_dieGiftPrefab, pos, Quaternion.identity);
    }




    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (isTestSpawn)
        {
           // SpawnGift();
            isTestSpawn = false;
        }
    }
}
