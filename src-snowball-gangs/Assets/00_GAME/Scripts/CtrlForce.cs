using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using NTC.Global.Cache;
using DG.Tweening;

public class CtrlForce : MonoCache
{
    public static CtrlForce Instance;
    [SerializeField] private DecalProjector _decalProjector;
    [SerializeField] private Slider _sliderForce;

    [field: Range(0F, 1F)]
    [field: SerializeField]
    public float CurrForce { get; private set; } = 0F;


    [SerializeField] private float baseSpeed = 1F;


    private Vector3 _sizeStart;


    private int dirIncrease = 1;
    [Range(0.5F, 5F)]
    public float speedChangeForce = 1F;

    Tween speedMotion;
   
    
    void UpdateSpeedPingPong()
    {
       // _decalProjector.size = _sizeStart * currSpeed + Vector3.one * 1.0F;
       // _decalProjector.transform.Rotate(-Vector3.forward, currSpeed * 100F * Time.deltaTime);


        CurrForce += Time.deltaTime * dirIncrease * speedChangeForce;
        if (CurrForce >= 1F)
        {
            dirIncrease = -1;
        // CurrForce = 0F;
        }
        if (CurrForce < 0F)
        {
            dirIncrease = 1;
        }
    }

    private void UpdateDecal()
    {

        Vector2 offset = _decalProjector.uvBias;
        offset.y -= (CurrForce * Time.deltaTime + baseSpeed * Time.deltaTime);
        _decalProjector.uvBias = offset;

        Vector2 tiling = _decalProjector.uvScale;
       
        // 1 полный размер
        // 2 делится
        // чем больше число тем больше делений
      //  tiling.y = 1F/currSpeed;
       // _decalProjector.uvScale = tiling;
        //_decalProjector.material.mainTextureOffset = offset;
    }


    private void UpdateSlider(float val)
    {
        _sliderForce.value = CurrForce;
    }
    
    //private void Update()
    protected override void Run()
    {
       // UpdateSpeedPingPong();
    //    UpdateSlider();
        //  UpdateDecal();
    }

    private void Awake()
    {
        //_sizeStart = _decalProjector.size;
        Instance = this;
    }


    private float _duration = 1F;



    private bool IsShowedSlider = false;

    public void OnShowSlider(bool isShow)
    {
        //Избежать повтроного вызова,тк там апдейт
        if (IsShowedSlider == isShow) return;
        
        Debug.Log("CtrlForce : OnShowSlider :"+ isShow.ToString());
        if(isShow)
        {
            speedMotion = DOVirtual.Float(0F, 1F, _duration, SetFloat)
                .SetLoops(-1, LoopType.Yoyo); ;
        } else
        {
            speedMotion.Kill();
        }
        IsShowedSlider = isShow;
    }

    private void SetFloat(float val)
    {
        CurrForce = val;
        UpdateSlider(CurrForce);
    }
}
