using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    private int _state;
    
    private GameObject _camera;
    private Vector3 _cameraPos;
    private GameObject _arrow;
    
    
    [SerializeField] private float _radius=3f;
    [SerializeField] private float _heigth=1f;
    [SerializeField] private float _deltaArrow=1f;
    
    private void Awake()
    {
        _camera = GameObject.Find("XR Origin");
        _cameraPos = _camera.transform.position;
        float x,y,z ;
        float angle;
        
        GameObject researchObj = GameObject.Find("ResearchObj");

        for (int i = 0; i < researchObj.transform.childCount; i++)
        {
            angle = i * Mathf.PI*2f / researchObj.transform.childCount;
            x =(Mathf.Cos(angle)*_radius)+_cameraPos.x ; 
            y = _heigth+_cameraPos.y;
            z = (Mathf.Sin(angle) * _radius)+_cameraPos.z;
            Vector3 newPos = new Vector3(x, y, z);
            GameObject _gameObject = researchObj.transform.GetChild(i).gameObject;
            _gameObject.transform.position = newPos;
        }
        
        _state = 0;
    }

    private void Start()
    {
        _arrow = GameObject.Find("Arrow");
        Vector3 posObj = GameObject.Find("ResearchObj").transform.GetChild(0).gameObject.transform.position;
        _arrow.transform.position = posObj + new Vector3(0,_deltaArrow,0);
    }

    private void UpdateGameState(int newState)
    {       
        _arrow = GameObject.Find("Arrow");
        GameObject researchObj = GameObject.Find("ResearchObj");
        Vector3 posObj = researchObj.transform.GetChild(newState).gameObject.transform.position;
        
        float angle = newState * 360f / researchObj.transform.childCount;
        Debug.Log(angle);

        _arrow.transform.position = posObj + new Vector3(0,_deltaArrow,0);
        _arrow.transform.eulerAngles += new Vector3(0, angle, 0);
        
        _state = newState;
    }


    private void NextState()
    {
        if (_state +1== GameObject.Find("ResearchObj").transform.childCount)
        {
            LevelManager.Instance.PlayMainMenu();
            _state = 0;
            _arrow = GameObject.Find("Arrow");
            _arrow.transform.position =  GameObject.Find("ResearchObj").transform.GetChild(0).gameObject.transform.position
                                         + new Vector3(0,_deltaArrow,0);
        }
        else
        {
            UpdateGameState(_state+1);
        }
    }

    public void OnSelectGameObj(int id)
    {
        if (id == _state)
        {
            NextState();
        }
    }

    
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            NextState();
    }
    
}
