using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject gameOverCanvas;

    int amountOfUnBombedTiles = 0;
    bool hasStarted;

    void Start()
    {
        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        Win();
    }

    public void AddAmountOfUnBombedTiles()
    {
        amountOfUnBombedTiles++;
    }

    public void SubtractAmountOfUnBombedTiles()
    {
        amountOfUnBombedTiles--;
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

    void Win()
    {
        if (amountOfUnBombedTiles == 0)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        Time.timeScale = 1;
    }
}
