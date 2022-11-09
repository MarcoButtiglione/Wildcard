using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    private int _state;
    
    private GameObject _camera;
    private Vector3 _cameraPos;
    [SerializeField] private GameObject[] _gameObject;
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

        for (int i = 0; i < _gameObject.Length; i++)
        {
            angle = i * Mathf.PI*2f / _gameObject.Length;
            x =(Mathf.Cos(angle)*_radius)+_cameraPos.x ; 
            y = _heigth+_cameraPos.y;
            z = (Mathf.Sin(angle) * _radius)+_cameraPos.z;
            Vector3 newPos = new Vector3(x, y, z);
            _gameObject[i]=Instantiate(_gameObject[i], newPos, Quaternion.identity);
        }
        
        _state = 0;
    }

    private void Start()
    {
        _arrow = GameObject.Find("Arrow");
        _arrow.transform.position = _gameObject[0].transform.position + new Vector3(0,_deltaArrow,0);
    }

    private void UpdateGameState(int newState)
    {       
        _arrow = GameObject.Find("Arrow");

        _arrow.transform.position = _gameObject[newState].transform.position + new Vector3(0,_deltaArrow,0);
        _state = newState;
    }


    private void NextState()
    {
        if (_state +1== _gameObject.Length)
        {
            LevelManager.Instance.PlayMainMenu();
            _state = 0;
            _arrow = GameObject.Find("Arrow");
            _arrow.transform.position = _gameObject[0].transform.position + new Vector3(0,_deltaArrow,0);
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
