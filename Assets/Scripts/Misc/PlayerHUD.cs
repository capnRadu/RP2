using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI playerHighscoreText;
    public TextMeshProUGUI comboText;

    public GameObject livesParentObject;

    public void LoseLife()
    {
        if (livesParentObject.transform.childCount > 1)
        {
            Destroy(livesParentObject.transform.GetChild(0).gameObject);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
