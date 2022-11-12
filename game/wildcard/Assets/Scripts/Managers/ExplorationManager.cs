using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationManager : MonoBehaviour
{
    private GameObject _player;
    private GameObject _character;

    private int _currentCheckpoint;
    private bool _isHover;
    private bool _isMovingChar;
    
    [SerializeField] private float _speedPlayer = 2f;
    [SerializeField] private float _speedCharacter = 4f;
    [SerializeField] private GameObject[] _checkpoints;
    
    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("XR Origin");
        _character=GameObject.Find("Character");

        _currentCheckpoint = 0;
        _character.transform.position = _checkpoints[0].transform.position;


        _isHover = false;
        _isMovingChar = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isHover)
        {
            MoveTo(_player,_checkpoints[_currentCheckpoint],_speedPlayer);
        }

        if (_isMovingChar)
        {
            if (Vector3.Distance(_character.transform.position, _checkpoints[_currentCheckpoint].transform.position) <
                Single.Epsilon)
            {
                _isMovingChar = false;
            }
            else
            {
                MoveTo(_character,_checkpoints[_currentCheckpoint],_speedCharacter);
            }
            
        }

        if (Input.GetButton("Jump"))
        {
            MoveTo(_player,_checkpoints[_currentCheckpoint],_speedPlayer);
        }
        
    }

    private void MoveTo(GameObject obj,GameObject to , float speed )
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, to.transform.position, speed*Time.deltaTime);
    }
    
    
    public void HoverEnteredCharacter()
    {
        _isHover = true; 
    }
    public void HoverExitedCharacter()
    {
        _isHover = false;
    }

    public void NextStep()
    {
        if (!_isMovingChar)
        {
            if (_currentCheckpoint + 1 == _checkpoints.Length)
            {
                LevelManager.Instance.PlayMainMenu();
            }
            else
            {
                _currentCheckpoint++;
                _isMovingChar = true;
            }
            
            
        }
        
        
    }
    
    
}
