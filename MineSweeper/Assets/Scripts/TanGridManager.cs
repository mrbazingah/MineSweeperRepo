using UnityEngine;

public class TanGridManager : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] TanTile TanTilePrefab;
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
                var spawnTanTile = Instantiate(TanTilePrefab, new Vector3(x, y, 0.1f), Quaternion.identity);
                spawnTanTile.name = $"TanTile {x}{y}";

                var is0ffset = (x % 2 != 0 && y % 2 == 0) || (x % 2 == 0 && y % 2 != 0);
                spawnTanTile.SetColor(is0ffset);

                spawnTanTile.transform.SetParent(parent.transform);
            }
        }
    }
}
