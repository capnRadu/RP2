using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerHUD playerHudScript;

    private int playerScore;
    private int playerHighScore = 0;

    private int comboMultiplier = 1;
    private float comboTimer = 0f;
    [NonSerialized] public float maxComboTime = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        comboTimer += Time.deltaTime;

        if (comboTimer >= maxComboTime && playerHudScript.comboText.gameObject.activeSelf)
        {
            playerHudScript.comboText.gameObject.SetActive(false);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerHudScript = FindObjectOfType<PlayerHUD>();

        playerScore = 0;
        playerHudScript.playerScoreText.text = $"{playerScore}";

        playerHudScript.playerHighscoreText.text = $"HI {playerHighScore}";
    }

    public void UpdatePlayerScore(int score)
    {
        playerScore += score;
        playerHudScript.playerScoreText.text = $"{playerScore}";

        if (playerScore > playerHighScore)
        {
            playerHighScore = playerScore;
            playerHudScript.playerHighscoreText.text = $"HI {playerHighScore}";
        }
    }

    public int CalculateCombo()
    {
        if (comboTimer <= maxComboTime)
        {
            comboMultiplier++;
            playerHudScript.comboText.text = $"x{comboMultiplier}";
            playerHudScript.comboText.gameObject.SetActive(true);
        }
        else
        {
            comboMultiplier = 1;
            playerHudScript.comboText.gameObject.SetActive(false);
        }

        comboTimer = 0f;

        return comboMultiplier;
    }
}
