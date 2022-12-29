using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;


    public Action OnEndAnimationAttack;


    public void AnimatorEventCallBackEndAttack()
    {
        OnEndAnimationAttack?.Invoke();
    }



    #region OLD
    /*
    private void OnStopMovement()
    {
        _animator.SetTrigger("Idle");
        isMovingNow = false;
    }


    public void MoveForward(float distance)
    {
        Vector3 startPos = transform.position;
        _moving = transform.forward;
        _wayPoint = startPos + _moving * distance;

        _animator.SetTrigger("RunForward");
        isMovingNow = true;
    }

    public void MoveBack(float distance)
    {
        _characterController.Move(transform.forward);

        Vector3 startPos = transform.position;
        _moving = transform.forward;
        _wayPoint = startPos + _moving * distance;

        _animator.SetTrigger("RunBack");
        isMovingNow = true;
    }


    public void MoveLeft(float distance)
    {
        _characterController.Move(-transform.right);
        isMovingNow = true;
    }

    public void MoveRight(float distance)
    {
        _characterController.Move(transform.right);
        isMovingNow = true;
    }

    private void ResetPosRot()
    {
        _animator.transform.localPosition = localPos;
        _animator.transform.localRotation = localRot;
    }

    private void SaveLocalPosRot()
    {
        localPos = _animator.transform.localPosition;
        localRot = _animator.transform.localRotation;
    }
    */

    #endregion


    string _animDamagePrefix = "Damaged_"; //from 1 started
    int curDamagedAnim = 0;

    string _animDiePrefix = "Die_"; //from 1 started
   

    public void OnDamaged()
    {
        curDamagedAnim++;
        _animator.SetTrigger(_animDamagePrefix + curDamagedAnim);
        if(curDamagedAnim > 4) { curDamagedAnim = 0; }
    }

    public void OnDied()
    {
        var dieAnim = UnityEngine.Random.Range(1, 5);
        _animator.SetTrigger(_animDiePrefix + dieAnim);
        StartCoroutine(KillComponent(4F));
    }

    public void SetHorisontal(float val)
    {
        _animator.SetFloat("Horisontal", val);
    }

    public void SetVertical(float val)
    {
        _animator.SetFloat("Vertical", val);
    }


    private CharacterController _characterController;


    private IEnumerator KillComponent(float time)
    {
        yield return new WaitForSeconds(time);
        var animCtrl = GetComponent<AnimatorController>().enabled = false;
    }


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponentInChildren<CharacterController>();
    }

}
