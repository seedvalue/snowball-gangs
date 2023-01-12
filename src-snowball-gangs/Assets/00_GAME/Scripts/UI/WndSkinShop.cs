using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WndSkinShop : MonoBehaviour
{
    public Button BtnMainMenu;

    [SerializeField] private GameObject PivotPopupApplyTo = null;

   
    [SerializeField] private ShopData _shopData;
    [SerializeField] int _currentMoney = 100;

    [SerializeField] private Transform _parentForItems;
    [SerializeField] private Transform _prefabItem;

    [SerializeField] TMP_Text _textMoney = null;
    private List<ItemShopSkinUi> _itemShopSkinUis = new List<ItemShopSkinUi>();


    [Header("Apply to:")]
    public Button BtnApplyToHuman = null;
    public Button BtnApplyToCpu = null;

    private void ShowPopupApplyTo(bool isShow)
    {
       if(PivotPopupApplyTo) PivotPopupApplyTo.SetActive(isShow);
    }



    ItemSkin _selectedItemSkin = null;
    public void OnApplyItem(ItemSkin itemSkin)
    {
        Debug.Log("WndSkinShop : OnApplyItem : " + itemSkin.Name);
        _selectedItemSkin = itemSkin;
        ShowPopupApplyTo(true);
    }

    public void OnBuyItem(ItemSkin itemSkin)
    {
        Debug.Log("WndSkinShop : OnBuyItem : " + itemSkin.Name);
        CtrlGame.Instance.BuySkinItem(itemSkin.Name, itemSkin.Price);
        RefreshMoney();
        RefreshAllItemsCanBuy();
    }

    private void RefreshAllItemsCanBuy()
    {
        foreach(var one in _itemShopSkinUis)
        {
            one.RefreshCanBuy(_currentMoney);
        }
    }




    private void RefreshMoney()
    {
        _currentMoney = GetMoneyPrefs();
        _textMoney.text =_currentMoney.ToString();
    }

    private int GetMoneyPrefs()
    {
       if(PlayerPrefs.HasKey("CANDY"))
        {
            return PlayerPrefs.GetInt("CANDY");
        }
        return 0;
    }

    private void SetMoneyPrefs(int val)
    {
        PlayerPrefs.SetInt("CANDY", val);
    }

    private void RenderShop()
    {
        RefreshMoney();
        CleanUpItems();
        foreach(var one in _shopData.itemSkins)
        {
            CreateItem(one);
        }
    }



    private void CreateItem(ItemSkin itemSkin)
    {
        var obj = Instantiate(_prefabItem, _parentForItems);
        if(obj.TryGetComponent<ItemShopSkinUi>(out var comp))
        {
            bool isItemBuyed = CtrlGame.Instance.IsSkinBuyed(itemSkin.Name);
            comp.SetupItem(itemSkin, _currentMoney, isItemBuyed);
            comp.SetupActions(OnApplyItem, OnBuyItem);
            _itemShopSkinUis.Add(comp);
        }
    }

    private void CleanUpItems()
    {
        foreach(Transform item in _parentForItems)
        {
            GameObject.Destroy(item.gameObject);
        }
        _itemShopSkinUis.Clear();
    }


    public void OnclickedMainMenu()
    {
        Debug.Log("WndSkinShop : OnclickedMainMenu");
        CtrlUi.Instance.ShowMainMenu();
    }

    public void OnclickedApplyToHuman()
    {
        Debug.Log("WndSkinShop : OnclickedApplyToHuman : " + _selectedItemSkin.Name);
        CtrlGame.Instance.SetSkinHuman(_selectedItemSkin.Name );
        ShowPopupApplyTo(false);
    }

    public void OnclickedApplyToCpu()
    {
        Debug.Log("WndSkinShop : OnclickedApplyToCpu : " + _selectedItemSkin.Name);
        CtrlGame.Instance.SetSkinCpu(_selectedItemSkin.Name);
        ShowPopupApplyTo(false);
    }


    private void Awake()
    {
        BtnMainMenu.onClick.AddListener(OnclickedMainMenu);
        BtnApplyToHuman.onClick.AddListener(OnclickedApplyToHuman);
        BtnApplyToCpu.onClick.AddListener(OnclickedApplyToCpu);
        ShowPopupApplyTo(false);
    }

    private void OnEnable()
    {
        RenderShop();
        ShowPopupApplyTo(false);
    }
}


public class ItemShopSkin 
{
    public bool isFree;
    public bool isBuyed; 
    public int Price;
    public string Name;
    public string Description;
    public Texture2D Icon;
}