using NTC.Global.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using NTC.Global.Cache;

public class FootDecal : MonoCache
{
    [SerializeField] DecalProjector _decal;

    float opacity;

    private IEnumerator HideDecal(float time)
    {
        yield return new WaitForSeconds(time);
        NightPool.Despawn(gameObject);
    }



    // private void OnEnable()
    protected override void OnEnabled()
    {
        // StartCoroutine(HideDecal(10F));
        _decal.fadeFactor = opacity;
    }

    private void Awake()
    {
        opacity = _decal.fadeFactor;
    }

    //void Update()
    protected override void Run()
    {
        _decal.fadeFactor = Mathf.Lerp(_decal.fadeFactor, 0F, Time.deltaTime * 0.3F);
        if(_decal.fadeFactor <= 0.2F)
        {
            NightPool.Despawn(gameObject);
        }
    }

}
