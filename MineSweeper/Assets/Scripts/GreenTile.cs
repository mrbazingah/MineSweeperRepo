using System.Collections.Generic;
using UnityEngine;

public class GreenTile : MonoBehaviour
{
    [SerializeField] Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject highlight;
    [SerializeField] List<GameObject> neighbors;
    [SerializeField] string index;

    bool mouseIsOn;

    void Start()
    {
        SetNeighbors();
    }

    void SetNeighbors()
    {
        int firstIndex = int.Parse(index) - 9;
        string firstIndexString = firstIndex < 10 ? "0" + firstIndex.ToString() : firstIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {firstIndexString}"));

        int secondIndex = int.Parse(index) + 1;
        string secondIndexString = secondIndex < 10 ? "0" + secondIndex.ToString() : secondIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {secondIndexString}"));

        int thirdIndex = int.Parse(index) + 11;
        string thirdIndexString = thirdIndex < 10 ? "0" + thirdIndex.ToString() : thirdIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {thirdIndexString}"));

        int forthIndex = int.Parse(index) - 10;
        string forthIndexIndexString = forthIndex < 10 ? "0" + forthIndex.ToString() : forthIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {forthIndexIndexString}"));

        int fifthIndex = int.Parse(index) + 10;
        string fifthIndexString = fifthIndex < 10 ? "0" + fifthIndex.ToString() : fifthIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {fifthIndexString}"));

        int sixthIndex = int.Parse(index) - 11;
        string sixthIndexIndexString = sixthIndex < 10 ? "0" + sixthIndex.ToString() : sixthIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {sixthIndexIndexString}"));

        int seventhIndex = int.Parse(index) - 1;
        string seventhIndexIndexString = seventhIndex < 10 ? "0" + seventhIndex.ToString() : seventhIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {seventhIndexIndexString}"));

        int eigthIndex = int.Parse(index) + 9;
        string eigthIndexIndexString = eigthIndex < 10 ? "0" + eigthIndex.ToString() : eigthIndex.ToString();
        neighbors.Add(GameObject.Find($"GreenTile {eigthIndexIndexString}"));
    }

    void Update()
    {
        if (mouseIsOn && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i] == null)
            {
                neighbors.RemoveAt(i);
            }
        }
    }

    void OnMouseEnter()
    {
        mouseIsOn = true;
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        mouseIsOn = false;
        highlight.SetActive(false);
    }

    public void SetColor(bool greenTileIsOffset)
    {
        spriteRenderer.color = greenTileIsOffset ? baseColor : offsetColor;
    }

    public void SetIndex(string newIndex)
    {
        index = newIndex;
    }

    public List<GameObject> GetNeighbors()
    {
        return neighbors;
    }
}
