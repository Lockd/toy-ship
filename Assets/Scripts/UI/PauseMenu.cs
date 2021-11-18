using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject ingameUI;
    public Text countdownText;

    void Awake()
    {
        countdownText.gameObject.SetActive(false);
    }
    public void Resume()
    {
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
