using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopSkinUi : MonoBehaviour
{
    public Button ApplySkin = null;
    public Button BuySkin = null;
    [Space]

    [SerializeField] RawImage _skinIcon = null;
    [SerializeField] TMP_Text _name = null;
    [SerializeField] TMP_Text _price = null;

    [SerializeField] GameObject _pivotHaveMoney = null;
    [SerializeField] GameObject _pivotNeedBuy = null;
    [SerializeField] GameObject _pivotIsMine = null;

    [Header("Debug:")]
    [SerializeField] ItemSkin _currentItemSkin = null;
    
    public Action<ItemSkin> OnApply = null;
    public Action<ItemSkin> OnBuy = null;


    public void SetupItem(ItemSkin itemSkin, int currentMoney, bool isMine)
    {
        Debug.Log("ItemShopSkinUi : SetupItem : itemSkin = " + itemSkin.Name + " buyed = " + isMine);
        if (itemSkin == null) 
        {
            Debug.LogError("ItemShopSkinUi : SetupItem : itemSkin is NULL");
        } else
        {
            _currentItemSkin = itemSkin;
            _skinIcon.texture = itemSkin.Icon;
            _name.text = itemSkin.Name;
            _price.text = itemSkin.Price.ToString();

            RefreshCanBuy(currentMoney);

            if (isMine)
            {
                _pivotIsMine.SetActive(true);
                _pivotNeedBuy.SetActive(false);
            } else
            {
                _pivotIsMine.SetActive(false);
                _pivotNeedBuy.SetActive(true);
            }

            if (itemSkin.isFree)
            {
               _pivotIsMine.SetActive(true);
               _pivotNeedBuy.SetActive(false);
            }
        }
    }

    public void SetupActions(Action<ItemSkin> onApply, Action<ItemSkin> onPressedBuy)
    {
        OnApply = onApply;
        OnBuy = onPressedBuy;
    }

    public void RefreshCanBuy(int currentMoney)
    {
        if (currentMoney >= _currentItemSkin.Price)
        {
            _pivotHaveMoney.SetActive(true);
        }
        else _pivotHaveMoney.SetActive(false);
    }

    public void OnClickApply()
    {
        Debug.Log("ItemShopSkinUi : OnClickApply : ");
        // CtrlSound.Instance.PlaySkinApply();
        CtrlSound.Instance.PlayButtonClick();
        OnApply?.Invoke(_currentItemSkin);
    }

    public void OnClickBuy()
    {
        Debug.Log("ItemShopSkinUi : OnClickApply : ");
        CtrlSound.Instance.PlayButtonClick();
        CtrlSound.Instance.PlaySkinBuy();
        OnBuy?.Invoke(_currentItemSkin);
        // money -=
        // refresh item
        _pivotIsMine.SetActive(true);
        _pivotNeedBuy.SetActive(false);
    }


    private void Awake()
    {
        ApplySkin.onClick.AddListener(OnClickApply);
        BuySkin.onClick.AddListener(OnClickBuy);
    }
}
