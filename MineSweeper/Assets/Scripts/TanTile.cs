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

    void CheckForBombs()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard"))
            || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
    }

    public void Init(bool greenTileIsOffset)
    {
        spriteRenderer.color = greenTileIsOffset ? baseColor : offsetColor;
    }
}
