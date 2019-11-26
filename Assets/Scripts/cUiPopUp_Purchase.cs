using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp_Purchase : cUiPopUp
{
    public string                   m_itemName = "cUiPopUp_Shop_Inventory -> Get Data!";
    public UILabel                  m_mainText;
    public cUiPopUp_Shop_Inventory  m_Inventory;
    GameObject                      m_NotifyPrefab = null;
    
    public void TextReset()
    {
        if (m_itemName.Equals("아이템_공격력"))
        {
            m_mainText.text = "[ " + m_itemName.Remove(0, 4) + " ]" + "\n\n[ff0000]" + cItem_Factory.GetItemEffect(m_itemName) + "( " + cDataManager.INSTANCE.PLAYER.m_Attack.ToString() + " → " + (cDataManager.INSTANCE.PLAYER.m_Attack + cItem_Factory.MakeItem(m_itemName).ItemDataOut()).ToString() + " )" + "[ffffff]\n\n구매 하시겠습니까?";
        }
        else if (m_itemName.Equals("아이템_체력"))
        {
            m_mainText.text = "[ " + m_itemName.Remove(0, 4) + " ]" + "\n\n[ff0000]" + cItem_Factory.GetItemEffect(m_itemName) + "( " + cDataManager.INSTANCE.PLAYER.m_hp.ToString() + " → " + (cDataManager.INSTANCE.PLAYER.m_hp + cItem_Factory.MakeItem(m_itemName).ItemDataOut()).ToString() + " )" + "[ffffff]\n\n구매 하시겠습니까?";
        }
        else
        {
            m_mainText.text = "[ " + m_itemName.Remove(0, 4) + " ]" + "\n\n[ff0000]" + cItem_Factory.GetItemEffect(m_itemName) + "[ffffff]구매 하시겠습니까?";
        }
    }

    public void Sell()
    {
        //여기서 돈 조건 따져야함 돈이 조건이 충분하다면
        //AddByShopToInventory실행!
        //부족하다면 돈이 부족합니다 팝업창 실행!
        cItem selectItem = cItem_Factory.MakeItem(m_itemName);

        if (m_itemName.Equals("아이템_공격력") || m_itemName.Equals("아이템_체력"))
        {
            //돈이 모자라다면
            if (cDataManager.INSTANCE.PLAYER.m_Cash < selectItem.m_cashPrice)
            {
                MakePrefabNotify("금액이 부족합니다.");
            }
            else
            {
                cDataManager.INSTANCE.PLAYER.m_Cash -= selectItem.m_cashPrice;

                if (m_itemName.Equals("아이템_공격력"))
                {
                    MakePrefabNotify("[ 구매완료 ]" + "\n\n" + m_itemName.Remove(0, 4) + " : " + cDataManager.INSTANCE.PLAYER.m_Attack.ToString() + " → " + (cDataManager.INSTANCE.PLAYER.m_Attack + cItem_Factory.MakeItem(m_itemName).ItemDataOut()).ToString() + "\n 증가 되었습니다.");
                }
                else if (m_itemName.Equals("아이템_체력"))
                {
                    MakePrefabNotify("[ 구매완료 ]" + "\n\n" + m_itemName.Remove(0, 4) + " : " + cDataManager.INSTANCE.PLAYER.m_hp.ToString() + " → " + (cDataManager.INSTANCE.PLAYER.m_hp + cItem_Factory.MakeItem(m_itemName).ItemDataOut()).ToString() + "\n 증가 되었습니다.");
                }
                

                cItem_Factory.MakeItem(m_itemName).Use();                
                cDataManager.INSTANCE.PLAYER.Notify();                
            }
        }
        else
        {
            //돈이 모자라다면
            if (cDataManager.INSTANCE.PLAYER.m_GameMoney < selectItem.m_goldPrice)
            {
                MakePrefabNotify("금액이 부족합니다.");
            }
            else
            {
                cDataManager.INSTANCE.PLAYER.m_GameMoney -= selectItem.m_goldPrice;                
                m_Inventory.AddByShopToInventory(m_itemName);
                cDataManager.INSTANCE.PLAYER.Notify();
                MakePrefabNotify("구매가 완료되었습니다.");
            }
        }                                                                        
        FadeOut();
    }

    void MakePrefabNotify(string _msg)
    {
        if (m_NotifyPrefab == null)
        {
            m_NotifyPrefab = cPrefabManager.INSTANCE.MakePrefab("Prefab_Popup_Notify", "UI Root");
            m_NotifyPrefab.GetComponent<cUiPopUp_Notify>().TextReset(_msg);
            m_NotifyPrefab.GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_NotifyPrefab.GetComponent<cUiPopUp_Notify>().TextReset(_msg);
            m_NotifyPrefab.GetComponent<cUiPopUp>().FadeIn();
        }
    }
}
