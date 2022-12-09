using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EyeTrackingCollector : MonoBehaviour
{
    private List<EyeTrackingCollectorData> _eyeTrackingCollectorData;
    string filePath;
    public string sceneName;
    private void Start()
    {
        filePath = Application.persistentDataPath + "/Story/Story_Session_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        _eyeTrackingCollectorData = new List<EyeTrackingCollectorData>();
    }

    public void SetInvalidData(EyeTrackingData eyeTrackingData)
    {
        var eyeData = new EyeTrackingCollectorData()
        {
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = 0f,
            hitPoint2Dy = 0f,
            timestamp = eyeTrackingData.timestamp,
            isLeftEyeBlinking = eyeTrackingData.isLeftEyeBlinking,
            isRightEyeBlinking = eyeTrackingData.isRightEyeBlinking
        };
        _eyeTrackingCollectorData.Add(eyeData);
    }

    public void SetHitData(Vector3 hitPoint, EyeTrackingData eyeTrackingData)
    {
        var tan = hitPoint.x / hitPoint.z;
        var hitX = (float) Math.Atan(tan);
        var hitY = hitPoint.y;
        var eyeData = new EyeTrackingCollectorData()
        {
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            timestamp = eyeTrackingData.timestamp,
            isLeftEyeBlinking = eyeTrackingData.isLeftEyeBlinking,
            isRightEyeBlinking = eyeTrackingData.isRightEyeBlinking
        };
        _eyeTrackingCollectorData.Add(eyeData);
    }
    public void WriteCSV()
    {
        if (_eyeTrackingCollectorData.Count > 0)
        {
            TextWriter tw = new StreamWriter(filePath, false);
            tw.WriteLine("TimeStamp,IsGazeRayValid,HitPointX,HitPointY,IsLeftEyeBlinking,IsRightEyeBlinking");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for (int i = 0; i < _eyeTrackingCollectorData.Count; i++)
            {
                tw.WriteLine(_eyeTrackingCollectorData[i].timestamp +
                             "," + _eyeTrackingCollectorData[i].isGazeRayValid +
                             "," + _eyeTrackingCollectorData[i].hitPoint2Dx +
                             "," + _eyeTrackingCollectorData[i].hitPoint2Dy +
                             "," + _eyeTrackingCollectorData[i].isLeftEyeBlinking +
                             "," + _eyeTrackingCollectorData[i].isRightEyeBlinking);
            }
            tw.Close();
        }
    }
}

public struct EyeTrackingCollectorData
{
    public bool isGazeRayValid;
    public float hitPoint2Dx;
    public float hitPoint2Dy;
    public float timestamp;
    public bool isLeftEyeBlinking;
    public bool isRightEyeBlinking;
}
