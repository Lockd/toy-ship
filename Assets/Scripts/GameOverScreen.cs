using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text distanceText;
    public Text coinsText;
    public Text totalCoinsText;
    public Text newRecordText;

    public void Awake() {
        newRecordText.text = "";
    }

    public void onDisplayChange(bool isActive, float distance, int coins) {
        gameObject.SetActive(isActive);
        if (isActive) {
            float roundedDistance = Mathf.Round(distance);
            distanceText.text = "You travelled " + roundedDistance.ToString() + " meters";

            coinsText.text = "And collected " + coins.ToString() + " coin";
            if (!coins.ToString().EndsWith("1") || coins == 11) {
                coinsText.text += "s";
            }

            float totalCoins = PlayerPrefs.GetFloat("Coins") + coins;
            PlayerPrefs.SetFloat("Coins", totalCoins);
            totalCoinsText.text = "Coins: " + totalCoins.ToString();

            float bestDistance = PlayerPrefs.GetFloat("BestDistance");
            if (roundedDistance > bestDistance) {
                PlayerPrefs.SetFloat("BestDistance", roundedDistance);
                newRecordText.text = "New record!";
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
