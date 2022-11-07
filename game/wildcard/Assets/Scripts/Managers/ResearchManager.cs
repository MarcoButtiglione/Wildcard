using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    private int _state;

    [SerializeField] private GameObject[] _gameObject;
    private GameObject _arrow;
    [SerializeField] private double _radius=3f;
    [SerializeField] private float _heigth=1f;
    
    
    private void Awake()
    {
        _state = 0;
    }
    
    private void Start()
    {
        for (var i = 0; i < _gameObject.Length; i++)
        {
            Instantiate(_gameObject[i], new Vector3(i, 1f,0), Quaternion.identity);
        }
        _arrow = GameObject.Find("Arrow");
        _arrow.transform.position = new Vector3(0, 2f, 0);
    }
    
    private void UpdateGameState(int newState)
    {       
        _arrow = GameObject.Find("Arrow");   
        _arrow.transform.position=new Vector3(newState,2f,0f);
        _state = newState;
    }


    private void NextState()
    {
        if (_state +1== _gameObject.Length)
        {
            LevelManager.Instance.PlayMainMenu();
            _state = 0;
            _arrow.transform.position = new Vector3(0, 2f, 0);
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
}
