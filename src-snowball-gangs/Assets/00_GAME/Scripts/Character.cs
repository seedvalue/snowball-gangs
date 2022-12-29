using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
   //[SerializeField] private GameObject SelectionDecal;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AnimatorController _animatorController;
    [SerializeField] private AnimationEvent _animationEvent;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _ballDropPos;


    [SerializeField] private Health healthUi;
    [SerializeField] private int _health = 10;

    //для отключение после смерти
    [SerializeField] protected BoxCollider _DraggableCollider;

    private int _currentHealth = 0;


    private Vector3 localPos;
    private Quaternion localRot;





    #region MOVING

    private CharacterController _characterController;
    
    private Vector3 _moving = Vector3.forward;
    private float _movingSpeed = 0.1F;
    
    private Vector3 _wayPoint;


    public bool isDisabledMovement = false;
    public bool _isPlayerChar = false;


    private Vector3 curPos;
    private Vector3 lastTimePos;


    private bool isMovingNow = false;
    
    
   
    
    
    

    #endregion
    
    public void Attack()
    {
        _animator.SetTrigger(Attack1);
        CtrlSnowBall.Instance.SaveForce(CtrlForce.Instance.CurrForce);
    }

    public void AttackEnemy(float force)
    {
        _animator.SetTrigger(Attack1);
        CtrlSnowBall.Instance.SaveForce(CtrlForce.Instance.CurrForce);
    }
    
    private float force = 2F;
    private static readonly int Attack1 = Animator.StringToHash("Attack");




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            OnDamaged();
            Vector3 point = collision.contacts[0].point;
            Vector3 normal = collision.contacts[0].normal;
            CtrlParticles.Instance.PlayPlayerSnowImpact(point, normal);
        }
        
    }

    public void OnBallTouched()
    {
        OnDamaged();
        Vector3 point = transform.position;
    }

    private void OnDamaged()
    {
        _currentHealth--;
        if (_currentHealth <= -1)
        {
            if(_isPlayerChar) return;
            
           // CtrlCandySpawner.Instance.SpawnCandy(transform.position);
            CtrlDieGift.Instance.SpawnGift(transform.position);
            CtrlSound.Instance.PlayDamagedRandom();
            Destroy(gameObject);
            return;
        }

            SetHealthUi(_currentHealth, _health);
        if (_currentHealth <= 0)
        {
            Die();
        } else
        {
            _animatorController.OnDamaged();
            CtrlSound.Instance.PlaySnowFace();
            CtrlSound.Instance.PlayDamagedRandom();
        }
    }


    private void Die()
    {
        _animatorController.OnDied();
        if (_DraggableCollider != null) _DraggableCollider.enabled = false;

        ShowHealthUi(false);
        CtrlSound.Instance.PlaySnowFace();
        CtrlSound.Instance.PlayEnemyDie();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if(agent) agent.isStopped = true;
        if (_isPlayerChar) GetComponent<BoxCollider>().enabled = false;
        
        if (_isPlayerChar) CtrlCharactersData.Instance.OnDiePlayer(this);
        else CtrlCharactersData.Instance.OnDieEnemy(this);
    }

    private void SetHealthUi(int curHealth, int maxHealth)
    {
        float cur = curHealth;
        float max = maxHealth;
        float sliderVal = cur/ max;
        if (healthUi) healthUi.SetHealth(sliderVal);
    }

    private void ShowHealthUi(bool isShow)
    {
        if(healthUi) healthUi.gameObject.SetActive(isShow);
    }





    #region EVENTS


    [SerializeField]
    private Transform _leftFoot;
    [SerializeField]
    private Transform _rightFoot;


    float upFactor = 0.2F;

    public void OnAnimationEndAttack()
    {
        //  Debug.Log("Character : OnAnimationEndAttack : ");
        Vector3 fwd = transform.forward;
        fwd.y += upFactor;
        CtrlSnowBall.Instance.DropBall(_ballDropPos.position, fwd);
        CtrlSound.Instance.PlayWoosh();
    }

    public void OnAnimationEndDie()
    {
       // Debug.Log("Character : OnAnimationEndDie : ");
        Vector3 pos = transform.position;
        pos.y = 0;
        CtrlParticles.Instance.PlayPlayerSnowImpact(pos, Vector3.up);
    }

    public void OnAnimationFootLeft()
    {
       // Debug.Log("Character : OnAnimationFootLeft : ");
        Vector3 pos = _leftFoot.position;
        pos.y = 0.3F;
        CtrlSound.Instance.PlayFootStep();
        CtrlFootTrace.Instance.ShowFootTraceLeft(pos, new Vector3(90F, 0, 0));
    }

    public void OnAnimationFootRight()
    {
       // Debug.Log("Character : OnAnimationFootRight : ");
        Vector3 pos = _rightFoot.position;
        pos.y = 0.3F;
        CtrlSound.Instance.PlayFootStep();
        CtrlFootTrace.Instance.ShowFootTraceRight(pos, new Vector3(90F, 0, 0));
    }

    private void SubscribeAnimatorEvents()
    {
        _animationEvent.ActionAnimationEndAttack += OnAnimationEndAttack;
        _animationEvent.ActionAnimationEndDie += OnAnimationEndDie;
        _animationEvent.ActionFootDownLeft += OnAnimationFootLeft;
        _animationEvent.ActionFootDownRight += OnAnimationFootRight;
    }

    private void UnscribeAnimatorEvents()
    {
        _animationEvent.ActionAnimationEndAttack -= OnAnimationEndAttack;
        _animationEvent.ActionAnimationEndDie -= OnAnimationEndDie;
        _animationEvent.ActionFootDownLeft -= OnAnimationFootLeft;
        _animationEvent.ActionFootDownRight -= OnAnimationFootRight;
    }


    #endregion




    void Init()
    {
        _characterController = transform.GetComponent<CharacterController>();
        _animatorController = transform.GetComponent<AnimatorController>();
        if (transform.CompareTag("Player")) _isPlayerChar = true;
        _currentHealth = _health;
        ShowHealthUi(false); 
        SetHealthUi(_currentHealth, _health);
        SubscribeAnimatorEvents();
    }

    public void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        UnscribeAnimatorEvents();
    }
}
