using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Box : MonoBehaviour
{
    public UnityEvent boxOpenEvent;
    [SerializeField] private BallGenerator ballGenerator;

    public void BoxOpen()
    {
        boxOpenEvent.Invoke();
    }
}
