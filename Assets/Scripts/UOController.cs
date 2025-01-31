using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UOController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private TextMeshProUGUI currentLevel;
    [SerializeField]
    private TextMeshProUGUI nextLevel;

    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;

    [Header("inGame")]
    [SerializeField]
    public Image levelProgressBar;
    [SerializeField]
    private TextMeshProUGUI currentScore;

    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("LEVEL") + 1).ToString();
        nextLevel.text = (PlayerPrefs.GetInt("LEVEL") + 2).ToString();

        if (PlayerPrefs.GetInt("DEACTIVATEMAIN") == 0) mainPanel.SetActive(true);
        else mainPanel.SetActive(false);
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
    }

    private float LevelProgressBar { set => levelProgressBar.fillAmount = value; }

    public int CurrentScore { set => currentScore.text = value.ToString(); }
}
