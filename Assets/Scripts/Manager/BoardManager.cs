using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    [SerializeField] private Transform tilePrefab;
    public Transform[,] grid;
    private QuarterObjectSO[,] quarterObjectGrid;
    private QuarterObjectSO quarterObject;
    public int gridHeight = 6;
    public int gridWidth = 6;
    private void Awake()
    {
        instance = this;
        grid = new Transform[gridWidth, gridHeight];
        quarterObjectGrid = new QuarterObjectSO[gridWidth, gridHeight];
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
            QuarterObject quarterObject = child.GetComponent<QuarterObject>();
            if (quarterObject != null)
            {
                quarterObjectGrid[(int)pos.x, (int)pos.y] = quarterObject.GetQuarterObjectSO();
                //Debug.Log("hadi inş" + quarterObject.transform);
            }
        }
    }
    public bool IsGridFull(int x, int y, CreateShape shape)
    {
        return grid[x, y] != null && grid[x, y].parent != shape.transform;
    }
    public void CheckMatchingColors(CreateShape shape)
    {
        foreach (Transform child in shape.transform)
        {
            QuarterObject quarterObject = child.GetComponent<QuarterObject>();
            if (quarterObject != null && quarterObject.GetQuarterObjectSO() != null)
            {
                Debug.Log($"Quarter Color: {quarterObject.GetQuarterObjectSO().quarterColor}");
            }
        }
        // for (int y = 0; y < gridHeight; y++)
        // {
        //     for (int x = 0; x < gridWidth; x++)
        //     {
        //         if (quarterObjectGrid[x, y] != null)
        //         {
        //             Debug.Log("burası doğru mu " + quarterObjectGrid[x, y]);
        //             CheckAdjacentColors(x, y);
        //         }
        //     }
        // }
    }
    private void CheckAdjacentColors(int x, int y)
    {
        QuarterObjectSO current = quarterObjectGrid[x, y];
        if (current == null) return;

        // Sağ ve aşağı hücreleri kontrol et
        if (x < gridWidth - 1 && quarterObjectGrid[x + 1, y] == current)
        {
            Debug.Log($"Match found at ({x}, {y}) and ({x + 1}, {y}) - Color: {current.quarterColor}");
        }
        if (y < gridHeight - 1 && quarterObjectGrid[x, y + 1] == current)
        {
            Debug.Log($"Match found at ({x}, {y}) and ({x}, {y + 1}) - Color: {current.quarterColor}");
        }
    }
}
