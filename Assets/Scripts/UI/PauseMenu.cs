using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject ingameUIGameObject;
    public GameObject pauseMenuContainer;
    public Text countdownText;
    private bool isCountingDown = false;
    private float countdownLength;
    private bool isInCoroutine = false;
    ingameUI ingameUI;
    
    void Awake()
    {
        countdownText.gameObject.SetActive(false);
        countdownLength = 3;
        ingameUI = ingameUIGameObject.GetComponent<ingameUI>();
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
        pauseMenuContainer.gameObject.SetActive(false);
        ingameUI.changeDisplay(true);
    }

    public void getBackToScene()
    {
        countdownText.gameObject.SetActive(false);
        countdownLength = 3;
        isCountingDown = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        isGamePaused = false;
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        pauseMenuContainer.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        ingameUI.changeDisplay(false);
    }

    public void onRestart() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
