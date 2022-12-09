using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ExplorationManager_1 : MonoBehaviour
{
    private GameObject _player;
    private GameObject _character;
    private bool _isHover=false;
    

    private int _currentCheckpoint;
    private bool _isMovingChar;
    private int _pointedObject;
    private Vector3[] _posPicture;
    private FocusController[] _focusControllers;
    
    [SerializeField] private float _playerSpeed=2f;
    [SerializeField] private float _speedCharacter = 4f;
    [SerializeField] private GameObject[] _checkpoints;
    [SerializeField] private UnityEvent writeCsvEyeTracking;
    [SerializeField] private UnityEvent setFinishedDataCollector;
    [SerializeField] private UnityEvent setPointing;
    [SerializeField] private UnityEvent setNotPointing;
    
    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("XR Origin");
        _character=GameObject.Find("Character");
        
        GameObject pictures = GameObject.Find("Picture");
        var childCount = pictures.transform.childCount;
        _posPicture = new Vector3[childCount];
        _focusControllers = new FocusController[childCount];
        
        for (int i = 0; i < childCount; i++)
        {
             _posPicture[i] = pictures.transform.GetChild(i).gameObject.transform.position;
             _focusControllers[i] = pictures.transform.GetChild(i).gameObject.GetComponent<FocusController>();
        }
        _isHover = false;

        _currentCheckpoint = 0;
        _character.transform.position = _checkpoints[0].transform.position;
        
        //Rotate character to player
        Vector3 movementDirection =_player.transform.position - _character.transform.position;
        movementDirection.Normalize();
        
        if(movementDirection!=Vector3.zero)
            _character.transform.forward = movementDirection;


        _isMovingChar = false;
    }
    
    private void SetRun(GameObject obj)
    {
        if (obj.GetComponentInChildren<Animator>())
        {
            obj.GetComponentInChildren<Animator>().SetTrigger("run");
        }
    }
    private void SetWave(GameObject obj)
    {
        if (obj.GetComponentInChildren<Animator>())
        {
            obj.GetComponentInChildren<Animator>().SetTrigger("wave");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isHover&&_focusControllers[_pointedObject].getFocused())
        {
            MoveTo(_player,_posPicture[_pointedObject],_playerSpeed);
        }
        
        if (_isMovingChar)
        {
            if (Vector3.Distance(_character.transform.position, _checkpoints[_currentCheckpoint].transform.position) <
                Single.Epsilon)
            {
                SetWave(_character);
                _isMovingChar = false;
                //Rotate character to player
                Vector3 movementDirection =_player.transform.position - _character.transform.position;
                movementDirection.Normalize();
                if(movementDirection!=Vector3.zero)
                    _character.transform.forward = movementDirection;
            }
            else
            {
                MoveToAndRotate(_character,_checkpoints[_currentCheckpoint],_speedCharacter);
            }
        }
    }
    private void MoveToAndRotate(GameObject obj,GameObject to , float speed )
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, to.transform.position, speed*Time.deltaTime);

        Vector3 movementDirection =to.transform.position - obj.transform.position;
        movementDirection.Normalize();

        if(movementDirection!=Vector3.zero)
            obj.transform.forward = movementDirection;


    }
    public void NextStep()
    {
        if (!_isMovingChar)
        {
            if (_currentCheckpoint + 1 == _checkpoints.Length)
            {
                setFinishedDataCollector.Invoke();
                LevelManager.Instance.PlayMainMenu();
            }
            else
            {
                _currentCheckpoint++;
                _isMovingChar = true;
                SetRun(_character);
            }
        }
    }
    private void MoveTo(GameObject obj,Vector3 to , float speed )
    {
        Vector3 nextPos= Vector3.MoveTowards(obj.transform.position, to, speed*Time.deltaTime); 
        Transform transform = obj.transform;
        transform.position = new Vector3(nextPos.x, transform.position.y, nextPos.z);

    }

    public void HoverEnteredCharacter(int id)
    {
        setPointing.Invoke();
        _isHover = true;
        _pointedObject = id;

    }
    public void HoverExitedCharacter()
    {
        setNotPointing.Invoke();
        _isHover = false;
    }
}
