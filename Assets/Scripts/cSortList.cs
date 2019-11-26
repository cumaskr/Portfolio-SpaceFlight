using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSortList<T> : cSort {
    
    List<T> m_arr;
    public delegate bool compareDelegate(T _a, T _b);
    
    compareDelegate m_comDel;
    
    public cSortList(List<T> _arr, compareDelegate _comDel)
    {
        m_arr = _arr;
        m_comDel = _comDel;        
    }








    public override void Ascending()
    {



        











        for (int i = 0; i < m_arr.Count; i++)
        {
            //마지막이라면            
            if (i == m_arr.Count - 1)
            {
                for (int p = i; p <= 0; p--)
                {
                    if (m_comDel(m_arr[i], m_arr[p]))
                    {
                        T tmpUser;
                        tmpUser = m_arr[i];
                        m_arr[i] = m_arr[p];
                        m_arr[p] = tmpUser;
                    }
                }
            }
            //마지막 이전데이터까지
            else
            {
                for (int p = i + 1; p < m_arr.Count; p++)
                {
                    if (m_comDel(m_arr[i], m_arr[p]))
                    {
                        T tmpUser;
                        tmpUser = m_arr[i];
                        m_arr[i] = m_arr[p];
                        m_arr[p] = tmpUser;
                    }
                }
            }
        }
    }

    public override void Descending()
    {
        for (int i = 0; i < m_arr.Count; i++)
        {
            //마지막이라면            
            if (i == m_arr.Count - 1)
            {
                for (int p = i; p <= 0; p--)
                {
                    if (!m_comDel(m_arr[i], m_arr[p]))
                    {
                        T tmpUser;
                        tmpUser = m_arr[i];
                        m_arr[i] = m_arr[p];
                        m_arr[p] = tmpUser;
                    }
                }
            }
            else
            {
                for (int p = i + 1; p < m_arr.Count; p++)
                {
                    if (!m_comDel(m_arr[i], m_arr[p]))
                    {
                        T tmpUser;
                        tmpUser = m_arr[i];
                        m_arr[i] = m_arr[p];
                        m_arr[p] = tmpUser;
                    }
                }
            }
        }
    }
}
