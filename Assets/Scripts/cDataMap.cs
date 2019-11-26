using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class cDataMap : cDataParsing
{

    /*===========================데이터 목록==============================*/
    public List<cMapInfo> m_mapInfoList = new List<cMapInfo>();
    /*==================================================================*/
    
    public class cMapInfo
    {
        public bool m_isOn = false;
        public int m_score = 0;
    }

    public cDataMap()
    {
        if (LoadXML() == false)
        {
            Debug.LogError("맵 데이터 가 로드되지 않았습니다.");
            Application.Quit();
        }
#if UNITY_EDITOR
        Debug.Log("맵 데이터 로딩 완료. + 로딩된 맵 갯수 : " + m_mapInfoList.Count);
#endif
    }

    int m_mapInfoListIndex = 0;
    string[] nodeStr = new string[] { "/맵/맵정보", "온오프", "점수"};

    public override bool LoadXML()
    {       
        if (File.Exists(Application.persistentDataPath + "/xmlMap.xml"))
        {
            string text = File.ReadAllText(Application.persistentDataPath + "/xmlMap.xml");
            if (text == null) return false;            
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                cMapInfo tmpMap = new cMapInfo();
                tmpMap.m_isOn = Convert.ToBoolean(childNode[nodeStr[1]].InnerText);
                tmpMap.m_score = Convert.ToInt32(childNode[nodeStr[2]].InnerText);
                m_mapInfoList.Add(tmpMap);
            }
        }
        else
        {
            TextAsset ta = Resources.Load("xmlMap", typeof(TextAsset)) as TextAsset;
            if (ta == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(ta.text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                cMapInfo tmpMap = new cMapInfo();
                tmpMap.m_isOn = Convert.ToBoolean(childNode[nodeStr[1]].InnerText);
                tmpMap.m_score = Convert.ToInt32(childNode[nodeStr[2]].InnerText);
                m_mapInfoList.Add(tmpMap);
            }
        }
        return true;
    }

    public override bool SaveXML()
    {        
        if (File.Exists(Application.persistentDataPath + "/xmlMap.xml"))
        {
            string text = File.ReadAllText(Application.persistentDataPath + "/xmlMap.xml");
            if (text == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                childNode.SelectSingleNode(nodeStr[1]).InnerText = m_mapInfoList[m_mapInfoListIndex].m_isOn.ToString();
                childNode.SelectSingleNode(nodeStr[2]).InnerText = m_mapInfoList[m_mapInfoListIndex].m_score.ToString();
                m_mapInfoListIndex++;
            }
            m_mapInfoListIndex = 0;
            File.WriteAllText(Application.persistentDataPath + "/xmlMap.xml", m_xmlDocument.OuterXml, System.Text.Encoding.UTF8);            
        }
        else
        {
            TextAsset ta = Resources.Load("xmlMap", typeof(TextAsset)) as TextAsset;
            if (ta == null) return false;
            XmlDocument m_xmlDocument = new XmlDocument();
            m_xmlDocument.LoadXml(ta.text);
            XmlNodeList rootNode = m_xmlDocument.SelectNodes(nodeStr[0]);
            foreach (XmlNode childNode in rootNode)
            {
                childNode.SelectSingleNode(nodeStr[1]).InnerText = m_mapInfoList[m_mapInfoListIndex].m_isOn.ToString();
                childNode.SelectSingleNode(nodeStr[2]).InnerText = m_mapInfoList[m_mapInfoListIndex].m_score.ToString();
                m_mapInfoListIndex++;
            }
            m_mapInfoListIndex = 0;
            File.WriteAllText(Application.persistentDataPath + "/xmlMap.xml", m_xmlDocument.OuterXml, System.Text.Encoding.UTF8);
        }
        return true;
    }
}
