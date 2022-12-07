using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRayController : MonoBehaviour
{
    [SerializeField] private EyeTracking _eyeController;
    [SerializeField] private EyeTrackingCollector _eyeTrackingCollector;
    

    void Update()
    {
        var _eyeData = _eyeController.GetEyeTracking();
        RaycastHit hit;
        if (_eyeData.isGazeRayValid)
        {
            var pos = _eyeData.rayOrigin;
            var direction = _eyeData.rayDirection;
            
            if (Physics.Raycast(pos + direction * 100, -direction, out hit, 100f))
            {
                _eyeTrackingCollector.SetHitData(hit.point,_eyeData);
                
            }
        }
        else
        {
            _eyeTrackingCollector.SetInvalidData(_eyeData);
        }
    }
}