using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cZoomInOut : MonoBehaviour
{

    public Camera m_3dCamera;
    public Camera[] m_cameraList;

    Vector3[] m_firstPos = new Vector3[2];
    float[] m_firstSize = new float[2];

    void CameraChange()
    {
        if (m_3dCamera.enabled) m_3dCamera.enabled = false;
        else m_3dCamera.enabled = true;

        if (m_cameraList[0].gameObject.activeSelf) m_cameraList[0].gameObject.SetActive(false);
        else m_cameraList[0].gameObject.SetActive(true);
    }

    public void StartZoomInOut()
    {
        for (int i = 0; i < 2; i++)
        {
            m_firstPos[i] = m_cameraList[i].transform.localPosition;
            m_firstSize[i] = m_cameraList[i].orthographicSize;
        }

        CameraChange();

        StartCoroutine("ZoomIn");
    }

    IEnumerator ZoomIn()
    {
        int nTimer = 0;
        Time.timeScale = 0.1f;
        while (nTimer < 10)
        {
            m_cameraList[0].GetComponent<Camera>().orthographicSize -= 0.1f;
            m_cameraList[0].transform.localPosition += new Vector3(0, -0.3f, 0);
            m_cameraList[1].GetComponent<Camera>().orthographicSize -= 0.01f;
            yield return new WaitForSeconds(0.001f);
            nTimer++;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("ZoomOut");
        yield break;
    }

    IEnumerator ZoomOut()
    {
        int nTimer = 0;
        while (nTimer < 10)
        {
            m_cameraList[0].GetComponent<Camera>().orthographicSize += 0.1f;
            m_cameraList[0].transform.localPosition += new Vector3(0, 0.3f, 0);
            m_cameraList[1].GetComponent<Camera>().orthographicSize += 0.01f;
            yield return new WaitForSeconds(0.001f);
            nTimer++;
        }
        for (int i = 0; i < 2; i++)
        {
            m_cameraList[i].transform.localPosition = m_firstPos[i];
            m_cameraList[i].GetComponent<Camera>().orthographicSize = m_firstSize[i];
        }
        Time.timeScale = 1.0f;
        CameraChange();
        yield break;
    }
}
