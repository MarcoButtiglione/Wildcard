using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameType _currentGame;
    private int _state;
    public static event Action<int> OnGameStateChanged;

    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private double _radius=3f;
    private void Awake()
    {
        Instance = this;
        if (LevelManager.Instance)
        {
            _currentGame = LevelManager.Instance.GetCurrentGame();
        }
    }

    private void Start()
    {
        for (var i = 0; i < _gameObject.Length; i++)
        {
            var x = _radius * Math.Sin((i / _gameObject.Length) * Math.PI);
            var z = _radius * Math.Cos((i / _gameObject.Length) * Math.PI);
            Instantiate(_gameObject[i], new Vector3(i, 1, 0), Quaternion.identity);
        }
        UpdateGameState(0);
    }

    private void OnDestroy()
    {
        
    }

    private void UpdateGameState(int newState)
    {
        var x = _radius * Math.Sin((newState / _gameObject.Length) * Math.PI);
        var z = _radius * Math.Cos((newState / _gameObject.Length) * Math.PI);
        _arrow.transform.position = new Vector3(Convert.ToSingle(x), 4f, Convert.ToSingle(z));
        _state = newState;
        OnGameStateChanged?.Invoke(newState);
    }


    public void NextState()
    {
        if (_state +1< _gameObject.Length-1)
        {
            UpdateGameState(_state+1);
        }
        else
        {
            LevelManager.Instance.PlayMainMenu();
        }
    }



}
