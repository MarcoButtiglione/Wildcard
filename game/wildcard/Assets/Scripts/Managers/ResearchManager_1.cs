using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;


public class ResearchManager_1 : MonoBehaviour
{
    private int _state;
    private List<int> _orderOfTheState;

    private GameObject _camera;
    private Vector3 _cameraPos;
    private GameObject _arrow;
    [SerializeField] private GameObject boomEffect;

    [SerializeField] private float _deltaAngle = 30f;

    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _heigth = 1f;
    [SerializeField] private float _deltaArrow = 1f;
    [SerializeField] private double _randomDirection = 0f;

    [SerializeField] private UnityEvent setFinishedDataCollector;
    [SerializeField] private UnityEvent setClicking;
    [SerializeField] private UnityEvent setClickingRight;

    private static Random rng = new Random();


    public static void Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }


    private void Awake()
    {

        _camera = GameObject.Find("XR Origin");
        _cameraPos = _camera.transform.position;
        float x, y, z;
        float angle;

        GameObject researchObj = GameObject.Find("ResearchObj");
        _orderOfTheState = new List<int>();
        for (int i = 0; i < researchObj.transform.childCount; i++)
        {
            _orderOfTheState.Insert(i, i);
        }
        //Shuffle(_orderOfTheState);

        for (int i = 0; i < researchObj.transform.childCount; i++)
        {
            angle = _orderOfTheState[i] * Mathf.PI * 2f / researchObj.transform.childCount;

            x = (Mathf.Cos(angle + ConvertToRadians(_deltaAngle)) * _radius) + _cameraPos.x;
            y = _heigth + _cameraPos.y;
            z = (Mathf.Sin(angle + ConvertToRadians(_deltaAngle)) * _radius) + _cameraPos.z;
            Vector3 newPos = new Vector3(x, y, z);
            GameObject _gameObject = researchObj.transform.GetChild(i).gameObject;
            _gameObject.transform.position = newPos;

            float randx = (float)(_randomDirection * (rng.NextDouble() - 0.5f));
            float randz = (float)(_randomDirection * (rng.NextDouble() - 0.5f));

            Vector3 _cameraPosCamera = new Vector3(_cameraPos.x + randx, y, _cameraPos.z + randz);

            Vector3 objDir = newPos - _cameraPosCamera;
            objDir.Normalize();
            if (objDir != Vector3.zero)
                _gameObject.transform.forward = objDir;

        }

        _state = 0;
    }
    private float ConvertToRadians(float angle)
    {
        return (float)(Math.PI / 180) * angle;
    }
    private void Start()
    {
        _arrow = GameObject.Find("Arrow");
        Vector3 posObj = GameObject.Find("ResearchObj").transform.GetChild(_orderOfTheState[0]).gameObject.transform.position;
        _arrow.transform.position = posObj + new Vector3(0, _deltaArrow, 0);

        Vector3 _cameraPosCamera = new Vector3(_cameraPos.x, _deltaArrow, _cameraPos.z);

        Vector3 arrowDirection = _cameraPosCamera - _arrow.transform.position;
        arrowDirection.Normalize();
        if (arrowDirection != Vector3.zero)
            _arrow.transform.forward = arrowDirection;
    }

    private void UpdateGameState(int newState)
    {
        _arrow = GameObject.Find("Arrow");
        GameObject researchObj = GameObject.Find("ResearchObj");
        Vector3 posObj = researchObj.transform.GetChild(_orderOfTheState[newState]).gameObject.transform.position;
        researchObj.transform.GetChild(_orderOfTheState[newState - 1]).gameObject.SetActive(false);


        _arrow.transform.position = posObj + new Vector3(0, _deltaArrow, 0);

        Vector3 _cameraPosCamera = new Vector3(_cameraPos.x, _deltaArrow, _cameraPos.z);

        boomEffect = Instantiate(boomEffect, researchObj.transform.GetChild(_orderOfTheState[newState - 1]).gameObject.transform.position, Quaternion.identity);
        boomEffect.transform.LookAt(_cameraPosCamera);


        Vector3 arrowDirection = _cameraPosCamera - _arrow.transform.position;
        arrowDirection.Normalize();
        if (arrowDirection != Vector3.zero)
            _arrow.transform.forward = arrowDirection;

        _state = newState;
    }


    private void NextState()
    {
        if (_state + 1 == GameObject.Find("ResearchObj").transform.childCount)
        {
            setFinishedDataCollector.Invoke();
            StartCoroutine("WaitFor");
            //_arrow = GameObject.Find("Arrow");
            //_arrow.transform.position =  GameObject.Find("ResearchObj").transform.GetChild(0).gameObject.transform.position+ new Vector3(0,_deltaArrow,0);
        }
        else
        {
            UpdateGameState(_state + 1);
        }
    }

    public void OnSelectGameObj(int id)
    {
        setClicking.Invoke();
        if (id == _orderOfTheState[_state])
        {
            setClickingRight.Invoke();
            GameObject researchObj = GameObject.Find("ResearchObj");
            FocusController focusControl = researchObj.transform.GetChild(_orderOfTheState[id]).gameObject.GetComponent<FocusController>();
            if (focusControl.getFocused())
            {
                NextState();
            }
        }
    }


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            NextState();
    }

    public int GetCurrentState()
    {
        return _state;
    }

    IEnumerator WaitFor()
    {
        _arrow = GameObject.Find("Arrow");
        _arrow.SetActive(false);
        GameObject researchObj = GameObject.Find("ResearchObj");
        //Vector3 posObj = researchObj.transform.GetChild(_orderOfTheState[newState]).gameObject.transform.position;
        researchObj.transform.GetChild(_orderOfTheState[_state]).gameObject.SetActive(false);


        //_arrow.transform.position = posObj + new Vector3(0,_deltaArrow,0);

        Vector3 _cameraPosCamera = new Vector3(_cameraPos.x, _deltaArrow, _cameraPos.z);

        boomEffect = Instantiate(boomEffect, researchObj.transform.GetChild(_orderOfTheState[_state]).gameObject.transform.position, Quaternion.identity);
        boomEffect.transform.LookAt(_cameraPosCamera);

        yield return new WaitForSecondsRealtime(1f);

        LevelManager.Instance.PlayMainMenu();

        _state = 0;
    }

}
