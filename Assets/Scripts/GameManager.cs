using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public CanvasGroup gameWin;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    public AudioSource gameOvermusic;
    public AudioSource victorymusic;

    public GameObject gameWinCanvas;

    private int score;

    private void Start()
    {
        gameWinCanvas.SetActive(false);
        NewGame();
    }

    public void NewGame()
    {
        gameWinCanvas.SetActive(false);
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        gameWin.alpha = 0f;
        gameWin.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

    }

    public void GameOver()
    {
        gameOvermusic.Play();
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

   
    public void Victory()
    {
        gameWinCanvas.SetActive(true);
        board.enabled = false;
        gameWin.interactable = true;
        victorymusic.Play();
        StartCoroutine(Fade(gameWin, 1f, 1f));
    }

    public void Continue()
    {
        gameWinCanvas.SetActive(false);
        gameWin.interactable = false;
        board.enabled = true;
    }
}
