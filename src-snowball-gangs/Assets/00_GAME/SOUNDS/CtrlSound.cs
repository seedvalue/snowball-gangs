using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSound : MonoBehaviour
{
    public static CtrlSound Instance;

    [SerializeField] private AudioSource snowFace;
    [SerializeField] private AudioSource enemyDie;
    [SerializeField] private AudioSource snowBallGrounded;

    [SerializeField] private AudioSource woosh;

    

    [Space]
    [SerializeField] private AudioSource Damaged_1;
    [SerializeField] private AudioSource Damaged_2;
    [SerializeField] private AudioSource Damaged_3;

    [Space]
    [SerializeField] private List<AudioSource> SnowFoots;

    [SerializeField] private AudioSource _mainMenuMusic;
    [SerializeField] private AudioSource _gamePlayMusic;
    [SerializeField] private AudioSource _skinShopMusic;

    #region UI

    [Header("Ui")]
    [SerializeField] private AudioSource ButtonClick;
    [SerializeField] private AudioSource SkinApply;
    [SerializeField] private AudioSource SkinBuy;

    public void PlayButtonClick()
    {
       if(ButtonClick) ButtonClick.Play();
    }

    public void PlaySkinApply()
    {
        if (SkinApply) SkinApply.Play();
    }

    public void PlaySkinBuy()
    {
        if (SkinBuy) SkinBuy.Play();
    }


    #endregion


    #region CANDY GIFT
    [Header("Die Gift")]
    //CANDY
    [SerializeField] private AudioSource _candySpawn;
    [SerializeField] private AudioSource _candyGet;
    [SerializeField] private AudioSource _candyTouch;
    [SerializeField] private AudioSource _candyTouchEnemy;
    [SerializeField] private AudioSource _candyLooseUi;


    //GIFT
    [SerializeField] private AudioSource _giftSpawn;
    [SerializeField] private AudioSource _giftOpen;

    [SerializeField] private AudioSource _levelFinishWin;
    [SerializeField] private AudioSource _levelFinishLoose;

    public void PlayGiftSpawn()
    {
        if (!_giftSpawn) return;
        _giftSpawn.pitch += 0.1F;
        if (_giftSpawn.pitch >= 3F) _giftSpawn.pitch = 1F;
        _giftSpawn.Play();
    }

    public void PlayGiftOpen()
    {
        if (!_giftOpen) return;
        _giftOpen.pitch += 0.1F;
        if (_giftOpen.pitch >= 3F) _giftOpen.pitch = 1F;
        _giftOpen.Play();
    }

    public void PlayCandySpawn()
    {
        if (!_candySpawn) return;
        _candySpawn.pitch += 0.1F;
        if (_candySpawn.pitch >= 3F) _candySpawn.pitch = 1F;
        _candySpawn.Play();
    }

    public void PlayCandyTouch()
    {
        if (!_candyTouch) return;
        _candyTouch.pitch += 0.1F;
        if (_candyTouch.pitch >= 3F) _candyTouch.pitch = 1F;
        _candyTouch.Play();
    }

    public void PlayCandyTouchEnemy()
    {
        if (!_candyTouch) return;
        _candyTouch.pitch += 0.1F;
        if (_candyTouch.pitch >= 3F) _candyTouch.pitch = 1F;
        _candyTouch.Play();
    }

    public void PlayCandyGet()
    {
        if (!_candyGet) return;
        _candyGet.pitch += 0.1F;
        if (_candyGet.pitch >= 3F) _candyGet.pitch = 1F;
        _candyGet.Play();
    }

    public void PlayCandyLooseUi()
    {
        if (!_candyLooseUi) return;
        _candyLooseUi.pitch -= 0.1F;
        if (_candyLooseUi.pitch < 0.6) _candyLooseUi.pitch = 1.5F;
        _candyLooseUi.Play();
    }



    public void PlayCandyEnemyTouch()
    {
        if (!_candyTouchEnemy) return;
        _candyTouchEnemy.pitch -= 0.1F;
        if (_candyTouchEnemy.pitch <= 0.7F) _candyTouchEnemy.pitch = 1.5F;
        _candyTouchEnemy.Play();
    }


    public void PlayDamagedRandom()
    {
        int randSnd = UnityEngine.Random.Range(1, 4);
        switch(randSnd)
        {
            case 1:
                {
                    PlayDamaged_1();
                } break;
            case 2:
                {
                    PlayDamaged_2();
                }
                break;
            case 3:
                {
                    PlayDamaged_3();
                }
                break;

        }
    }

    #endregion



    #region DAMAGE

    public void PlayDamaged_1()
    {
        Damaged_1.pitch = UnityEngine.Random.Range(2, 3);
        if (Damaged_1) Damaged_1.Play();
    }

    public void PlayDamaged_2()
    {
        Damaged_2.pitch = UnityEngine.Random.Range(2, 3);
        if (Damaged_2) Damaged_2.Play();
    }

    public void PlayDamaged_3()
    {
        Damaged_3.pitch = UnityEngine.Random.Range(2, 3);
        if (Damaged_3) Damaged_3.Play();
    }

    public void PlayEnemyDie()
    {
        enemyDie.pitch = 2F;
        if (enemyDie) enemyDie.Play();
    }

    #endregion


    public void PlayLevelFinish(bool isWin)
    {
        if(isWin) _levelFinishWin.Play();
        else _levelFinishLoose.Play();
    }

    public void PlayMainMenu()
    {
        _gamePlayMusic.Stop();
        _skinShopMusic.Stop();
        if (!_mainMenuMusic) return;
        _mainMenuMusic.Play();
        _mainMenuMusic.loop = true;
        _gamePlayMusic.playOnAwake = false; 
    }

    public void PlayGamePlay()
    {
        _mainMenuMusic.Stop();
        _skinShopMusic.Stop();
        if (!_gamePlayMusic) return;
        _gamePlayMusic.Play();
        _gamePlayMusic.loop = true;
        _gamePlayMusic.playOnAwake = false;
    }

    public void PlaySkinShop()
    {
        _mainMenuMusic.Stop();
        _gamePlayMusic.Stop();
        _skinShopMusic.Play();
        _skinShopMusic.loop = true;
    }

    public void PlayFootStep()
    {
      bool isSomePlayed = false;
      for(int i = 0; i < SnowFoots.Count ; i++)
        {
            if (SnowFoots[i].isPlaying == false)
            {
                SnowFoots[i].Play();
                isSomePlayed = true;
                return;
            }
            else continue;
        }
      if(isSomePlayed) SnowFoots[0].Play();
    }
    
    public void PlaySnowFace()
    {
        if(snowFace)  snowFace.Play();
    }

   

    public void PlaySnowballGrounded()
    {
        if(snowBallGrounded) snowBallGrounded.Play();
    }
    public void PlayWoosh()
    {
        if(woosh) woosh.Play();
    }
    

    private void Awake()
    {
        Instance = this;
    }
}
