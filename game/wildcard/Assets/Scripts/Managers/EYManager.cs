using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.PXR;
using UnityEngine;
public class EYManager : MonoBehaviour
{
    public static EYManager Instance;
    [SerializeField] private PXR_Manager _pxrManager;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private List<Camera[]> _eyeTrackingRecords;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        Camera[] _cameras = _pxrManager.GetEyeCamera();
        _textMeshPro.SetText("Pos x: "+_cameras[0].transform.position.x+
                             "; Pos y: "+_cameras[0].transform.position.y+
                             "; Pos z: "+_cameras[0].transform.position.z+
                             "; Dir x: "+_cameras[0].transform.forward.x+
                             "; Dir y: "+_cameras[0].transform.forward.y+
                             "; Dir z: "+_cameras[0].transform.forward.z
                             );
        
        _eyeTrackingRecords.Insert(_eyeTrackingRecords.Count,_cameras);
        
    }
}
