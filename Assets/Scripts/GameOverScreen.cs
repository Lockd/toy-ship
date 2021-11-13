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
        distanceText.text = "You travelled " + distance.ToString() + " meters";
        cointsText.text = "And collected " + coins.ToString() + " coins";
    }

    public void onClickRestartButton() {
        SceneManager.LoadScene("Road");
    }

    public void onClickShopButton() {
        // TODO one day you should add shop
        SceneManager.LoadScene("Experiment");
    }
}
