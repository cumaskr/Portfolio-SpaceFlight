using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class cDataInventory : cDataParsing
{    
    /*===========================데이터 목록==============================*/
    public List<cItem> m_inventoryList = new List<cItem>();
    /*==================================================================*/
    
    public cDataInventory()
    {
        if (LoadXML() == false)
        {
            Debug.LogError("인벤토리 데이터 가 로드되지 않았습니다.");
            Application.Quit();
        }
#if UNITY_EDITOR
        Debug.Log("인벤토리 데이터 로딩 완료. + 로딩된 아이템 갯수 : " + m_inventoryList.Count);
#endif
    }
    
    string[] nodeStr = new string[] { "/인벤토리/아이템", "이름"};
        
    public override bool LoadXML()
    {
        if (File.Exists(Application.persistentDataPath + "/xmlInventory.xml"))
        {
            string text = File.ReadAllText(Application.persistentDataPath + "/xmlInventory.xml");
            if (text == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                cItem tmpItem = cItem_Factory.MakeItem(childNode[nodeStr[1]].InnerText);                
                m_inventoryList.Add(tmpItem);                
            }            
        }
        else
        {
            TextAsset ta = Resources.Load("xmlInventory", typeof(TextAsset)) as TextAsset;
            if (ta == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(ta.text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                cItem tmpItem = cItem_Factory.MakeItem(childNode[nodeStr[1]].InnerText);                
                m_inventoryList.Add(tmpItem);                               
            }            
        }        
        return true;
    }

    public override bool SaveXML()
    {
        XmlDocument m_xmlDocument = new XmlDocument();
        m_xmlDocument.AppendChild(m_xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        XmlNode rootNode = m_xmlDocument.CreateNode(XmlNodeType.Element, "인벤토리", string.Empty);
        m_xmlDocument.AppendChild(rootNode);
        for (int i = 0; i < m_inventoryList.Count; i++)
        {
            XmlNode childNode = m_xmlDocument.CreateNode(XmlNodeType.Element, "아이템", string.Empty);
            rootNode.AppendChild(childNode);
            XmlElement item = m_xmlDocument.CreateElement("이름");
            item.InnerText = m_inventoryList[i].m_name.ToString();
            childNode.AppendChild(item);
        }
        File.WriteAllText(Application.persistentDataPath + "/xmlInventory.xml", m_xmlDocument.OuterXml, System.Text.Encoding.UTF8);
        return true;
    }
}
