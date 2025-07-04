// GreenTile.cs
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenTile : MonoBehaviour
{
    [SerializeField] Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject highlight;
    [SerializeField] GameObject visuals;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject bomb;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] List<GameObject> neighbors;
    [SerializeField] string index;
    [SerializeField] bool hasBomb;
    [SerializeField] bool isFlagged;
    [SerializeField] int numberOfBombs;

    int gridX, gridY;
    bool mouseIsOn;
    bool hasNumber;
    bool isRevealed;

    GameSession gameSession;
    ColorManager colorManager;

    public Vector2Int GridPosition => new Vector2Int(gridX, gridY);
    public bool HasBomb => hasBomb;
    public bool HasNumber => hasNumber;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        colorManager = FindObjectOfType<ColorManager>();
    }

    /// <summary>Called by GridManager when instantiating.</summary>
    public void SetGridPosition(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public void SetColor(bool greenTileIsOffset)
    {
        spriteRenderer.color = greenTileIsOffset ? baseColor : offsetColor;
    }

    public void SetIndex(string newIndex)
    {
        index = newIndex;
    }

    /// <summary>Marks this tile as a bomb. Numbering happens later.</summary>
    public void AsignBomb()
    {
        hasBomb = true;
        bomb.SetActive(true);
    }

    void Start()
    {
        SetNeighbors();
    }

    /// <summary>Populates the 8‐neighbor list using the index string.</summary>
    void SetNeighbors()
    {
        int idx = int.Parse(index);
        int[] offsets = { -9, 1, 11, -10, 10, -11, -1, 9 };
        foreach (int o in offsets)
        {
            int nIdx = idx + o;
            string nStr = nIdx < 10 ? "0" + nIdx : nIdx.ToString();
            neighbors.Add(GameObject.Find($"GreenTile {nStr}"));
        }
    }

    /// <summary>Compute and show the adjacent‐bomb count.</summary>
    public void SetNumber()
    {
        if (hasBomb)
        {
            hasNumber = false;
            numberText.gameObject.SetActive(false);
            return;
        }

        int count = 0;
        foreach (var n in neighbors)
            if (n != null && n.GetComponent<GreenTile>().hasBomb)
                count++;

        if (count > 0)
        {
            hasNumber = true;
            numberText.text = count.ToString();
            numberText.gameObject.SetActive(true);
        }
        else
        {
            hasNumber = false;
            numberText.gameObject.SetActive(false);
        }

        numberOfBombs = count;

        SetNumberColor();
    }

    void SetNumberColor()
    {
        if (numberOfBombs == 0) return;
        numberText.color = colorManager.numberColors[numberOfBombs - 1];
    }

    void Update()
    {
        if (gameSession.GetGameOver())
        {
            highlight.SetActive(false);
            return;
        }

        if (mouseIsOn && Input.GetMouseButtonDown(0) && !isFlagged)
            gameSession.OnTileClicked(this);

        neighbors.RemoveAll(n => n == null);

        if (mouseIsOn && Input.GetMouseButtonDown(1) && !isRevealed)
        {
            ActivateFlag();
        }
    }

    void ActivateFlag()
    {
        if (isFlagged)
        {
            isFlagged = false;
            flag.SetActive(false);
        }
        else
        {
            isFlagged = true;
            flag.SetActive(true);
        }
    }

    public void Reveal()
    {
        visuals.SetActive(false);
        isRevealed = true;
    }

    void OnMouseEnter()
    {
        if (gameSession.GetGameOver())
            return;

        mouseIsOn = true;
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        if (gameSession.GetGameOver())
            return;

        mouseIsOn = false;
        highlight.SetActive(false);
    }

    public List<GameObject> GetNeighbors() => neighbors;
}
