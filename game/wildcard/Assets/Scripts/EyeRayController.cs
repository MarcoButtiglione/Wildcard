using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRayController : MonoBehaviour
{
    [SerializeField] private EyeTracking _eyeController;
    [SerializeField] private DataCollectorStoryManager _dataCollectorStoryManager;
    
    //This updata add a sample in the list for the csv
    void Update()
    {
        /*
        var _eyeData = _eyeController.GetEyeTracking();
        RaycastHit hit;
        if (_eyeData.isGazeRayValid)
        {
            var pos = _eyeData.rayOrigin;
            var direction = _eyeData.rayDirection;
            LayerMask mask = LayerMask.GetMask("EyeTrackingSphere");
            if (Physics.Raycast(pos + direction * 100, -direction, out hit, 100f,mask))
            {
                _dataCollectorStoryManager.SetHitData(hit.point,_eyeData);
            }
        }
        else
        {
            _dataCollectorStoryManager.SetInvalidData(_eyeData);
        }
        */
    }
}