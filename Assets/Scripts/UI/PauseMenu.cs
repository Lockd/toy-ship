using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject ingameUI;
    public Text countdownText;
    private bool isCountingDown = false;
    private float countdownLength;
    private bool isInCoroutine = false;

    void Awake()
    {
        countdownText.gameObject.SetActive(false);
        countdownLength = 3;
    }

    void Update()
    {
        if (isCountingDown && !isInCoroutine)
        {
            StartCoroutine(countDown());
        }
    }

    IEnumerator countDown() {
        if (countdownLength == 0) {
            getBackToScene();
        }
        isInCoroutine = true;
        countdownText.text = countdownLength.ToString("f0");
        
        countdownLength -= 1;
        yield return new WaitForSecondsRealtime(0.7f);
        isInCoroutine = false;
    }

    public void Resume()
    {
        isCountingDown = true;
        countdownText.gameObject.SetActive(true);
    }

    public void getBackToScene()
    {
        countdownText.gameObject.SetActive(false);
        countdownLength = 3;
        isCountingDown = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        isGamePaused = false;
        ingameUI.SetActive(true);
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        ingameUI.SetActive(false);
    }
}
