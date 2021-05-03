using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    [Header("Screens")]
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject prepareToPlay;
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private Button playBtn;

    public void ShowTitleScreen() {
        gameoverScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void ShowMainMenu() {
        titleScreen.SetActive(false);
        mainMenu.SetActive(true);
        playBtn.Select();
    }

    public void PrepareToPlay() {
        prepareToPlay.SetActive(true);
    }

    public void Playing() {
        prepareToPlay.SetActive(false);
    }

    public void GameOver() {
        gameoverScreen.SetActive(true);
    }
}
