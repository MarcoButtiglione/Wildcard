using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionExplorationController : MonoBehaviour
{
    [SerializeField] private UnityEvent interactAction;
    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("MainCamera")){
            interactAction.Invoke();
        }
    }
}
