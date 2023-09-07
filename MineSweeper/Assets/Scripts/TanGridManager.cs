using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanGridManager : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] TanTile TanTilePrefab;

    void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnTanTile = Instantiate(TanTilePrefab, new Vector3(x, y, 0.1f), Quaternion.identity);
                spawnTanTile.name = $"TanTile {x}{y}";

                var is0ffset = (x % 2 != 0 && y % 2 == 0) || (x % 2 == 0 && y % 2 != 0);
                spawnTanTile.Init(is0ffset);
            }
        }
    }
}
