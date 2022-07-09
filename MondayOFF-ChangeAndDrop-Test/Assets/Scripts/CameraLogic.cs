using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private BallGenerator ballGenerator;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private float cameraMoveSpeed = 1f;
    [SerializeField] private Vector3 cameraOffset ;


    void LateUpdate()
    {
        if(ballGenerator.generatedBallList.Count > 0)
        {
            cameraPosition = ballGenerator.generatedBallList[0].transform.position;

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraPosition.y, transform.position.z) + cameraOffset, cameraMoveSpeed * Time.deltaTime);
        }
        
    }
}
