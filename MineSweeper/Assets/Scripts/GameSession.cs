using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] int amountOfFlaggedBombs = 0;

    int amountOfBombs = 0;
    bool hasStarted;

    GreenTile greenTile;

    void Start()
    {
        greenTile = FindObjectOfType<GreenTile>();

        amountOfBombs = FindObjectsOfType<Bomb>().Length;

        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        
    }

    public void AddFlaggedBombs()
    {
        amountOfFlaggedBombs++;

        Debug.Log("+1 Flag");

        if (amountOfBombs <= amountOfFlaggedBombs)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0;

            Debug.Log("You Won");
        }
    }

    public void SubtractFlaggedBombs()
    {
        amountOfFlaggedBombs--;

        Debug.Log("-1 Flag");

        if (amountOfBombs <= amountOfFlaggedBombs)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0;

            Debug.Log("You Won");
        }
    }

    void StartOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
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

        Time.timeScale = 1;
    }
}
