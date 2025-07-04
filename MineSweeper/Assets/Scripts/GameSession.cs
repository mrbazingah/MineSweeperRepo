using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] float gameOverTimeDelay;

    bool firstClick = true;
    HashSet<GreenTile> visited = new HashSet<GreenTile>();
    GreenGridManager greenGridManager;

    bool gameOver = false;

    void Awake()
    {
        greenGridManager = FindObjectOfType<GreenGridManager>();
    }

    /// <summary>
    /// Call this from your tile when it’s clicked.
    /// </summary>
    public void OnTileClicked(GreenTile tile)
    {
        if (firstClick)
        {
            firstClick = false;
            greenGridManager.AssignBombs(tile);
            greenGridManager.CalculateAllNumbers();
        }

        visited.Clear();
        FloodFill(tile);
    }

    private void FloodFill(GreenTile tile)
    {
        if (visited.Contains(tile)) return;
        visited.Add(tile);

        tile.Reveal();

        if (tile.HasBomb)
        {
            StartCoroutine(ProcessLoss());
            return;
        }

        // Stop at numbered tiles
        if (tile.HasNumber) return;

        foreach (var neighborGO in tile.GetNeighbors())
        {
            var neighbor = neighborGO.GetComponent<GreenTile>();
            if (neighbor != null)
                FloodFill(neighbor);
        }
    }

    public void Win()
    {
        StartCoroutine(ProcessWin());
    }

    IEnumerator ProcessWin()
    {
        gameOver = true;

        yield return new WaitForSeconds(gameOverTimeDelay);

        winPanel.SetActive(true);
    }

    IEnumerator ProcessLoss()
    {
        gameOver = true;

        yield return new WaitForSeconds(gameOverTimeDelay);

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public bool GetGameOver()
    {
        return gameOver;
    }   
}
