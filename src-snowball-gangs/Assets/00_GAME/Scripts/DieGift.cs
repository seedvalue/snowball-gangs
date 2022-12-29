using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DieGift : MonoBehaviour
{

    [SerializeField] Transform PivotBox;
    [SerializeField] Transform PivotTopBox;
    [SerializeField] Candy CandyPrefab;

    Tween _candyUpdateTween;


    [SerializeField] List<Transform> _candyPositions;

    private float _scaleStart = 1F;


    public bool TestOpenGift = false;


    private void OpenGift()
    {
        StartCoroutine(SpawnCandies());
        PivotTopBox.DOLocalMove(transform.up * 2F, 2F).SetEase(Ease.OutElastic);
        CtrlSound.Instance.PlayGiftOpen();
    }


    private IEnumerator SpawnCandies() 
    {
        foreach (var Item in _candyPositions)
        {
            yield return new WaitForSeconds(0.2F);
            SpawnCandy(Item.position);
            CtrlSound.Instance.PlayCandySpawn();
        }
        yield return new WaitForSeconds(1F);

        transform.DOScale(Vector3.zero, 1F);
        yield return new WaitForSeconds(2F);
        Destroy(gameObject);
    }



    private void SpawnCandy(Vector3 posEnd)
    {
        Candy candy = Instantiate(CandyPrefab, transform.position, Quaternion.identity);
        candy.transform.DOLocalJump(posEnd, 1F, 1, 1);
    }


    void Start()
    {
        _scaleStart = PivotBox.localScale.x;


        PivotBox.DORotate(new Vector3(0, -360, 0), 5F, RotateMode.LocalAxisAdd)
           .SetLoops(-1)
           .SetEase(Ease.Linear);

        PivotBox.transform.localScale = Vector3.one * _scaleStart;
        _candyUpdateTween = PivotBox.DOScale(1F, 2F).SetEase(Ease.OutElastic).SetLoops(-1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            OpenGift();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TestOpenGift)
        {
            OpenGift();
            TestOpenGift = false;
        }
    }
}
