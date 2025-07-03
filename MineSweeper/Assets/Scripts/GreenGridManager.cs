using UnityEngine;

public class GreenGridManager : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GreenTile greenTilePrefab;
    [SerializeField] Transform camPos;
    [SerializeField] GameObject parent;

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

                string tileIndex = $"{x}{y}";
                spawnGreenTile.SetIndex(tileIndex);

                var is0ffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnGreenTile.SetColor(is0ffset);

                spawnGreenTile.transform.SetParent(parent.transform);
            }
        }

        camPos.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
