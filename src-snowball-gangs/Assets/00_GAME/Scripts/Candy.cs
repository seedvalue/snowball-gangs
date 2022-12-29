using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;




public class Candy : MonoBehaviour
{
    [SerializeField]
    private Transform PivotAnimate;



    private void GetCandy()
    {
        Vector3 toPose = (Camera.main.transform.position);
        toPose.y += 1F;
        transform.DOMove(toPose, 1F).OnComplete(() => CtrlCandySpawner.Instance.OnTweenEnd(this));
        CtrlSound.Instance.PlayCandyTouch();
    }

    private void LooseCandy(Transform enemy)
    {

        Vector3 pos = enemy.position;
        pos.y = 1F;
        CtrlSound.Instance.PlayCandyEnemyTouch();
        transform.DOJump(pos, 1,1,1).OnComplete(() => CtrlCandySpawner.Instance.OnTweenEndEnemyGeted(this));
        transform.DOScale(Vector3.zero, 1).SetEase(Ease.InOutElastic);
    }



    private IEnumerator DelayedRand()
    {
        yield return new WaitForSeconds(Random.Range(0, 1F));
        PivotAnimate.DORotate(new Vector3(0, -360, 0), 1F, RotateMode.LocalAxisAdd)
           .SetLoops(-1)
           .SetEase(Ease.Linear);
        PivotAnimate.DOScale(1.2F, 1F)
            .SetEase(Ease.InOutElastic)
            .SetLoops(-1);

    }

    void Start()
    {
        StartCoroutine(DelayedRand());
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetCandy();
        }

        if (other.CompareTag("Enemy"))
        {
            LooseCandy(other.transform);
        }
    }



}
