using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTC.Global.Pool;
using UnityEngine;
using NTC.Global.Cache;
using Unity.VisualScripting;

public class CtrlSnowBall : MonoCache
{
    public static CtrlSnowBall Instance;

    [SerializeField] private Transform snowballPrefab;

    [SerializeField] private Transform ParentForBalls;
    [SerializeField] private Transform ParentForDisabledBalls;

    public List<Transform> balls;

    /*
     проверка попаданий.
    +каждый трансформ м€чика хранить
    +провер€ть только включенный

    обнул€ть высоты м€чика,
    обнул€ть высоты плееров и енеми

    потом на основе этого смотреть дистанцию
    если дистанци€ менбше 0.5 тогда попал

    - отключить физику в м€чиках
    - удалить коллайдеры

    так же кроме проверки попаланий проверить  что м€ч ниже земли
     */

    private float _impactDistance = 0.5F;

    private bool isEditor = false;
    private void CheckEditor()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.LinuxEditor)
        {
            isEditor = true;
        }
    }


    private void UpdateBallImpact()
    {
        for(int i=0; i < balls.Count; i++)
        {
            if (balls[i].gameObject.activeSelf)
            {
                if (balls[i] == null) continue;
                //ћ€чик летит
                Transform impactedCharacter = ImpactedCharacter(balls[i]);
                if(impactedCharacter != null)
                {
                    //¬ кого то попали
                    //ќ“ѕ–ј¬»“№ “–јЌсформ
                    if (!isEditor) Debug.Log("<color=green> CtrlSnowBall </color: UpdateBallImpact : SOME HITED");
                    var component = impactedCharacter.GetComponent<Character>();
                    component.OnBallTouched();
                    CtrlParticles.Instance.PlayPlayerSnowImpact(balls[i].position, Vector3.up);
                    //var component2 = impactedCharacter.GetComponent<CharacterPlayer>();
                    //component2.OnBallTouched();
                    NightPool.Despawn(balls[i].gameObject);
                }
            }
        }
    }

    private Transform ImpactedCharacter(Transform ball)
    {
        Transform impactedChar = null;
        List<EnemyData> enemies =  CtrlCharactersData.Instance.enemies;
        List<PlayerData> humanPlayers = CtrlCharactersData.Instance.humanPlayers;
        Vector3 ballPos = ball.position;
        ballPos.y = 0;

        for (int i=0; i < humanPlayers.Count; i++)
        {
            //поиск по люд€м
            Vector3 pos = humanPlayers[i].playerTransform.position;
            pos.y = 0;
            float distance = Vector3.Distance(ballPos, pos);
            if(distance < _impactDistance)
            {
                //ѕќѕјЋ»
                impactedChar = humanPlayers[i].playerTransform;
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            //поиск по енеми
            if (enemies[i].enemyTransform == null) continue;
            
            Vector3 pos = enemies[i].enemyTransform.position;
            pos.y = 0;
            float distance = Vector3.Distance(ballPos, pos);
            if (distance < _impactDistance)
            {
                //ѕќѕјЋ»
                impactedChar = enemies[i].enemyTransform;
            }
        }


        return impactedChar;
    }

    private void UpdateBallGrounded()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].gameObject.activeSelf)
            {
                if (balls[i].position.y < 0)
                {
                    NightPool.Despawn(balls[i].gameObject);
                }
            }
        }

    }

    private float savedForce = 0F;

    public void SaveForce(float force)
    {
        savedForce = force;
    }

    public void DropBall(Vector3 from, Vector3 direction)
    {
        if (!isEditor) Debug.Log("CtrlSnowBall : DropBall");
        Transform ball = null;
        ball = NightPool.Spawn(snowballPrefab, from, Quaternion.identity);

        bool alreadyExist = balls.Contains(ball);
        if(!alreadyExist)
        {
            balls.Add(ball);
        }
        

        //var rig = ball.GetComponent<Rigidbody>();
        //rig.velocity = Vector3.zero;
        //rig.AddForce(direction * savedForce * 3F, ForceMode.Impulse);
        Vector3 endPose = from + direction * savedForce * 10F;
        endPose.y = -0.3f;
        FropBallByTweener(ball, endPose, savedForce);
    }

    private void FropBallByTweener(Transform ball, Vector3 endPos, float force)
    {
        float duration = 2F;// force * 2F;
        float power = force * 3;
        ball.DOJump(endPos, power, 1 , duration).SetEase(Ease.OutQuint);
    }


    public void KillSnowBall(GameObject ball)
    {
        ball.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        NightPool.Despawn(ball, 0.0F);
    }

    /*
    private Transform GetExistedBall()
    {
       Transform ball = ParentForDisabledBalls.GetChild(0);
       ball.SetParent(ParentForBalls);
       return ball;
    }
    
    private Transform GetNewBall()
    {
       Transform ball = Instantiate(snowballPrefab, Vector3.zero, Quaternion.identity);
       ball.SetParent(ParentForBalls);
       return ball;
    }
    
    
    private bool IsHaveBall()
    {
       if (ParentForDisabledBalls.childCount > 0)
       {
          return true;
       }
       return false;
    }
    */
    private void Awake()
    {
        Instance = this;
        //CheckEditor();
        isEditor = true;
    }

    protected override void Run()
    {
        UpdateBallImpact();
        UpdateBallGrounded();
    }
}