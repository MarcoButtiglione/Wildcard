using System.Collections;
using System.Collections.Generic;
using Tobii.XR;
using UnityEngine;

public class EyeTrackingController : MonoBehaviour
{
    public EyeTrackingData GetEyeTracking()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        EyeTrackingData o = new EyeTrackingData
        {
            isGazeRayValid = eyeTrackingData.GazeRay.IsValid,
            rayOrigin = Vector3.zero,
            rayDirection = Vector3.zero,
            timestamp = eyeTrackingData.Timestamp,
            isLeftEyeBlinking = eyeTrackingData.IsLeftEyeBlinking,
            isRightEyeBlinking = eyeTrackingData.IsRightEyeBlinking,
        };

        if(eyeTrackingData.GazeRay.IsValid)
        {
            // The origin of the gaze ray is a 3D point
            o.rayOrigin = eyeTrackingData.GazeRay.Origin;

            // The direction of the gaze ray is a normalized direction vector
            o.rayDirection = eyeTrackingData.GazeRay.Direction;
        }
        return o;
    }
}

public struct EyeTrackingData
{
    public bool isGazeRayValid;
    public Vector3 rayOrigin;
    public Vector3 rayDirection;
    public float timestamp;
    public bool isLeftEyeBlinking;
    public bool isRightEyeBlinking;
}
