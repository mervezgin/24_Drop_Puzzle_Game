using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    [SerializeField] private Transform tilePrefab;
    private Transform[,] grid;
    private int height = 12;
    private int width = 12;
    private void Awake()
    {
        instance = this;
        grid = new Transform[width, height];
    }
    private void Start()
    {
        SetGameGrid();
    }
    private void SetGameGrid()
    {
        for (int y = 0; y < height; y += 2)
        {
            for (int x = 0; x < width; x += 2)
            {
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.parent = this.transform;
            }
        }
    }
    private bool CheckToShapesInBoard(int x, int y)
    {
        return x >= 0 && x < width && y >= 0;
    }
    public bool ShapesAreValidPosition(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = GameManager.instance.VectorToInt(child.position);
            if (!CheckToShapesInBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }
        }
        return true;
    }
}
