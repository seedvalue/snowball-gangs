using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using NTC.Global.Cache;

public class DragObject : MonoCache
{

    [SerializeField] private float heightOffset = 0F;
    
    [SerializeField] private GameObject SelectionMarker;
    
    [SerializeField] private bool isHideCursorWhenTouch = true;
    //Fill in inspector some tags like a Player or other tags 
    [SerializeField] private string[] tagsCanGrab;
    [SerializeField] private bool isDebugLogTouches = true;
    

    [Header("Debug:"), Tooltip("Just for can see in Play mode")]

    [SerializeField] private GameObject selectedObject;
   // [SerializeField] private Vector2 touchPos = Vector2.zero;
    [Space]
    [SerializeField] private Vector3 touchWorldPosNear = Vector3.zero;
    [SerializeField] private Vector3 touchWorldPosFar = Vector3.zero;
    [Space]
    [SerializeField] private TouchPhase touchPhase = TouchPhase.Ended;
    [SerializeField] private Camera currentCamera;


    Vector3 curDragOffset = Vector3.zero;

    private void OnTouchBegan(Vector2 touchPos)
    {
     if(isDebugLogTouches)   Debug.LogError("OnTouchBegan");
        var hit = CastRay(touchPos);
        if (!hit.collider) return;
        if (!CompareTagInTagList(hit.collider)) return;
        if (isDebugLogTouches) Debug.LogError("!!!!!!!!!!");
        SelectObj(hit.collider.transform.parent.gameObject);
        curDragOffset = hit.collider.transform.parent.position - hit.point;
        if (isHideCursorWhenTouch) Cursor.visible = false;
    }

    private bool CompareTagInTagList(Collider other)
    {
        return tagsCanGrab.Any(other.CompareTag);
    }
    
    private void UpdateTouches()
    {
        if (Touch.activeFingers.Count <= 0)
        {
            touchWorldPosNear = Vector3.zero;
            touchWorldPosFar = Vector3.zero;
            DeselectObj();
            return;
        }
        
        Touch activeTouch = Touch.activeFingers[0].currentTouch;

        
        if (activeTouch.began)
        {
            OnTouchBegan(activeTouch.startScreenPosition);
         //   ShowSelectionMarker(true);
           // OnSelected(selectedObject);
        }
        
        if (activeTouch.ended)
        {
            ShowSelectionMarker(false);
            OnSelected(selectedObject, false);
            DeselectObj();
            //OnSelected(selectedObject, false);
        }
            
        if (activeTouch.isInProgress)
        {
            Dragging(activeTouch.screenPosition);
            OnSelected(selectedObject, true);
          //  ShowSelectionMarker(true);
        }
        else
        {
            OnSelected(selectedObject, false);
        }
        
        if(isDebugLogTouches)   Debug.Log($"Phase: {touchPhase} | Position: {activeTouch.screenPosition}");

    }

    private void ShowSelectionMarker(bool isShow)
    {
        if(SelectionMarker)SelectionMarker.SetActive(isShow);
        CtrlForce.Instance.OnShowSlider(isShow);
    }

    private void SetPositionMarker()
    {
        if (SelectionMarker && selectedObject)
            SelectionMarker.transform.position = selectedObject.transform.position;
    }

    private RaycastHit CastRay(Vector2 touchPos) 
    {
        Vector3 screenMousePosFar = new Vector3(touchPos.x, touchPos.y, currentCamera.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(touchPos.x, touchPos.y, currentCamera.nearClipPlane);
        touchWorldPosFar = currentCamera.ScreenToWorldPoint(screenMousePosFar);
        touchWorldPosNear = currentCamera.ScreenToWorldPoint(screenMousePosNear);
        Physics.Raycast(touchWorldPosNear, touchWorldPosFar - touchWorldPosNear, out var hit);
        return hit;
    }

    //NOT USED OLD
    
    
    private void Dragging(Vector2 screenPos)
    {
        if(!selectedObject) return;
        ShowSelectionMarker(true);
        SetPositionMarker();
        Vector3 position = new Vector3(screenPos.x, screenPos.y, currentCamera.WorldToScreenPoint(selectedObject.transform.position).z);
        Vector3 worldPosition = currentCamera.ScreenToWorldPoint(position);
       // Vector3 offsetted = worldPosition - curDragOffset;
        float y = selectedObject.transform.position.y;
        Vector3 pos = new Vector3(worldPosition.x, y, worldPosition.z);

        selectedObject.transform.position = pos;// - curDragOffset;

        //TODO Поворот потом если надо будет. Пока не удаляю
        /*
         if (Input.GetMouseButtonDown(1)) {
            selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                selectedObject.transform.rotation.eulerAngles.x,
                selectedObject.transform.rotation.eulerAngles.y + 90f,
                selectedObject.transform.rotation.eulerAngles.z));
                */
    }



    private void SelectObj(GameObject obj)
    {
        selectedObject = obj;
     //  this.OnSelected(selectedObject);
    }

    private void DeselectObj()
    {
      //  if(!selectedObject) return;
     //   this.OnDeSelected(selectedObject);
        selectedObject = null;
    }
    
   

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(touchWorldPosNear, 1);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(touchWorldPosFar, 1);
        */
    }


    private CharacterPlayer SelectedPLayer = null;
    
    //TODO костыль. отвязат как то
    private void OnSelected(GameObject t, bool isSelected)
    {
        if (isSelected == false)
        {
            if(SelectedPLayer)SelectedPLayer.SetPlayerSelected(false);
            SelectedPLayer = null;
        }
        else
        {
            if (!SelectedPLayer)
                if (t)
                {
                    SelectedPLayer = t.transform.GetComponent<CharacterPlayer>();
                }
            if(SelectedPLayer)SelectedPLayer.SetPlayerSelected(true);
        }
    }

    #region UNITY

    private void Start()
    {
        currentCamera = Camera.main;
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        ShowSelectionMarker(false);
    }

    //void Update ()
    protected override void Run()
    {
        UpdateTouches();
    }

    #endregion
    
    
} 
