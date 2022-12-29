using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character
{
    private static readonly int PlayerSelected = Animator.StringToHash("PlayerSelected");
    private static readonly int PlayerDeSelected = Animator.StringToHash("PlayerDeSelected");

    public bool isSelected = false;


  


    

    private string GetRandomDanceAnim()
    {
        return "Dance";
    }
    
    
    public void OnPlayerDeselected()
    {
      //  Debug.Log("CharacterPlayer : OnPlayerDeselected");
      //  _animator.SetTrigger(GetRandomDanceAnim());
    }
    
    
 
    public void  SetPlayerSelected(bool isPlayerSelected)
    {
        Debug.Log("CharacterPlayer : OnPlayerSelected" + isPlayerSelected);
        //Set animation prepare Attack
      

        if (isSelected != isPlayerSelected)
        {
            //new state have
            if (isPlayerSelected)
            {
                Debug.Log("ANIMATOR Set trigger " + PlayerSelected);
                //selected anim
                _animator.SetTrigger(PlayerSelected);
            }
            else
            {
                Debug.Log("ANIMATOR Set trigger " + PlayerDeSelected);

                // deselected anim
               // _animator.SetTrigger(PlayerDeSelected);
               this.Attack();
            }
        }
        
        isSelected = isPlayerSelected;
    }
    
    void Start()
    {
        OnPlayerDeselected();
        base.Start();

        CtrlCharactersData.Instance.RegistratePlayer(gameObject);
    }

}
