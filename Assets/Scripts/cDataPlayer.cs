using UnityEngine;
using System.Xml;
using System.IO;

public class cDataPlayer : cDataParsing {

    const int MAXITEMCOUNT = 3;
    /*===========================데이터 목록==============================*/
    public float m_GameMoney = -1;
    public float m_Cash = -1;
    public float m_Attack = -1;
    public float m_hp = -1;
    public cItem[] m_itemList = new cItem[MAXITEMCOUNT];
    /*==================================================================*/

    public delegate void DeligateChangeMoney();
    public DeligateChangeMoney m_event;
    public void Notify()
    {        
        m_event();
    }

    public float m_deltaToch = 0.5f;

    public cDataPlayer()
    {
        if (LoadXML() == false)
        {
            Debug.LogError("플레이어 데이터 가 로드되지 않았습니다.");
            Application.Quit();
        }

#if UNITY_EDITOR
        Debug.Log("플레이어 데이터 로딩 완료.");        
#endif

    }

    string[] nodeStr = new string[] { "플레이어", "게임돈", "캐쉬", "체력", "공격력" , "/플레이어/아이템리스트" ,"아이템"};
    int m_itemListindex=0;

    public override bool LoadXML()
    {                          
        if (File.Exists(Application.persistentDataPath + "/xmlPlayer.xml"))
        {
            string text = File.ReadAllText(Application.persistentDataPath + "/xmlPlayer.xml");
            if (text == null) return false;

            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                m_GameMoney = System.Convert.ToSingle(childNode[nodeStr[1]].InnerText);
                m_Cash = System.Convert.ToSingle(childNode[nodeStr[2]].InnerText);
                m_hp = System.Convert.ToSingle(childNode[nodeStr[3]].InnerText);
                m_Attack = System.Convert.ToSingle(childNode[nodeStr[4]].InnerText);
                XmlNodeList itemListNode = m_xmlDocument.SelectNodes(nodeStr[5]);
                foreach (XmlNode item in itemListNode)
                {
                    m_itemList[m_itemListindex] = cItem_Factory.MakeItem(item.InnerText);
                    m_itemListindex++;
                }
                m_itemListindex = 0;
            }
        }
        else
        {
            TextAsset ta = Resources.Load("xmlPlayer", typeof(TextAsset)) as TextAsset;
            if (ta == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(ta.text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                m_GameMoney = System.Convert.ToSingle(childNode[nodeStr[1]].InnerText);
                m_Cash = System.Convert.ToSingle(childNode[nodeStr[2]].InnerText);
                m_hp = System.Convert.ToSingle(childNode[nodeStr[3]].InnerText);
                m_Attack = System.Convert.ToSingle(childNode[nodeStr[4]].InnerText);
                XmlNodeList itemListNode = m_xmlDocument.SelectNodes(nodeStr[5]);
                foreach (XmlNode item in itemListNode)
                {
                    m_itemList[m_itemListindex] = cItem_Factory.MakeItem(item.InnerText);
                    m_itemListindex++;
                }
                m_itemListindex = 0;
            }
        }                               
        return true;
    }

    public override bool SaveXML()
    {
        if (File.Exists(Application.persistentDataPath + "/xmlPlayer.xml"))
        {
            string text = File.ReadAllText(Application.persistentDataPath + "/xmlPlayer.xml");
            if (text == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                childNode.SelectSingleNode(nodeStr[1]).InnerText = m_GameMoney.ToString();
                childNode.SelectSingleNode(nodeStr[2]).InnerText = m_Cash.ToString();
                childNode.SelectSingleNode(nodeStr[3]).InnerText = m_hp.ToString();
                childNode.SelectSingleNode(nodeStr[4]).InnerText = m_Attack.ToString();
                XmlNodeList itemListNode = m_xmlDocument.SelectNodes(nodeStr[5]);
                foreach (XmlNode item in itemListNode)
                {
                    if (m_itemList[m_itemListindex] != null)
                    {
                        item.SelectSingleNode(nodeStr[6]).InnerText = m_itemList[m_itemListindex].m_name;
                    }
                    else
                    {
                        item.SelectSingleNode(nodeStr[6]).InnerText = null;
                    }

                    m_itemListindex++;
                }
            }
            m_itemListindex = 0;
            File.WriteAllText(Application.persistentDataPath + "/xmlPlayer.xml", m_xmlDocument.OuterXml, System.Text.Encoding.UTF8);            
        }
        else
        {
            TextAsset ta = Resources.Load("xmlPlayer", typeof(TextAsset)) as TextAsset;
            if (ta == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(ta.text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                childNode.SelectSingleNode(nodeStr[1]).InnerText = m_GameMoney.ToString();
                childNode.SelectSingleNode(nodeStr[2]).InnerText = m_Cash.ToString();
                childNode.SelectSingleNode(nodeStr[3]).InnerText = m_hp.ToString();
                childNode.SelectSingleNode(nodeStr[4]).InnerText = m_Attack.ToString();
                XmlNodeList itemListNode = m_xmlDocument.SelectNodes(nodeStr[5]);
                foreach (XmlNode item in itemListNode)
                {
                    if (m_itemList[m_itemListindex] != null)
                    {
                        item.SelectSingleNode(nodeStr[6]).InnerText = m_itemList[m_itemListindex].m_name;
                    }
                    else
                    {
                        item.SelectSingleNode(nodeStr[6]).InnerText = null;
                    }
                    
                    m_itemListindex++;
                }
            }
            m_itemListindex = 0;
            File.WriteAllText(Application.persistentDataPath + "/xmlPlayer.xml", m_xmlDocument.OuterXml, System.Text.Encoding.UTF8);            
        }
        return true;
    }

    public int ReturnItemListLength()
    {
        int _outData = 0;
        for (int i = 0; i < m_itemList.Length; i++)
        {
            if (m_itemList[i] != null) _outData++;
        }
        return _outData;
    }
}
