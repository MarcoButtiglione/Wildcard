using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StoryManager : MonoBehaviour
{
    private int _state;

    private GameObject _camera;
    private Vector3 _cameraPos;
    [SerializeField] private GameObject[] _gameObject;
    private List<int> _orderOfTheObject;
    
    private GameObject _arrow;

    private GameObject _character;
    [SerializeField] private float _initRot;
    [SerializeField] private float _speed = 0.1f;
    private bool _isMoving;
    private float _characterProgression;
    
    private GameObject _menu;
    private bool _levelCompleted=false;
    
    [SerializeField] private float _radius=3f;
    [SerializeField] private float _heigth=3f;
    [SerializeField] private float _deltaTaken = 0.3f;
    [SerializeField] private float _deltaArrow = 1.7f;
    [SerializeField] private double _randomDirection = 0f;
    
    private static Random rng = new Random();  

    public static void Shuffle (List<int> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            var k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    
    private void Awake()
    {
        _menu = GameObject.Find("MenuCanvas");
        _levelCompleted = false;
        _menu.SetActive(false);
        
        _camera = GameObject.Find("XR Origin");
        _cameraPos = _camera.transform.position;
        
        _character = GameObject.Find("CharacterParent");
        _character.transform.position = new Vector3(_radius+_cameraPos.x , _heigth+_cameraPos.y, _cameraPos.z);
        _character.transform.eulerAngles = new Vector3(0, _initRot, 0);
        
        _arrow = GameObject.Find("Arrow");
        Vector3 posObj = new Vector3(_radius+_cameraPos.x , _heigth+_cameraPos.y, _cameraPos.z);
        _arrow.transform.position = posObj + new Vector3(0,_deltaArrow,0);

        
        Vector3 cameraPosCamera = new Vector3(_cameraPos.x,_deltaArrow,_cameraPos.z);
        
        Vector3 arrowDirection =cameraPosCamera - _arrow.transform.position;
        arrowDirection.Normalize();
        if(arrowDirection!=Vector3.zero)
            _arrow.transform.forward = arrowDirection;
        
        //SetIdle(_character);
        _isMoving = false;
        _characterProgression = 0;
        
        _state = 0;
        
        _orderOfTheObject = new List<int>();
        for (int i = 0; i < _gameObject.Length; i++)
        {
            _orderOfTheObject.Insert(i,i);
        }
        Shuffle(_orderOfTheObject);
        
        for (int i = 0; i < _gameObject.Length; i++)
        {
            float angle = (i+1) * Mathf.PI*2f / (_gameObject.Length+1);
            float x =(Mathf.Cos(angle)*_radius)+_cameraPos.x ;
            float y = _heigth+_cameraPos.y;
            float z = (Mathf.Sin(angle) * _radius)+_cameraPos.z;
            Vector3 newPos = new Vector3(x, y, z);
            _gameObject[_orderOfTheObject[i]]=Instantiate(_gameObject[_orderOfTheObject[i]], newPos, Quaternion.identity);
            
            
            float randx = (float) (_randomDirection*(rng.NextDouble()-0.5f));
            float randz = (float) (_randomDirection*(rng.NextDouble()-0.5f));
            
            cameraPosCamera = new Vector3(_cameraPos.x+randx,y,_cameraPos.z+randz);
        
            Vector3 objDir =newPos - cameraPosCamera;
            objDir.Normalize();
            if(objDir!=Vector3.zero)
                _gameObject[_orderOfTheObject[i]].transform.forward = objDir;
        }
    }
    
    
    private void UpdateGameState(int newState)
    {
        _state = newState;
        _gameObject[_orderOfTheObject[newState-1]].SetActive(false);
    }

    private void SetWalking(GameObject obj)
    {
        if (obj.GetComponentInChildren<Animator>())
        {
            obj.GetComponentInChildren<Animator>().SetTrigger("walk");
        }
    }
    private void SetIdle(GameObject obj)
    {
        if (obj.GetComponentInChildren<Animator>())
        {
            obj.GetComponentInChildren<Animator>().SetTrigger("idle");
        }
    }

    private void MoveCharacter()
    {
        if (_arrow.activeSelf)
        {
            _arrow.SetActive(false);
        }
        
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
                _levelCompleted = true;
                _menu.SetActive(true);
                //LevelManager.Instance.PlayMainMenu();
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
