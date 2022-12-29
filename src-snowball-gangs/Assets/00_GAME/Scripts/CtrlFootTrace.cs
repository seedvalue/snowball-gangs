using NTC.Global.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CtrlFootTrace : MonoBehaviour
{
    public static CtrlFootTrace Instance;

    [SerializeField]
    DecalProjector LeftFoot;
   



    public void ShowFootTraceLeft(Vector3 pos, Vector3 rot)
    {
        NightPool.Spawn(LeftFoot, pos, Quaternion.Euler(rot));
    }

    public void ShowFootTraceRight(Vector3 pos, Vector3 rot)
    {
        NightPool.Spawn(LeftFoot, pos, Quaternion.Euler(rot));
    }



    private void Awake()
    {
        Instance = this;
    }


}
