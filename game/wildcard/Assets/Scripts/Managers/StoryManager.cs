using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private int _state;

    [SerializeField] private GameObject[] _gameObject;
    private GameObject _character;
    [SerializeField] private float _speed = 0.1f;
    private bool _isMoving;
    
    [SerializeField] private double _radius=3f;
    [SerializeField] private float _heigth=1f;
    
    
    private void Awake()
    {
        _character = GameObject.Find("Character");
        _character.transform.position = new Vector3(0f, 3f, 0f);
        _isMoving = false;
        
        _state = 0;
        for (var i = 0; i < _gameObject.Length; i++)
        {
            _gameObject[i]=Instantiate(_gameObject[i], new Vector3(i+1f, 3f,0f), Quaternion.identity);
        }
    }
    
    
    private void UpdateGameState(int newState)
    {
        _state = newState;
        _gameObject[newState-1].SetActive(false);
    }


    private void MoveCharacter()
    {
        _character.transform.position += new Vector3(_speed*Time.fixedDeltaTime,0f,0f);
        //If the position have overpassed the position of next object in the scene...
        if (_character.transform.position.x>_state+1)
        {
            //If the game is finished
            if (_state +1== _gameObject.Length)
            {
                LevelManager.Instance.PlayMainMenu();
                _state = 0;
                _character.transform.position = new Vector3(0f, 0f, 0f);
                for (var i = 0; i < _gameObject.Length; i++)
                {
                    _gameObject[i].SetActive(true);
                }
            }
            //Else update the current game state
            else
            {
                UpdateGameState(_state+1);
            }
        }
        
        
    }

    
    public void HoverEnteredCharacter()
    {
        _isMoving = true;
    }
    public void HoverExitedCharacter()
    {
        _isMoving = false;
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveCharacter();
        }
            //_character.transform.position += new Vector3(_speed*Time.fixedDeltaTime,0f,0f);
    }
}
