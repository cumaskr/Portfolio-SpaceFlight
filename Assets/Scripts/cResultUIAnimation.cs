using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cResultUIAnimation : MonoBehaviour {

    public cGameManager m_gameManager;
    public UILabel m_time;
    public UILabel m_coin;
    public UILabel m_star;
    public UILabel m_score;
    public UILabel m_finalResult;    
    public UILabel m_explication;

    UILabel[] m_labelList = new UILabel[6];

    private void OnEnable()
    {
        m_time.text = " →진행시간 : " + cStopWatch.INSTANCE.m_timer.ToString("N1");
        m_coin.text = " →획득코인 : " + (cDataManager.INSTANCE.PLAYER.m_GameMoney - m_gameManager.m_FirstMoneyInGame).ToString();
        m_star.text = " →획득스타 : " + (cDataManager.INSTANCE.PLAYER.m_Cash - m_gameManager.m_FirstSCashInGame).ToString();
                
        if (m_gameManager.m_isClear)
        {
            string _msg;
            switch (m_gameManager.ResultScroeMapCalculate())
            {
                case 1:
                    _msg = "★☆☆";
                    break;
                case 2:
                    _msg = "★★☆";
                    break;
                case 3:
                    _msg = "★★★";
                    break;
                default :
                    _msg = "☆☆☆";
                    break;
            }
            m_score.text = " →최종점수 : " + _msg;
            m_finalResult.text = " →최종결과 : " + "미션 성공!";
        }
        else
        {
            m_score.text = " →최종점수 : " + "☆☆☆";
            m_finalResult.text = " →최종결과 : " + "미션 실패!";
        }


        m_explication.text = "최종점수는 체력(70%↑/50%↑/50%↓)으로 계산됩니다.\n클리어 실패시, 코인/스타는 획득되지 않습니다.";

        //여기서 셋팅
        m_labelList[0] = m_time;
        m_labelList[1] = m_coin;
        m_labelList[2] = m_star;
        m_labelList[3] = m_score;
        m_labelList[4] = m_finalResult;
        m_labelList[5] = m_explication;

        StartCoroutine("CoResultAnimation");
    }

    IEnumerator CoResultAnimation()
    {
        int nIndex = 0;
        while (nIndex < m_labelList.Length)
        {
            yield return new WaitForSeconds(0.5f);
            m_labelList[nIndex].gameObject.SetActive(true);
            nIndex++;
        }
        yield break;
    }
}
