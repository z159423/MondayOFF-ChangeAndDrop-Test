using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] defaultBalls;

    public List<GameObject> generatedBallList = new List<GameObject>();

    private Stack<GameObject> ballStack = new Stack<GameObject>();

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform objectPoolParent;

    [Space]

    [SerializeField] private Transform ballReleasePosition;
    [SerializeField] private int ballReleaseAmount = 5;

    public static BallGenerator instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        for(int i = 0; i < defaultBalls.Length; i++)
        {
            ballStack.Push(defaultBalls[i].gameObject);
        }
    }


    public GameObject GenerateBall(Vector3 position)
    {
        if(ballStack.Count > 0)
        {
            GameObject ball = ballStack.Pop();
            generatedBallList.Add(ball);

            ball.transform.position = position;

            ball.SetActive(true);
            return ball;
        }
        else
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity, objectPoolParent);
            generatedBallList.Add(ball);

            return ball;
        }
    }

    public void PushBall(GameObject ball)
    {
        if (generatedBallList.Contains(ball))
            generatedBallList.Remove(ball);

        ballStack.Push(ball);

        ball.SetActive(false);
    }

    public void ReleaseBall()
    {
        for(int i = 0; i < ballReleaseAmount; i++)
        {
            StartCoroutine(generateBall());
        }

        IEnumerator generateBall()
        {
            GenerateBall(new Vector3(ballReleasePosition.position.x, ballReleasePosition.position.y + Random.Range(-0.1f, 0.1f), ballReleasePosition.position.z + Random.Range(-0.1f, 0.1f)));

            yield return new WaitForSeconds(0.2f);
        }
    }
}
