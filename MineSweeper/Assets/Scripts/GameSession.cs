using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject gameOverCanvas;

    int amountOfFlaggedBombs = 0;
    int amountOfBombs = 0;

    GreenTile greenTile;

    void Start()
    {
        greenTile = FindObjectOfType<GreenTile>();

        amountOfBombs = FindObjectsOfType<Bomb>().Length;

        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        
    }

    void Update()
    {
        CheckFlaggedBombs();
    }

    void CheckFlaggedBombs()
    {
        amountOfFlaggedBombs += greenTile.ReturnAmountOfFlaggedBombs();

        if (amountOfBombs == amountOfFlaggedBombs)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Lose()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
