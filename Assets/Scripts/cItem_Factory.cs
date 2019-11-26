using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Factory {

    static cItem returnItem;
     
   //골드, 캐쉬, 공격력, 체력, 스피드돌진, 폭탄, 총알UP
   //몬스터 한테서는 캐쉬와 골드가 떨어진다.

    static public cItem MakeItem(string _itemName)
    {        
        switch (_itemName)
        {
            case "아이템_골드":
                returnItem = new cItem_Gold();
                returnItem.m_name = "아이템_골드";
                returnItem.m_goldPrice = 5;                                
                break;
            case "아이템_캐쉬":
                returnItem = new cItem_Cash();
                returnItem.m_name = "아이템_캐쉬";
                returnItem.m_cashPrice = 10;                
                break;
            case "아이템_공격력":
                returnItem = new cItem_Passive_Attack();
                returnItem.m_name = "아이템_공격력";
                returnItem.m_cashPrice = 50;                
                break;
            case "아이템_체력":
                returnItem = new cItem_Passive_HpUp();
                returnItem.m_name = "아이템_체력";
                returnItem.m_cashPrice = 50;                                
                break;
            case "아이템_스피드돌진":
                returnItem = new cItem_Active_Speed();
                returnItem.m_name = "아이템_스피드돌진";
                returnItem.m_goldPrice = 100;
                break;
            case "아이템_자석":
                returnItem = new cItem_Active_Magnet();
                returnItem.m_name = "아이템_자석";
                returnItem.m_goldPrice = 100;
                break;
            case "아이템_총알":
                returnItem = new cItem_Active_Bullet();
                returnItem.m_name = "아이템_총알";
                returnItem.m_goldPrice = 100;
                break;
        }

        if (returnItem != null)
        {
            cItem outItem = returnItem;
            returnItem = null;
            return outItem;
        }
        else
        {
            returnItem = null;
            return null;
        } 
    }

    static public string MakePath(string _itemName)
    {
        return "LoadedImage/" + _itemName;
    }

    static public string GetItemEffect(string _itemName)
    {
        string msg = "아이템 설명";
        switch (_itemName)
        {            
            case "아이템_공격력":
                msg = "- 효 과 -\n플레이어의 공격력을\n" + "+" + MakeItem(_itemName).ItemDataOut().ToString() + " 증가시킵니다.(영구)\n";
                break;
            case "아이템_체력":
                msg = "- 효 과 -\n플레이어의 체력을\n" + "+" + MakeItem(_itemName).ItemDataOut().ToString() + " 증가시킵니다.(영구)\n";
                break;
            case "아이템_스피드돌진":
                msg = "- 효 과 -\n" + MakeItem(_itemName).ItemDataOut().ToString() + "초 동안 빠른 스피드와\n주변 몬스터들에 피해를 줍니다.\n\n";
                break;
            case "아이템_자석":
                msg = "- 효 과 -\n" + MakeItem(_itemName).ItemDataOut().ToString() + "초 동안 화면의\n모든 아이템을 끌어당깁니다.\n\n";
                break;
            case "아이템_총알":
                msg = "- 효 과 -\n플레이어의 총알을\n +1 증가시킵니다.(최대3개)\n\n";                
                break;
        }
        return msg;
    }
    
}
