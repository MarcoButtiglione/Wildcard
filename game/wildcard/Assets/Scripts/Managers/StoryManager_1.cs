using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager_1 : MonoBehaviour
{
    private int _state;

    private GameObject _camera;
    private Vector3 _cameraPos;
    private Vector3 _cameraPosCamera;
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private GameObject[] _gameObject;
    private GameObject _character;
    [SerializeField] private float _initRot;
    [SerializeField] private float _speed = 0.1f;
    private bool _isMoving;
    private float _characterProgression;
    
    [SerializeField] private float _radius=3f;
    [SerializeField] private float _heigth=3f;
    [SerializeField] private float _deltaTaken = 0.3f;

    [SerializeField] private Sprite _spriteIdle;
    [SerializeField] private Sprite _spriteWalking;
    
    
    private void Awake()
    {
        _camera = GameObject.Find("XR Origin");
        _cameraPos = _camera.transform.position;
        
        _character = GameObject.Find("CharacterParent");
        _character.transform.position = new Vector3(_cameraPos.x , _heigth+_cameraPos.y, _radius+_cameraPos.z);

        Vector3 camPos =  new Vector3(_cameraPos.x , _heigth+_cameraPos.y, _cameraPos.z); ;
        Vector3 charDir = _character.transform.position - camPos ;
        charDir.Normalize();
        if(charDir!=Vector3.zero)
            _character.transform.forward = charDir;
        
        //SetIdle(_character);
        _isMoving = false;
        _characterProgression = 0;
        
        _state = 0;
        
        for (int i = 0; i < _gameObject.Length; i++)
        {
            float angle = (i+1) * Mathf.PI*2f / (_gameObject.Length+1);
            float x =(Mathf.Sin(angle)*_radius)+_cameraPos.x ;
            float y = _heigth+_cameraPos.y;
            float z = (Mathf.Cos(angle) * _radius)+_cameraPos.z;
            Vector3 newPos = new Vector3(x, y, z);
            _gameObject[i]=Instantiate(_gameObject[i], newPos, Quaternion.identity);
            
            
            _cameraPosCamera = new Vector3(_cameraPos.x,y,_cameraPos.z);
        
            Vector3 objDir = newPos -  _cameraPosCamera;
            objDir.Normalize();
            if(objDir!=Vector3.zero)
                _gameObject[i].transform.forward = objDir;

        }
    }
    
    
    private void UpdateGameState(int newState)
    {
        _state = newState;
        _gameObject[newState-1].SetActive(false);
        boomEffect = Instantiate(boomEffect,_gameObject[newState-1].transform.position,Quaternion.identity);
        boomEffect.transform.LookAt(_cameraPosCamera);
    }

    private void SetWalking(GameObject obj)
    {
        if (obj.GetComponentInChildren<SpriteRenderer>())
        {
            obj.GetComponentInChildren<SpriteRenderer>().sprite=_spriteWalking;
        }
    }
    private void SetIdle(GameObject obj)
    {
        if (obj.GetComponentInChildren<SpriteRenderer>())
        {
            obj.GetComponentInChildren<SpriteRenderer>().sprite=_spriteIdle;
        }
    }

    private void MoveCharacter()
    {
        _characterProgression += _speed * Time.fixedDeltaTime;
        
        float angle = _characterProgression * Mathf.PI*2f / (_gameObject.Length+1);
        float x =(Mathf.Sin(angle)*_radius)+_cameraPos.x ;
        //float y = _heigth+_cameraPos.y;
        float y = _character.transform.position.y;
        float z = (Mathf.Cos(angle) * _radius)+_cameraPos.z;
        Vector3 newPos = new Vector3(x, y, z);
        
        _character.transform.position = newPos;
        
        
        Vector3 _cameraPosCamera = new Vector3(_cameraPos.x,y,_cameraPos.z);
        
        Vector3 objDir =newPos - _cameraPosCamera ;
        objDir.Normalize();
        if(objDir!=Vector3.zero)
            _character.transform.forward = objDir;
        
        
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
        SetWalking(_character);
    }
    public void HoverExitedCharacter()
    {
        _isMoving = false;
        SetIdle(_character);
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveCharacter();
        }

        if (Input.GetButtonDown("Jump"))
        {
            HoverEnteredCharacter();
        }
        if (Input.GetButtonUp("Jump"))
        {
            HoverExitedCharacter();
        }
            
    }
}
