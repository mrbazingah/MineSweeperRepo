using TMPro;
using UnityEngine;

public class TanTile : MonoBehaviour
{
    [SerializeField] Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
  
    public void SetColor(bool tanTileOffset)
    {
        spriteRenderer.color = tanTileOffset ? baseColor : offsetColor;
    }
}
