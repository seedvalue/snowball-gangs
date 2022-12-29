using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using NTC.Global.Cache;

public class EnemyBrain : MonoCache
{
    private Character _character;
    
    private NavMeshAgent _agent;


    public Transform targetPos;


    private void Init()
    {
        _character = transform.GetComponent<Character>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;
        _agent.SetDestination(transform.position);

        if (CtrlCharactersData.Instance && !isRegistered)
        {
            CtrlCharactersData.Instance.RegistrateEnemy(gameObject);
            isRegistered = true;
        }
    }





    #region OLD

    float _rayDistance = 20F;

    

    private void ScanForward()
    {
        RaycastHit hit;
        int mask = 1 << LayerMask.NameToLayer("PlayerRaycast");
        if (Physics.Raycast(transform.position,transform.forward, out hit, _rayDistance, mask))
        {
            Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.blue, 1F);
            Debug.Log(hit.collider.name+ " has been shot");
         
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red, 1F);
                //PLAYER DETECTED
                float distance = hit.distance;
             _character.Attack();
            }
        }
    }


    private float CalculateForce()
    {
        return 10F;
    }

    #endregion


  

    private bool isRegistered = false;


    float navmeshDestinationX =0F;
    float navmeshDestinationZ = 0F;

    Vector3 destination;

    //X
    private float GetLeftRight()
    {
        if (targetPos)
        {
         return  targetPos.position.x;
        }
        return transform.position.x;
    }



    float distanceWithPplayerMin = 5F;
    float distanceWithPplayerMax = 15F;

    //Z
    private float GetForwardBack()
    {
        if (targetPos)
        {
            float z = targetPos.position.z;
            //add offset
            //z += UnityEngine.Random.Range(distanceWithPplayerMin, distanceWithPplayerMax);
            z += 5F;
            return z;
        }
        return transform.position.z;
    }


    private void UpdateNavmeshDestination()
    {
        if (targetPos)
        {
            destination.x = GetLeftRight();
            destination.y = transform.position.y;
            destination.z = GetForwardBack();
            if(!_agent.enabled) _agent.enabled = true;
            _agent.SetDestination(destination);
        }
    }


    AnimatorController _animatorController = null;

    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;


    void UpdateNavmeshCharackter()
    {
        Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.radius;

        // Update animation parameters
       //  anim.SetBool("move", shouldMove);
        _animatorController.SetVertical(velocity.x);
        _animatorController.SetHorisontal(velocity.y);

       // GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
    }




    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = _agent.nextPosition;
    }






    private void Awake()
    {
        Init();
    }


    private void Start()
    {
        InvokeRepeating("ScanForward", 5F, 1F);
        _animatorController = GetComponent<AnimatorController>();
      //  _agent.updatePosition = false;
    }


    Vector3 lerpedMovement = Vector3.zero;

    //private void Update()
    protected override void Run()
    {
        UpdateNavmeshDestination();
      //  UpdateNavmeshCharackter();
        //  lerpedMovement = Vector3.Lerp(lerpedMovement, _agent.velocity, Time.deltaTime * 10F);

       

       // if(SHOIs)
       // OnAnimatorMove();
    }

    protected override void FixedRun()
    {
        lerpedMovement = _agent.velocity.normalized;
        _animatorController.SetHorisontal(-lerpedMovement.z);
        _animatorController.SetVertical(-lerpedMovement.x);
    }
}
