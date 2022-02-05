using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bestDistanceText;
    public Text coinsText;
    public Text totalCoinsText;
    public Text newRecordText;
    public GameObject ingameUIGameObject;
    ingameUI ingameUI;
    float bestDistance;

    void Awake() {
        ingameUI = ingameUIGameObject.GetComponent<ingameUI>();
        newRecordText.text = "";
        bestDistance = PlayerPrefs.GetFloat("BestDistance");
        bestDistanceText.text = bestDistance.ToString();
    }

    public void onDisplayChange(bool isActive, float distance, int coins) {
        gameObject.SetActive(isActive);
        ingameUI.changeDisplay(!isActive);

        if (isActive) {
            float roundedDistance = Mathf.Round(distance);
            distanceText.text = roundedDistance.ToString();

            coinsText.text = "And collected " + coins.ToString() + " coin";
            if (!coins.ToString().EndsWith("1") || coins == 11) {
                coinsText.text += "s";
            }

            int totalCoins = PlayerPrefs.GetInt("Coins") + coins;
            PlayerPrefs.SetInt("Coins", totalCoins);
            totalCoinsText.text = "Total coins collected: " + totalCoins.ToString();

            float bestDistance = PlayerPrefs.GetFloat("BestDistance");
            if (roundedDistance > bestDistance) {
                newRecordText.text = "New record!";
            }
        }
    }

    public void onClickRestartButton() {
        gameObject.SetActive(false);
        ingameUI.changeDisplay(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onClickShopButton() {
        SceneManager.LoadScene("Shop");
    }
}
