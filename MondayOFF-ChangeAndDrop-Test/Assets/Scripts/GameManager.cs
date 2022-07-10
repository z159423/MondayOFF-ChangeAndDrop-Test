using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Button retryButton;
    [SerializeField] private CameraLogic camera;

    [Space]

    public Material[] blueMat;
    public Gradient blueColor;

    public Material[] orangeMat;
    public Gradient orangeColor;

    [SerializeField] private GameObject[] stages;

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;

        GenerateRandomStage();
    }

    public void GameOver()
    {
        StartCoroutine(delayTime());

        IEnumerator delayTime()
        {
            yield return new WaitForSeconds(0.5f);

            retryButton.gameObject.SetActive(true);
        }
    }

    public void GameClear()
    {
        camera.followBall = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GenerateRandomStage()
    {
        int value = Random.Range(0, stages.Length);

        stages[value].gameObject.SetActive(true);
    }
}
