using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreenGridManager : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GreenTile greenTilePrefab;
    [SerializeField] Transform camPos;
    [SerializeField] GameObject parent;
    [SerializeField] int amountOfBombs;

    [Header("Bomb Placement Settings")]
    [SerializeField, Min(0)] int safeAreaRadius = 1;

    private List<GameObject> allTiles = new List<GameObject>();

    void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var tile = Instantiate(greenTilePrefab,
                                       new Vector3(x, y), Quaternion.identity,
                                       parent.transform);
                tile.name = $"GreenTile {x}{y}";
                tile.SetGridPosition(x, y);
                tile.SetIndex($"{x}{y}");

                bool isOffset = (x % 2 == 0 && y % 2 != 0)
                             || (x % 2 != 0 && y % 2 == 0);
                tile.SetColor(isOffset);

                allTiles.Add(tile.gameObject);
            }

        camPos.position = new Vector3(
            width / 2f - 0.5f,
            height / 2f - 0.5f,
            -10f
        );
    }

    /// <summary>
    /// Excludes a square of radius ≤ safeAreaRadius around startTile,
    /// then places exactly amountOfBombs bombs on the rest.
    /// </summary>
    public void AssignBombs(GreenTile startTile)
    {
        var center = startTile.GridPosition;
        var forbidden = new HashSet<GreenTile>();

        // Build forbidden set by Chebyshev distance
        foreach (var go in allTiles)
        {
            var t = go.GetComponent<GreenTile>();
            if (t == null) continue;
            var p = t.GridPosition;
            if (Mathf.Max(
                    Mathf.Abs(p.x - center.x),
                    Mathf.Abs(p.y - center.y)
                ) <= safeAreaRadius)
                forbidden.Add(t);
        }

        // All other tiles are candidates
        var candidates = allTiles
            .Select(g => g.GetComponent<GreenTile>())
            .Where(t => t != null && !forbidden.Contains(t))
            .ToList();

        int toPlace = Mathf.Min(amountOfBombs, candidates.Count);

        // Fisher–Yates shuffle first toPlace elements
        for (int i = 0; i < toPlace; i++)
        {
            int j = Random.Range(i, candidates.Count);
            var tmp = candidates[i];
            candidates[i] = candidates[j];
            candidates[j] = tmp;
        }

        for (int i = 0; i < toPlace; i++)
            candidates[i].AsignBomb();
    }

    /// <summary>After bombs are assigned, compute all numbers in one pass.</summary>
    public void CalculateAllNumbers()
    {
        foreach (var go in allTiles)
        {
            var t = go.GetComponent<GreenTile>();
            if (t != null)
                t.SetNumber();
        }
    }

    /// <summary>Helper to grab a tile by its grid coords.</summary>
    public GreenTile GetTile(int x, int y)
    {
        string name = $"GreenTile {x}{y}";
        var go = allTiles.Find(g => g.name == name);
        return go != null ? go.GetComponent<GreenTile>() : null;
    }
}
