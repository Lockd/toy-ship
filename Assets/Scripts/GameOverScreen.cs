using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text distanceText;
    public Text coinsText;

    public void onDisplayChange(bool isActive, float distance, int coins) {
        gameObject.SetActive(isActive);
        if (isActive) {
            distanceText.text = "You travelled " + Mathf.Round(distance).ToString() + " meters";
            coinsText.text = "And collected " + coins.ToString() + " coin";
            if (!coins.ToString().EndsWith("1") || coins == 11) {
                coinsText.text += "s";
            }
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
