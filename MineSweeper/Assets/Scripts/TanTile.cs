using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TanTile : MonoBehaviour
{
    [SerializeField] Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] float bombCheckRadius;

    int numberOfBombs;

    void Start()
    {
        CheckForBombs();
    }

    void CheckForBombs()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, bombCheckRadius); ;

        foreach (Collider2D bombsNearby in collisions)
        {
            if (bombsNearby.gameObject.CompareTag("Bomb"))
            {
                numberOfBombs++;
                numberText.text = numberOfBombs.ToString();
            }
        }

      
    }

    public void Init(bool tanTileOffset)
    {
        spriteRenderer.color = tanTileOffset ? baseColor : offsetColor;
    }
}
