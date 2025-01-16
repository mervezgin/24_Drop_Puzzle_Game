using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left,
    Right,
    Down,
    Up,
}
public class CreateShape : MonoBehaviour
{
    public static CreateShape instance;
    private List<Vector2> allTransformList = new List<Vector2> { new Vector2(0.01f, 0), new Vector2(0.47f, 0), new Vector2(0.01f, 0.46f), new Vector2(0.47f, 0.46f) };
    private Vector2[] allsizes = { new Vector2(1, 1), new Vector2(1, 2), new Vector2(2, 1), new Vector2(2, 2) };
    [SerializeField] List<Transform> allQuarterColorList;
    [SerializeField] private Transform fullQuarter;
    private bool[] occupiedTransforms;
    private bool isValidSize = false;
    private void Start()
    {
        occupiedTransforms = new bool[allTransformList.Count];
        CreateShapeLast();
    }
    public void CreateShapeLast()
    {
        for (int i = 0; i < allTransformList.Count; i++)
        {
            if (occupiedTransforms[i]) continue;
            if (allQuarterColorList.Count == 0) return;

            int randomIndex = Random.Range(0, allQuarterColorList.Count);
            Transform selectedQuarter = allQuarterColorList[randomIndex];
            allQuarterColorList.RemoveAt(randomIndex);

            Vector2 randomSize;
            do
            {
                randomSize = allsizes[Random.Range(0, allsizes.Length)];
                isValidSize = CheckIfSizeFits(i, randomSize);
            } while (!isValidSize);
            MarkOccupiedTransforms(i, randomSize);

            Transform fullShape = Instantiate(selectedQuarter, allTransformList[i], Quaternion.identity);
            fullShape.localScale = randomSize;
            foreach (Transform child in fullShape)
            {
                child.position = allTransformList[i];
            }
            if (fullQuarter != null) fullShape.SetParent(fullQuarter);
            fullShape.localPosition = allTransformList[i];
        }
    }
    private bool CheckIfSizeFits(int startIndex, Vector2 size)
    {
        int rows = Mathf.CeilToInt(size.x);
        int cols = Mathf.CeilToInt(size.y);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {

                int indexToCheck = startIndex + row * 2 + col; //allTransform verisine göre 2 sütun var dedim
                int rowcheck = startIndex + row;
                if (startIndex <= 1 && rowcheck > 1 || startIndex > 1 && rowcheck > 3) return false;
                if (startIndex >= 2 && col == 1) return false;
                if (indexToCheck >= allTransformList.Count || occupiedTransforms[indexToCheck]) return false;
            }
        }
        return true;
    }
    private void MarkOccupiedTransforms(int startIndex, Vector2 size)
    {
        int rows = Mathf.CeilToInt(size.y);
        int cols = Mathf.CeilToInt(size.x);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int indexToMark = startIndex + row * 2 + col;
                if (indexToMark <= allTransformList.Count)
                {
                    occupiedTransforms[indexToMark] = true;
                }
            }
        }
    }
    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                transform.Translate(Vector3.left, Space.World);
                break;
            case Direction.Right:
                transform.Translate(Vector3.right, Space.World);
                break;
            case Direction.Down:
                transform.Translate(Vector3.down, Space.World);
                break;
            case Direction.Up:
                transform.Translate(Vector3.up, Space.World);
                break;
        }
    }
}
