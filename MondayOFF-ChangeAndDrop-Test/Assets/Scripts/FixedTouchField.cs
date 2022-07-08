using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Vector2 TouchDist;

    public Vector2 PointerOld;

    protected int PointerId;

    public bool Pressed;

    private bool BoxTrigger = false;
    private bool TouchEnable = true;

    [Space]

    [SerializeField] private Transform box;
    [SerializeField] private Animator boxAnimator;
    [SerializeField] private float boxMoveSpeed = 0.02f;
    [SerializeField] private float maxDragDistance;

    private Vector3 originBoxPosition;

    private void OnEnable()
    {
        originBoxPosition = box.transform.position;
    }

    void Update()
    {
        if (!TouchEnable)
            return;

        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;

                box.Translate(new Vector3(0, 0, TouchDist.x) * boxMoveSpeed * Time.deltaTime);
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }

            ClampBoxPosition();
        }
        else
        {
            TouchDist = new Vector2();
        }

        //box.transform.position = Vector3.ClampMagnitude(originBoxPosition, maxDragDistance);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;

        if (!BoxTrigger)
        {
            boxAnimator.SetTrigger("Close");
            BoxTrigger = true;
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        

        if (BoxTrigger)
        {
            boxAnimator.SetTrigger("Open");

            TouchEnable = false;
        }
    }

    private void ClampBoxPosition()
    {
        box.position = new Vector3(box.position.x, box.position.y, Mathf.Clamp(box.position.z, originBoxPosition.z - maxDragDistance, originBoxPosition.z + maxDragDistance));
    }

}
