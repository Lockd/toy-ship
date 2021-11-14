using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text distanceText;
    public Text cointsText;

    public void onDisplayChange(bool isActive, float distance, int coins) {
        gameObject.SetActive(isActive);
        if (isActive) {
            distanceText.text = "You travelled " + distance.ToString() + " meters";
            cointsText.text = "And collected " + coins.ToString() + " coins";
        }
    }

    public void onClickRestartButton() {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onClickShopButton() {
        // TODO one day you should add shop
        SceneManager.LoadScene("Experiment");
    }
}
