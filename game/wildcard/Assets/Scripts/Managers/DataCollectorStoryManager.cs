using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollectorStoryManager : MonoBehaviour
{
    
    [SerializeField] private EyeTracking _eyeTracking;
    
    private List<EyeTrackingSample> _eyeTrackingSamples;
    private FocusController _focusControllerCharacter;
    private string filePath;
    public string sceneName;
    private int _isPointing = 0;
    private int _isFocusing = 0;
    private bool levelFinished = false;
    private bool isWrittenCSV = false;

    
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/Story/Story_Session_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        _eyeTrackingSamples = new List<EyeTrackingSample>();
        _focusControllerCharacter = GameObject.Find("CharacterParent").transform.GetChild(0).gameObject
            .GetComponent<FocusController>();
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
        var eyeData = new EyeTrackingSample()
        {
            timestamp = eyeTrackingData.timestamp,
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = 0f,
            hitPoint2Dy = 0f,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isPointing = _isPointing
        };
        _eyeTrackingSamples.Add(eyeData);
    }
    public void SetHitData(Vector3 hitPoint, EyeTrackingData eyeTrackingData)
    {
        //Hit point calc
        var tan = hitPoint.x / hitPoint.z;
        var hitX = (float) Math.Atan(tan);
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

        var eyeData = new EyeTrackingSample()
        {
            timestamp = eyeTrackingData.timestamp,
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isPointing = _isPointing
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
            tw.WriteLine("TimeStamp,IsGazeRayValid,HitPointX,HitPointY,IsLeftEyeBlinking,IsRightEyeBlinking,IsFocusing,isPointing");
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
                             "," + _eyeTrackingSamples[i].isPointing
                );
            }
            tw.Close();
        }
    }


    private void UpdateFocusing()
    {
        if (_focusControllerCharacter.getFocused()) 
        { 
            _isFocusing = 1;
        }
        else 
        { 
            _isFocusing = 0;
        } 
    }
    public void IsPointing()
    {
        _isPointing = 1;
    }
    public void IsNotPointing()
    {
        _isPointing = 0;
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
                var tan = hit.point.x / hit.point.z;
                hitX = (float) Math.Atan(tan);
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
        _isPointing = 1;
        var eyeData = new EyeTrackingSample()
        {
            timestamp = _eyeData.timestamp,
            isGazeRayValid = _eyeData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isPointing = _isPointing
        };
        _eyeTrackingSamples.Add(eyeData);
        WriteCSV();
    }


    private void Update()
    {
        if (!levelFinished)
        {
            GetEyeDataRaycast();
        }

    }
}

public struct EyeTrackingSample
{
    public float timestamp;
    public bool isGazeRayValid;
    public float hitPoint2Dx;
    public float hitPoint2Dy;
    public int isLeftEyeBlinking;
    public int isRightEyeBlinking;
    public int isFocusing;
    public int isPointing;
}
