using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Button retryButton;

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }

    public void GameOver()
    {
        retryButton.gameObject.SetActive(true);
    }

    public void Retry()
    {

    }
}
