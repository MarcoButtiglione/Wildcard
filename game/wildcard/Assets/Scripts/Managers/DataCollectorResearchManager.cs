using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DataCollectorResearchManager : MonoBehaviour
{
    
    [SerializeField] private EyeTracking _eyeTracking;
    private GameObject _researchObj;
    private ResearchManager_1 _researchManager1;
    private int _currentState;

    private XRController _xrController;
    
    private List<EyeTrackingSampleResearch> _eyeTrackingSamples;
    private string filePath;
    public string sceneName;
    private int _isClicking = 0;
    private int _isClickingRight = 0;
    private int _isFocusing = 0;
    private bool levelFinished = false;
    private float initTime;

    
    // Start is called before the first frame update
    void Start()
    {
        _researchObj = GameObject.Find("ResearchObj");
        _researchManager1 = GameObject.Find("ResearchManager").GetComponent<ResearchManager_1>();
        
        
        filePath = Application.persistentDataPath + "/Research/Research_Session_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        initTime = _eyeTracking.GetEyeTracking().timestamp;
        _eyeTrackingSamples = new List<EyeTrackingSampleResearch>();
        
    }

    // Update is called once per frame
    public void SetInvalidData(EyeTrackingData eyeTrackingData)
    {
        //Bool to int convertion
        var isLeftEyeBlinking = 0;
        var isRightEyeBlinking = 0;
        if (eyeTrackingData.isLeftEyeBlinking)
        {
            isLeftEyeBlinking = 1;
        }
        if (eyeTrackingData.isRightEyeBlinking)
        {
            isRightEyeBlinking = 1;
        }
        //Focusing update
        UpdateFocusing();
        
        //Creation of a new sample
        var eyeData = new EyeTrackingSampleResearch()
        {
            timestamp = eyeTrackingData.timestamp-initTime,
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = 0f,
            hitPoint2Dy = 0f,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isClicking = _isClicking,
            isClickingRight = _isClickingRight
        };
        _eyeTrackingSamples.Add(eyeData);
    }
    public void SetHitData(Vector3 hitPoint, EyeTrackingData eyeTrackingData)
    {
        //Hit point calc
        var tan = hitPoint.x/hitPoint.z;
        float hitX;
        if (hitPoint.z > 0)
        {
            hitX = (float) Math.Atan(tan);
        }
        else
        {
            hitX = (float) (Math.Atan(tan)+Math.PI);
        }

        
        var hitY = hitPoint.y;
        
        //Bool to int convertion
        var isLeftEyeBlinking = 0;
        var isRightEyeBlinking = 0;
        if (eyeTrackingData.isLeftEyeBlinking)
        {
            isLeftEyeBlinking = 1;
        }
        if (eyeTrackingData.isRightEyeBlinking)
        {
            isRightEyeBlinking = 1;
        }
        //Update focusing
        UpdateFocusing();

        var eyeData = new EyeTrackingSampleResearch()
        {
            timestamp = eyeTrackingData.timestamp-initTime,
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isClicking = _isClicking,
            isClickingRight = _isClickingRight
        };
        _eyeTrackingSamples.Add(eyeData);
        
    }
    

    private void GetEyeDataRaycast()
    {
        var _eyeData = _eyeTracking.GetEyeTracking();
        RaycastHit hit;
        if (_eyeData.isGazeRayValid)
        {
            var pos = _eyeData.rayOrigin;
            var direction = _eyeData.rayDirection;
            LayerMask mask = LayerMask.GetMask("EyeTrackingSphere");
            if (Physics.Raycast(pos + direction * 100, -direction, out hit, 100f,mask))
            {
                SetHitData(hit.point,_eyeData);
            }
        }
        else
        {
            SetInvalidData(_eyeData);
        }
    }
    
    public void WriteCSV()
    {
        if (_eyeTrackingSamples.Count > 0)
        {
            TextWriter tw = new StreamWriter(filePath, false);
            tw.WriteLine("TimeStamp,IsGazeRayValid,HitPointX,HitPointY,IsLeftEyeBlinking,IsRightEyeBlinking,IsFocusing,IsClicking,IsClickingRight");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for (int i = 0; i < _eyeTrackingSamples.Count; i++)
            {
                tw.WriteLine(_eyeTrackingSamples[i].timestamp +
                             "," + _eyeTrackingSamples[i].isGazeRayValid +
                             "," + _eyeTrackingSamples[i].hitPoint2Dx +
                             "," + _eyeTrackingSamples[i].hitPoint2Dy +
                             "," + _eyeTrackingSamples[i].isLeftEyeBlinking +
                             "," + _eyeTrackingSamples[i].isRightEyeBlinking +
                             "," + _eyeTrackingSamples[i].isFocusing +
                             "," + _eyeTrackingSamples[i].isClicking +
                             "," + _eyeTrackingSamples[i].isClickingRight
                );
            }
            tw.Close();
        }
    }


    private void UpdateFocusing()
    {
        if (_researchObj.transform.GetChild(_currentState).gameObject.GetComponent<FocusController>().getFocused())
        {
            _isFocusing = 1;
        }
        else
        {
            _isFocusing = 0;
        }
    }
    public void isClicking()
    {
        _isClicking = 1;
    }
    public void isClickingRight()
    {
        _isClickingRight = 1;
    }

    public void FinishLevel()
    {
        levelFinished = true;
        var hitX = 0f;
        var hitY = 0f;
        var isLeftEyeBlinking = 0;
        var isRightEyeBlinking = 0;
        
        var _eyeData = _eyeTracking.GetEyeTracking();
        RaycastHit hit;
        if (_eyeData.isGazeRayValid)
        {
            var pos = _eyeData.rayOrigin;
            var direction = _eyeData.rayDirection;
            LayerMask mask = LayerMask.GetMask("EyeTrackingSphere");
            if (Physics.Raycast(pos + direction * 100, -direction, out hit, 100f,mask))
            {
                //Hit point calc
                var tan =  hit.point.x/hit.point.z;
                if (hit.point.z > 0)
                {
                    hitX = (float) Math.Atan(tan);
                }
                else
                {
                    hitX = (float) (Math.Atan(tan)+Math.PI);
                }
                
                

                hitY = hit.point.y;
        
                //Bool to int convertion
                
                if (_eyeData.isLeftEyeBlinking)
                {
                    isLeftEyeBlinking = 1;
                }
                if (_eyeData.isRightEyeBlinking)
                {
                    isRightEyeBlinking = 1;
                }
            }
        }
        else
        {
            //Bool to int convertion
            if (_eyeData.isLeftEyeBlinking)
            {
                isLeftEyeBlinking = 1;
            }
            if (_eyeData.isRightEyeBlinking)
            {
                isRightEyeBlinking = 1;
            }
        }

        _isFocusing = 1;
        _isClicking = 1;
        _isClickingRight = 1;
        var eyeData = new EyeTrackingSampleResearch()
        {
            timestamp = _eyeData.timestamp-initTime,
            isGazeRayValid = _eyeData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isClicking = _isClicking,
            isClickingRight = _isClickingRight
        };
        _eyeTrackingSamples.Add(eyeData);
        WriteCSV();
    }


    private void Update()
    {
        if (!levelFinished)
        {
            _currentState = _researchManager1.GetCurrentState();
            
            
            var inputDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevices(inputDevices);

            foreach (var device in inputDevices)
            {
                bool triggerValue;
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    Debug.Log("Trigger button is pressed.");
                }
            }
            
            
            GetEyeDataRaycast();
            _isClicking = 0;
            _isClickingRight = 0;
        }

    }
}

public struct EyeTrackingSampleResearch
{
    public float timestamp;
    public bool isGazeRayValid;
    public float hitPoint2Dx;
    public float hitPoint2Dy;
    public int isLeftEyeBlinking;
    public int isRightEyeBlinking;
    public int isFocusing;
    public int isClicking;
    public int isClickingRight;
}
