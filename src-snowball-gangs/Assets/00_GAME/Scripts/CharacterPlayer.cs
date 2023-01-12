using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character
{
    private static readonly int PlayerSelected = Animator.StringToHash("PlayerSelected");
    private static readonly int PlayerDeSelected = Animator.StringToHash("PlayerDeSelected");

    public bool isSelected = false;




    private bool isEditor = false;
    private void CheckEditor()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.LinuxEditor)
        {
            isEditor = true;
        }
    }


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
      //TODO ќѕ“»ћ»«ј÷»я ќно в консоль плевало как в апдейте, сделать так .что бы не лупило даже с дебагом
        //  Debug.Log("CharacterPlayer : OnPlayerSelected" + isPlayerSelected);
        //Set animation prepare Attack
      

        if (isSelected != isPlayerSelected)
        {
            //new state have
            if (isPlayerSelected)
            {
                if (!isEditor) Debug.Log("ANIMATOR Set trigger " + PlayerSelected);
                //selected anim
                _animator.SetTrigger(PlayerSelected);
            }
            else
            {
                if (!isEditor) Debug.Log("ANIMATOR Set trigger " + PlayerDeSelected);

                // deselected anim
               // _animator.SetTrigger(PlayerDeSelected);
               this.Attack();
            }
        }
        
        isSelected = isPlayerSelected;
    }



    private void Awake()
    {
        //CheckEditor();
        isEditor = true;
    }

    void Start()
    {
        OnPlayerDeselected();
        base.Start();

        CtrlCharactersData.Instance.RegistratePlayer(gameObject);
    }

}
