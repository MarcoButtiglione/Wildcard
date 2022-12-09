using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingCollector : MonoBehaviour
{
    private List<EyeTrackingCollectorData> _eyeTrackingCollectorData;

    private void Start()
    {
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
