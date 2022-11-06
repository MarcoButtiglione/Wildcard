using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameType _currentGame;
    private int _state;
    //public static event Action<int> OnGameStateChanged;

    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private double _radius=3f;
    private void Awake()
    {
        if (LevelManager.Instance)
        {
            _currentGame = LevelManager.Instance.GetCurrentGame();
        }
        _arrow= Instantiate(_arrow, new Vector3(0, 0.35f, 0.16f), Quaternion.identity); 
    }
    
    private void Start()
    {
        for (var i = 0; i < _gameObject.Length; i++)
        {
            Instantiate(_gameObject[i], new Vector3(i, 1,0), Quaternion.identity);
            _arrow.transform.position+=new Vector3(0,0,0.1f);
        }

        _state = 0;
    }

    private void OnDestroy()
    {
        
    }

    private void UpdateGameState(int newState)
    {
        _state = newState;
        //OnGameStateChanged?.Invoke(newState);
    }


    private void NextState()
    {
        
        if (_state +1== _gameObject.Length)
        {
            LevelManager.Instance.PlayMainMenu();
        }
        else
        {
            UpdateGameState(_state+1);
        }
    }

    public void OnSelectGameObj(int id)
    {
        Debug.Log("Current id: "+id+ "  Current state: "+_state);
        
        if (id == _state)
        {
            NextState();
        }
    }



}
