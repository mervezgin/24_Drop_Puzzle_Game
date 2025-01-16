using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    [SerializeField] private Transform tilePrefab;
    public Transform[,] grid;
    public int gridHeight = 6;
    public int gridWidth = 6;
    private void Awake()
    {
        instance = this;
        grid = new Transform[gridWidth, gridHeight];
    }
    private void Start()
    {
        SetGameGrid();
    }
    private void SetGameGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.parent = this.transform;
            }
        }
    }
    public bool CheckToShapesInGrid(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0;
    }
    public bool ShapesAreValidPosition(CreateShape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = GameManager.instance.VectorToInt(child.position);
            if (!CheckToShapesInGrid((int)pos.x, (int)pos.y))
            {
                return false;
            }
            if (pos.y < gridHeight)
            {
                if (IsGridFull((int)pos.x, (int)pos.y, shape))
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void SetShapeInToGrid(CreateShape shape)
    {
        if (shape == null) return;
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = GameManager.instance.VectorToInt(child.position);
            grid[(int)pos.x, (int)pos.y] = child;
        }
    }
    public bool IsGridFull(int x, int y, CreateShape shape)
    {
        Debug.Log(shape + " " + shape.transform);
        return grid[x, y] != null && grid[x, y].parent != shape.transform;
    }
}
