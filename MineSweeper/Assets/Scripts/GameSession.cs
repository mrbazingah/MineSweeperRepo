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

    /// <summary>
    /// If the tile is already revealed and has a number,
    /// and the player has exactly that many flags around it,
    /// reveal all other (non-flagged) neighbors.
    /// </summary>
    public void OnChord(GreenTile tile)
    {
        // only on a revealed number
        if (!tile.IsRevealed() || !tile.HasNumber)
            return;

        // count flags around
        int flagCount = 0;
        foreach (var go in tile.GetNeighbors())
        {
            var n = go.GetComponent<GreenTile>();
            if (n != null && n.IsFlagged())
                flagCount++;
        }

        // only proceed if the player's flags match the number
        if (flagCount != tile.GetBombCount())
            return;

        // clear visited so we get fresh flood-fill
        visited.Clear();

        // reveal each unflagged, unrevealed neighbor
        foreach (var go in tile.GetNeighbors())
        {
            var n = go.GetComponent<GreenTile>();
            if (n != null && !n.IsFlagged() && !n.IsRevealed())
            {
                FloodFill(n);
            }
        }
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
