using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private int _state;

    private GameObject _camera;
    private Vector3 _cameraPos;
    [SerializeField] private GameObject[] _gameObject;
    private GameObject _character;
    [SerializeField] private float _initRot;
    [SerializeField] private float _speed = 0.1f;
    private bool _isMoving;
    private float _characterProgression;
    
    [SerializeField] private float _radius=3f;
    [SerializeField] private float _heigth=3f;
    [SerializeField] private float _deltaTaken = 0.3f;
    
    
    private void Awake()
    {
        _camera = GameObject.Find("XR Origin");
        _cameraPos = _camera.transform.position;
        
        _character = GameObject.Find("CharacterParent");
        _character.transform.position = new Vector3(_radius+_cameraPos.x , _heigth+_cameraPos.y, _cameraPos.z);
        _character.transform.eulerAngles = new Vector3(0, _initRot, 0);
        
        _isMoving = false;
        _characterProgression = 0;
        
        _state = 0;
        
        for (int i = 0; i < _gameObject.Length; i++)
        {
            float angle = (i+1) * Mathf.PI*2f / (_gameObject.Length+1);
            float x =(Mathf.Cos(angle)*_radius)+_cameraPos.x ;
            float y = _heigth+_cameraPos.y;
            float z = (Mathf.Sin(angle) * _radius)+_cameraPos.z;
            Vector3 newPos = new Vector3(x, y, z);
            _gameObject[i]=Instantiate(_gameObject[i], newPos, Quaternion.identity);
        }
    }
    
    
    private void UpdateGameState(int newState)
    {
        _state = newState;
        _gameObject[newState-1].SetActive(false);
    }


    private void MoveCharacter()
    {
        _characterProgression += _speed * Time.fixedDeltaTime;
        
        float angle = _characterProgression * Mathf.PI*2f / (_gameObject.Length+1);
        float x =(Mathf.Cos(angle)*_radius)+_cameraPos.x ;
        //float y = _heigth+_cameraPos.y;
        float y = _character.transform.position.y;
        float z = (Mathf.Sin(angle) * _radius)+_cameraPos.z;
        Vector3 newPos = new Vector3(x, y, z);
        
        _character.transform.position = newPos;

        float rotY = _initRot - (_characterProgression * 360 / (_gameObject.Length+1));
        
        _character.transform.eulerAngles = new Vector3(0, rotY, 0);
        
        //If the position have overpassed the position of next object in the scene...
        if (_characterProgression+_deltaTaken>_state+1)
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

        if (Input.GetButton("Jump"))
        {
            MoveCharacter();
        }
            
    }
}
