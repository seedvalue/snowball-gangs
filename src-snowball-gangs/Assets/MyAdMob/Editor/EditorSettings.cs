using UnityEditor;
using UnityEngine;

namespace MyAdMob.Editor
{
    [CustomEditor(typeof(MyAdsConfig))]
    [CanEditMultipleObjects]
    public class EditorSettings : UnityEditor.Editor 
    {


        void OnGUI()
        {
            if (GUILayout.Button("I am a regular Automatic Layout Button"))
            {
                Debug.Log("Clicked Button");
            }
        }
    }
}
