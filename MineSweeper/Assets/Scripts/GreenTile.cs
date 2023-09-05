using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTile : MonoBehaviour
{
    [SerializeField] Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject highlight;

    [SerializeField] GameObject bombPrefab;
    [SerializeField] float randomFactor;

    bool hasBomb;

    void Start()
    {
        int bombCanSpawn = (int)Random.Range(0, randomFactor);
        if (bombCanSpawn == 0)
        {
            Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y, 0.5f), Quaternion.identity);
            hasBomb = true;
        }
    }

    public void Init(bool greenTileIsOffset)
    {
        spriteRenderer.color = greenTileIsOffset ? baseColor : offsetColor;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        Destroy(gameObject);

        if (hasBomb)
        {
            //TODO
            //Make a game over screen

            Debug.Log("You Lost");
        }
    }
}
