using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TanTile : MonoBehaviour
{
    [SerializeField] Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI numberText;

    [SerializeField] int numberOfBombs;

    BoxCollider2D myBoxcollider;

    void Start()
    {
        myBoxcollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        CheckForBombs();
    }

    void CheckForBombs()
    {
        
    }

    public void Init(bool greenTileIsOffset)
    {
        spriteRenderer.color = greenTileIsOffset ? baseColor : offsetColor;
    }
}
