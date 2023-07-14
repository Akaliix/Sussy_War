using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    public bool isGameStarted = false;
    [SerializeField]
    public bool isGamePaused = false;

    [SerializeField]
    public bool isMobile = false;

    [SerializeField]
    private TextMeshProUGUI inGameKillCountText;
    [SerializeField]
    private CanvasGroup panel;
    [SerializeField]
    private TextMeshProUGUI panelKillCountText;
    [SerializeField]
    private TextMeshProUGUI panelHighScoreText;

    private int KillCount = 0;

    public static GameSettings singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Debug.LogError("Two GameSettings Singleton!!!");
        }
        inGameKillCountText.text = "Kill: " + KillCount;
    }

    public void KilledEnemy()
    {
        inGameKillCountText.text = "Kill: " + ++KillCount;
    }

    public void GameOver()
    {
        isGamePaused = true;

        panel.interactable = true;
        panel.blocksRaycasts = true;
        panelKillCountText.text = "Kill: " + KillCount;
        int lastHighScore = PlayerPrefs.GetInt("KillCount", 0);
        if (lastHighScore < KillCount)
        {
            lastHighScore = KillCount;
            PlayerPrefs.SetInt("KillCount", KillCount);
        }
        panelHighScoreText.text = "High Score: " + lastHighScore;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float alpha = 0;
        float fadeEndValue = 1;
        //fadeOutUIImage.enabled = true;
        while (alpha <= fadeEndValue)
        {
            alpha += Time.deltaTime;
            panel.alpha = alpha;
            yield return new WaitForEndOfFrame();
        }
    }
}
