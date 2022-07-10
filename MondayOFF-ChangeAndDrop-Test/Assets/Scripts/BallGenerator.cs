using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] defaultBalls;

    public List<GameObject> generatedBallList = new List<GameObject>();

    private Stack<GameObject> ballStack = new Stack<GameObject>();
    [SerializeField] private Color currentBallColor;

    [Space]

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform objectPoolParent;
    [SerializeField] private GameObject colorChangeButton;

    [Space]

    [SerializeField] private Transform ballReleasePosition;
    [SerializeField] private int ballReleaseAmount = 5;

    public static BallGenerator instance;

    [Space]

    public UnityEvent gameOverEvent;

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


    public GameObject GenerateBall(Vector3 position, Vector3 addForce)
    {
        if(ballStack.Count > 0)
        {
            GameObject ball = ballStack.Pop();
            generatedBallList.Add(ball);

            ball.transform.position = position;

            ball.GetComponent<Ball>().ChangeBallCurrentColor(currentBallColor);

            ball.SetActive(true);

            ball.GetComponent<Rigidbody>().AddForce(addForce);
            return ball;
        }
        else
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity, objectPoolParent);

            ball.GetComponent<Ball>().ChangeBallCurrentColor(currentBallColor);
            generatedBallList.Add(ball);

            ball.GetComponent<Rigidbody>().AddForce(addForce);
            return ball;
        }
    }

    public void PushBall(GameObject ball)
    {
        if (generatedBallList.Contains(ball))
            generatedBallList.Remove(ball);

        ballStack.Push(ball);

        ball.SetActive(false);

        if (generatedBallList.Count <= 0)
        {
            Debug.LogError("2222");
            gameOverEvent.Invoke();
        }
            


    }

    public void ReleaseBall()
    {
        for(int i = 0; i < ballReleaseAmount; i++)
        {
            //StartCoroutine(generateBall());
            GenerateBall(new Vector3(ballReleasePosition.position.x, ballReleasePosition.position.y + Random.Range(-0.1f, 0.1f), ballReleasePosition.position.z + Random.Range(-0.1f, 0.1f)), Vector3.zero);


        }

        colorChangeButton.SetActive(true);

        IEnumerator generateBall()
        {
            GenerateBall(new Vector3(ballReleasePosition.position.x, ballReleasePosition.position.y + Random.Range(-0.1f, 0.1f), ballReleasePosition.position.z + Random.Range(-0.1f, 0.1f)), Vector3.zero);

            yield return new WaitForSeconds(0.2f);
        }

        
    }

    public void GeneratedBallColorChange()
    {
        if (currentBallColor == Color.Blue)
        {
            currentBallColor = Color.Orange;
        }
        else if (currentBallColor == Color.Orange)
        {
            currentBallColor = Color.Blue;
        }

        for (int i = 0; i < generatedBallList.Count; i++)
        {
            generatedBallList[i].GetComponent<Ball>().BallColorChange();
        }
    }

    public void SwapList(GameObject ball)
    {
        int idx = generatedBallList.FindIndex(a => ball);

        Debug.LogError(idx);

        GameObject temp = generatedBallList[0];

        generatedBallList[0] = ball;
        generatedBallList[idx] = temp;
    }

    public GameObject GetFastestBall()
    {
        GameObject fastestBall = null;
        float height = 100;

        for(int i = 0; i < generatedBallList.Count; i++)
        {
            if (generatedBallList[i].transform.position.y < height)
            {
                fastestBall = generatedBallList[i];
                height = generatedBallList[i].transform.position.y;
            }
        }

        return fastestBall;
    }
}
