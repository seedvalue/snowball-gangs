using NTC.Global.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.Universal;
using NTC.Global.Cache;
public class CtrlFootTrace : MonoCache
{
    public static CtrlFootTrace Instance;

    bool decalsIsDisabled = true;


  //  [SerializeField]
   // DecalProjector LeftFoot;

  //  [SerializeField]
  //  DecalProjector DoubleFoot;

   // [SerializeField]
   // List<DecalProjector> decalProjectors = new List<DecalProjector>();

   

    public void ShowFootTraceLeft(Vector3 pos, Vector3 rot)
    {
        if (decalsIsDisabled) return;
        
      //  DecalProjector trace = NightPool.Spawn(LeftFoot, pos, Quaternion.Euler(rot));
     //   decalProjectors.Add(trace);
      //  SetParent(trace.transform);
    }

    public void ShowFootTraceRight(Vector3 pos, Vector3 rot)
    {
      //  if (decalsIsDisabled) return;
      //  DecalProjector trace = NightPool.Spawn(LeftFoot, pos, Quaternion.Euler(rot));
     //   decalProjectors.Add(trace);
     //   SetParent(trace.transform);
    }

    private void SetParent(Transform t)
    {
        t.SetParent(transform);
    }


    float fadeStep = 0.05F;
    int frame = 0;
    int refreshEveryframe = 5;

    private void UpdateDecalClean()
    {
        if (decalsIsDisabled) return;
        frame++;
        if (frame >= refreshEveryframe)
        {
            frame = 0;
            Clean();
        }
    }

    private void Clean()
    {
      /*
        for (int i = 0; i < decalProjectors.Count; i++)
        {
            decalProjectors[i].fadeFactor -= fadeStep;
            if (decalProjectors[i].fadeFactor <= 0.2F)
            {
                NightPool.Despawn(decalProjectors[i].gameObject);
            }
        }
      */
    }



    private void Awake()
    {
        Instance = this;
    }

    protected override void Run()
    {
        if (decalsIsDisabled) return;
       // UpdateDecalClean();
    }
}
