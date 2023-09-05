using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGridManager : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GreenTile greenTilePrefab;
    [SerializeField] Transform camPos;

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
                var spawnGreenTile = Instantiate(greenTilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnGreenTile.name = $"GreenTile {x}{y}";

                var is0ffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnGreenTile.Init(is0ffset);
            }
        }

        camPos.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
