using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WndGamePlay : MonoBehaviour
{
    [SerializeField]
    private Button ButtonPause;

    [SerializeField] TMPro.TMP_Text TextCandyVal;
    [SerializeField] Transform OnCandyChangeAnimation;

    Tween _candyUpdateTween = null;


    public bool teestAnim = false;
    private void Update()
    {

        if (teestAnim)
        {
            int rand = UnityEngine.Random.Range(1, 1000);
            SetCandy(rand);
            teestAnim = false;
        }
    }




    public void SetCandy(int val)
    {
        _candyUpdateTween.Kill();
        TextCandyVal.text = val.ToString();
        OnCandyChangeAnimation.transform.localScale = Vector3.one * 0.1F;
        _candyUpdateTween = OnCandyChangeAnimation.DOScale(1F, 1F).SetEase(Ease.OutElastic);
    }



    private void OnClickPause()
    {
        CtrlUi.Instance.ShowPause();
       
    }

    private void Start()
    {
    ButtonPause.onClick.AddListener(OnClickPause);    
    }
}
