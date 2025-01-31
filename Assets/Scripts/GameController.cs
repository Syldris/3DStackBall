using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlatformSpawner platformSpawner;
    [SerializeField]
    private UOController uiController;

    private RandomColor randomColor;


    private int brokePlatformCount = 0;
    private int totalPlatformCount;

    public bool IsGamePlay { private set; get; } = false;
    private void Awake()
    {
        //���� ������������ ����ϴ� �÷��� ����
        totalPlatformCount = platformSpawner.SpawnPlatforms();
        //���� �ε��� �� ���� ���� ����
        // Pole, Platform, Player, UI(CurrentLevel, NextLevel, ProgressBar)
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameStart();
                yield break;
            }
            yield return null;
        }
    }

    private void GameStart()
    {
        IsGamePlay = true;
        uiController.GameStart();
    }

    public void OnCollisionWithPlatform()
    {
        brokePlatformCount++;
        uiController.levelProgressBar.fillAmount = (float)brokePlatformCount / (float)totalPlatformCount;

    }
}
