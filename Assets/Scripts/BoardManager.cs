using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    private int height = 12;
    private int width = 12;
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
}
