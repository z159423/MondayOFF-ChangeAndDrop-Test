using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private BallGenerator ballGenerator;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private float cameraMoveSpeed = 1f;
    [SerializeField] private Vector3 cameraOffset ;

    public bool followBall = true;
    public bool followBox = false;
    public Transform boxPosition;

    public GameObject forcusBall;
    public GameObject fastestBall;

    public static CameraLogic instance;

    private void Awake()
    {
        instance = this;

        StartCoroutine(CheckFastestBall());
    }


    void LateUpdate()
    {
        if(followBox)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, boxPosition.transform.position.y, transform.position.z) + cameraOffset, cameraMoveSpeed * Time.deltaTime);
        }
        else if(ballGenerator.generatedBallList.Count > 0)
        {
            if(fastestBall != null)
            {
                if(fastestBall.activeSelf)
                {
                    cameraPosition = fastestBall.transform.position;

                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraPosition.y, transform.position.z) + cameraOffset, cameraMoveSpeed * Time.deltaTime);
                }
                else
                {
                    fastestBall = BallGenerator.instance.GetFastestBall();
                }
            }
            else
            {
                fastestBall = BallGenerator.instance.GetFastestBall();
            }
        }
        /*else if(ballGenerator.generatedBallList.Count > 0 && followBall)
        {
            if(forcusBall != null)
            {
                if(forcusBall.activeSelf)
                {
                    cameraPosition = forcusBall.transform.position;

                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraPosition.y, transform.position.z) + cameraOffset, cameraMoveSpeed * Time.deltaTime);
                }
                else
                {
                    cameraPosition = ballGenerator.generatedBallList[0].transform.position;

                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraPosition.y, transform.position.z) + cameraOffset, cameraMoveSpeed * Time.deltaTime);
                }

            }
            else
            {
                cameraPosition = ballGenerator.generatedBallList[0].transform.position;

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraPosition.y, transform.position.z) + cameraOffset, cameraMoveSpeed * Time.deltaTime);
            }
        }*/
        
    }

    public void FollowBall(GameObject ball)
    {
        forcusBall = ball;
    }

    public void FollowBox(Transform box)
    {
        followBox = true;

        boxPosition = box;
    }

    public IEnumerator CheckFastestBall()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            if (ballGenerator.generatedBallList.Count > 0)
                fastestBall = BallGenerator.instance.GetFastestBall();
        }
    }
}
