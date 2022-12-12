using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollectorExplorationManager : MonoBehaviour
{
    
    [SerializeField] private EyeTracking _eyeTracking;
    private ExplorationManager_1 _explorationManager;
    
    private List<EyeTrackingSampleExploration> _eyeTrackingSamples;
    private GameObject pictures;
    private GameObject _camera;
    private string filePath;
    public string sceneName;
    private int _isPointing = 0;
    private int _isFocusing = 0;
    private bool levelFinished = false;
    private float initTime;

    
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/Exploration/Exploration_Session_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        initTime = _eyeTracking.GetEyeTracking().timestamp;
        _eyeTrackingSamples = new List<EyeTrackingSampleExploration>();
        pictures = GameObject.Find("Picture");
        _camera = GameObject.Find("Main Camera");
        _explorationManager = GameObject.Find("ExplorationManager_1").GetComponent<ExplorationManager_1>();
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
        //UpdateFocusing();
        _isFocusing = 0;
        
        //Creation of a new sample
        var eyeData = new EyeTrackingSampleExploration()
        {
            timestamp = eyeTrackingData.timestamp-initTime,
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = 0f,
            hitPoint2Dy = 0f,
            cameraPos2Dx = _camera.transform.position.x,
            cameraPos2Dy = _camera.transform.position.z,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isPointing = _isPointing,
            isLookingSky = 0,
        };
        _eyeTrackingSamples.Add(eyeData);
    }
    public void SetHitData(Vector3 hitPoint, EyeTrackingData eyeTrackingData,int isLookingSky, bool isLookingPicture)
    {
        var hitX = 0f;
        var hitY = 0f;
        if(isLookingSky==0)
        {
            hitX = hitPoint.x; 
            hitY = hitPoint.z;
        } 
        
        
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
        //UpdateFocusing();
        _isFocusing = 0;
        if (isLookingPicture)
        {
            _isFocusing = 1;
        }

        var eyeData = new EyeTrackingSampleExploration()
        {
            timestamp = eyeTrackingData.timestamp-initTime,
            isGazeRayValid = eyeTrackingData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            cameraPos2Dx = _camera.transform.position.x,
            cameraPos2Dy = _camera.transform.position.z,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isPointing = _isPointing,
            isLookingSky = isLookingSky
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
            string[] layerName = {"EyeTrackingExplorationMaze","EyeTrackingExplorationChild","EyeTrackingExplorationPicture"};
            LayerMask mask = LayerMask.GetMask(layerName);
            LayerMask maskSky = LayerMask.GetMask("EyeTrackingExplorationSky");
            if (Physics.Raycast(pos, direction, out hit, 100f,mask))
            {
                if (hit.collider.CompareTag("Picture"))
                {
                    var name = hit.collider.name;
                    var id = Int32.Parse(name) - 1;
                    SetHitData(hit.point,_eyeData,0,true);
                    _explorationManager.IsFocusingObj(id);
                }
                else
                {
                    SetHitData(hit.point,_eyeData,0,false);
                    _explorationManager.IsNotFocusingObj();
                }
            }
            else
            {
                SetHitData(hit.point,_eyeData,1,false);
                _explorationManager.IsNotFocusingObj();
            }
        }
        else
        {
            SetInvalidData(_eyeData);
            _explorationManager.IsNotFocusingObj();
        }
    }
    
    public void WriteCSV()
    {
        if (_eyeTrackingSamples.Count > 0)
        {
            TextWriter tw = new StreamWriter(filePath, false);
            tw.WriteLine("TimeStamp,IsGazeRayValid,HitPointX,HitPointY,ChildPositionX,ChildPositionY,IsLeftEyeBlinking,IsRightEyeBlinking,IsFocusing,IsPointing,IsLookingSky");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for (int i = 0; i < _eyeTrackingSamples.Count; i++)
            {
                tw.WriteLine(_eyeTrackingSamples[i].timestamp +
                             "," + _eyeTrackingSamples[i].isGazeRayValid +
                             "," + _eyeTrackingSamples[i].hitPoint2Dx +
                             "," + _eyeTrackingSamples[i].hitPoint2Dy +
                             "," + _eyeTrackingSamples[i].cameraPos2Dx +
                             "," + _eyeTrackingSamples[i].cameraPos2Dy +
                             "," + _eyeTrackingSamples[i].isLeftEyeBlinking +
                             "," + _eyeTrackingSamples[i].isRightEyeBlinking +
                             "," + _eyeTrackingSamples[i].isFocusing +
                             "," + _eyeTrackingSamples[i].isPointing +
                             "," + _eyeTrackingSamples[i].isLookingSky
                );
            }
            tw.Close();
        }
    }


    private void UpdateFocusing()
    {
        _isFocusing = 0;
        for (int i = 0; i < pictures.transform.childCount; i++)
        {
            if (pictures.transform.GetChild(i).gameObject.GetComponent<FocusController>().getFocused())
            {
                _isFocusing = 1;
            }
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
        var isLookingSky = 0;
        
        var _eyeData = _eyeTracking.GetEyeTracking();
        RaycastHit hit;
        if (_eyeData.isGazeRayValid)
        {
            var pos = _eyeData.rayOrigin;
            var direction = _eyeData.rayDirection;
            string[] layerName = {"EyeTrackingExplorationMaze","EyeTrackingExplorationChild","EyeTrackingExplorationPicture"};
            LayerMask mask = LayerMask.GetMask(layerName);
            LayerMask maskSky = LayerMask.GetMask("EyeTrackingExplorationSky");
            if (Physics.Raycast(pos, direction, out hit, 100f,mask))
            {
                //Hit point calc
                hitX = hit.point.x;
                hitY = hit.point.z;
            }
            else
            {
                isLookingSky = 1;
            }
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
        var eyeData = new EyeTrackingSampleExploration()
        {
            timestamp = _eyeData.timestamp-initTime,
            isGazeRayValid = _eyeData.isGazeRayValid,
            hitPoint2Dx = hitX,
            hitPoint2Dy = hitY,
            cameraPos2Dx = _camera.transform.position.x,
            cameraPos2Dy = _camera.transform.position.z,
            isLeftEyeBlinking = isLeftEyeBlinking,
            isRightEyeBlinking = isRightEyeBlinking,
            isFocusing = _isFocusing,
            isPointing = _isPointing,
            isLookingSky = isLookingSky
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

public struct EyeTrackingSampleExploration
{
    public float timestamp;
    public bool isGazeRayValid;
    public float hitPoint2Dx;
    public float hitPoint2Dy;
    public float cameraPos2Dx;
    public float cameraPos2Dy;
    public int isLeftEyeBlinking;
    public int isRightEyeBlinking;
    public int isFocusing;
    public int isPointing;
    public int isLookingSky;
}
