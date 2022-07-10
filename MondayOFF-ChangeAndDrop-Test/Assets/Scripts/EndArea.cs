using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class EndArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> Balls = new List<GameObject>();

    [SerializeField] private int goalCount = 50;
    [SerializeField] private int currentCount = 0;

    [SerializeField] private Animator boxAnimator;
    [SerializeField] private Transform gameClearUI;
    [SerializeField] private TextMeshProUGUI ballGoalCount;
    [SerializeField] private CameraLogic camera;

    [Space]

    [SerializeField] private Transform explodePosition;
    [SerializeField] private float explodeRadius = 3f;
    [SerializeField] private float explodeForce = 5;
    [SerializeField] private GameObject[] invisibleWalls;

    public UnityEvent gameClearEvent;

    private bool gameClear = false;
    private bool exploded = false;
    private bool forcusCamera = false;

    private Coroutine progressCorutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            if (Balls.Contains(other.gameObject))
                return;

            if(!forcusCamera)
            {
                forcusCamera = true;
                camera.FollowBox(explodePosition);
            }

            Balls.Add(other.gameObject);
            currentCount++;

            if (currentCount >= BallGenerator.instance.generatedBallList.Count)
                gameClearEvent.Invoke();

            ballGoalCount.text = currentCount + " / " + goalCount;

            if (gameClear)
                return;

            if(currentCount >= goalCount)
            {
                boxAnimator.SetFloat("AnimationSpeed", 0);
                StopCoroutine(progressCorutine);
                progressCorutine = null;

                boxAnimator.SetFloat("AnimationSpeed", 1);
                gameClear = true;
            }
            else
            {
                if(progressCorutine != null)
                {
                    boxAnimator.SetFloat("AnimationSpeed", 0);
                    StopCoroutine(progressCorutine);
                    progressCorutine = null;
                }

                progressCorutine = StartCoroutine(Progress());
            }
        }

        IEnumerator Progress()
        {
            boxAnimator.SetFloat("AnimationSpeed", 1);

            yield return new WaitForEndOfFrame();

            boxAnimator.SetFloat("AnimationSpeed", 0);
        }
    }

    public void Explode()
    {
        if(!exploded)
            StartCoroutine(explode());

        IEnumerator explode()
        {

            yield return new WaitForSeconds(1);

            for(int i = 0; i < invisibleWalls.Length; i++)
            {
                invisibleWalls[i].SetActive(false);
            }

            Collider[] colliders = Physics.OverlapSphere(explodePosition.position + (Vector3.up * 1.5f), explodeRadius);

            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<Ball>(out Ball ball))
                {
                    ball.Addforce(explodePosition.position, explodeForce);
                }
            }

            exploded = true;

            gameClearUI.gameObject.SetActive(true);
        }
    }
    
}
