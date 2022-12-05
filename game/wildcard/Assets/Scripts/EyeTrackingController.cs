using System.Collections;
using System.Collections.Generic;
using Tobii.XR;
using UnityEngine;

public class EyeTrackingController : MonoBehaviour
{
    public void GetEyeTracking()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        
        if(eyeTrackingData.GazeRay.IsValid)
        {
            // The origin of the gaze ray is a 3D point
            var rayOrigin = eyeTrackingData.GazeRay.Origin;

            // The direction of the gaze ray is a normalized direction vector
            var rayDirection = eyeTrackingData.GazeRay.Direction;
            
        }
        var timestamp = eyeTrackingData.Timestamp;
            
        var isLeftEyeBlinking = eyeTrackingData.IsLeftEyeBlinking;
        var isRightEyeBlinking = eyeTrackingData.IsRightEyeBlinking;
    }
}
