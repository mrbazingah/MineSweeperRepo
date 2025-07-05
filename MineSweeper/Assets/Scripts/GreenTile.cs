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

    List<GreenTile> chordHighlightedTiles = new List<GreenTile>();

    GameSession gameSession;
    ColorManager colorManager;
    GreenGridManager gridManager;

    public Vector2Int GridPosition => new Vector2Int(gridX, gridY);
    public bool HasBomb => hasBomb;
    public bool HasNumber => hasNumber;

    public bool IsFlagged() => isFlagged;
    public bool IsRevealed() => isRevealed;
    public int GetBombCount() => numberOfBombs;
    public List<GameObject> GetNeighbors() => neighbors;

    public void ShowHighlight(bool on) => highlight.SetActive(on);

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        colorManager = FindObjectOfType<ColorManager>();
        gridManager = FindObjectOfType<GreenGridManager>();
    }

    public void SetGridPosition(int x, int y) { gridX = x; gridY = y; }
    public void SetColor(bool greenTileIsOffset) => spriteRenderer.color = greenTileIsOffset ? baseColor : offsetColor;
    public void SetIndex(string newIndex) => index = newIndex;
    public void AsignBomb() { hasBomb = true; bomb.SetActive(true); }

    void Start() => SetNeighbors();

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

    public void CalculateBomb()
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

        hasNumber = count > 0;
        numberText.gameObject.SetActive(hasNumber);
        if (hasNumber) numberText.text = count.ToString();
        numberOfBombs = count;

        if (count > 0)
            numberText.color = colorManager.numberColors[count - 1];
    }

    void Update()
    {
        if (gameSession.GetGameOver())
        {
            highlight.SetActive(false);
            ClearChordHighlights();
            return;
        }

        bool chordHeld = mouseIsOn && IsRevealed() && HasNumber && Input.GetMouseButton(0) && Input.GetMouseButton(1);
        if (chordHeld)
        {
            int flagCount = 0;
            foreach (var go in neighbors)
            {
                var n = go.GetComponent<GreenTile>();
                if (n != null && n.IsFlagged()) flagCount++;
            }

            if (flagCount < numberOfBombs)
            {
                foreach (var go in neighbors)
                {
                    var n = go.GetComponent<GreenTile>();
                    if (n != null && !n.IsRevealed() && !n.IsFlagged() && !chordHighlightedTiles.Contains(n))
                    {
                        n.ShowHighlight(true);
                        chordHighlightedTiles.Add(n);
                    }
                }
            }
            else
            {
                ClearChordHighlights();
            }
        }
        else
        {
            ClearChordHighlights();
        }

        if (mouseIsOn && IsRevealed() && HasNumber &&
            ((Input.GetMouseButtonDown(0) && Input.GetMouseButton(1)) ||
             (Input.GetMouseButtonDown(1) && Input.GetMouseButton(0))))
        {
            gameSession.OnChord(this);
            return;
        }

        if (mouseIsOn && Input.GetMouseButtonDown(0) && !isFlagged)
        {
            gameSession.OnTileClicked(this);
            return;
        }

        if (mouseIsOn && Input.GetMouseButtonDown(1) && !IsRevealed())
        {
            ActivateFlag();
        }

        neighbors.RemoveAll(n => n == null);
    }

    void ClearChordHighlights()
    {
        foreach (var n in chordHighlightedTiles)
            n.ShowHighlight(false);
        chordHighlightedTiles.Clear();
    }

    void ActivateFlag()
    {
        isFlagged = !isFlagged;
        flag.SetActive(isFlagged);
    }

    public void Reveal()
    {
        visuals.SetActive(false);
        isRevealed = true;

        gridManager.ProcessTiles();
    }

    void OnMouseEnter()
    {
        if (gameSession.GetGameOver()) return;
        mouseIsOn = true;
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        if (gameSession.GetGameOver()) return;
        mouseIsOn = false;
        highlight.SetActive(false);
    }
}
