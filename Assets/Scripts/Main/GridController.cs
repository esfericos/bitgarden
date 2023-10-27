using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using TMPro;

public class GridController : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tilemap pathMap = null;
    [SerializeField] private Tile hoverTile = null;
    [SerializeField] private Tile selectedTile = null;

    [SerializeField] private TMP_Text tileCoordsText;

    private Vector3Int previousMousePos = new Vector3Int();
    private Vector3Int previousClickedPos = new Vector3Int();

    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        if (Input.mousePosition.x >= 1070) return;

        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            if (previousClickedPos == mousePos) return;
            if(previousClickedPos != previousMousePos)
             interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
           
             interactiveMap.SetTile(mousePos, hoverTile);
             previousMousePos = mousePos;
        }

        // Left mouse click 
        if (Input.GetMouseButton(0)){
            tileCoordsText.text = mousePos.x + "," + mousePos.y;
            interactiveMap.SetTile(previousClickedPos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, selectedTile);
            previousClickedPos = mousePos;
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
