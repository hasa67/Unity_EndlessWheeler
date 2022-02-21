using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static bool gameOver;
    public static bool isGameStarted;

    public GameObject gameOverPanel;
    public GameObject tapToStartText;

    public static int numberOfCoins;
    public Text coinText;

    private void Start() {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;

        gameOverPanel.SetActive(false);
        tapToStartText.SetActive(true);

        numberOfCoins = 0;
    }

    void Update() {
        if (gameOver == true) {
            gameOverPanel.SetActive(true);
        }

        coinText.text = "Coins: " + numberOfCoins;

        if (isGameStarted == false) {
            if (SwipeManager.tap) {
                isGameStarted = true;
                tapToStartText.SetActive(false);
                FindObjectOfType<AudioManager>().PlaySound("Background");
            }
        }
    }
}
