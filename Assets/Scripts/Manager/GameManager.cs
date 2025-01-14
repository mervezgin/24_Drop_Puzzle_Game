using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private CreateShape onScreenShape;
    private CreateShape onBoardShapes;
    private float moveDownTimeForShapes = 0.25f;
    private float moveDownCountdown;
    private float moveDownButtonPressCountdown;
    private float moveDownButtonPressTime = 0.1f;
    private float moveRightLeftButtonPressTime = 0.1f;
    private float moveLeftRightCountdown;
    private bool isGameActive = true;
    public bool IsGameActive => isGameActive;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (onScreenShape == null)
        {
            onScreenShape = SpawnerManager.instance.SpawnShape();
            onScreenShape.transform.position = VectorToInt(onScreenShape.transform.position);
        }
        if (onBoardShapes == null)
        {
            for (int x = 0; x <= 10; x += 2)
            {
                for (int y = 0; y <= 2; y += 2)
                {
                    Vector2 pos = new Vector2(x, y);
                    onBoardShapes = SpawnerManager.instance.SpawnShapeInGrid(pos);
                    onBoardShapes.transform.position = VectorToInt(pos);
                    BoardManager.instance.SetShapeInToGrid(onBoardShapes);
                }
            }
        }
    }
    private void Update()
    {
        if (!onScreenShape) return;
        HandleInput();
    }
    private void HandleInput()
    {
        if ((Input.GetKey(KeyCode.RightArrow) && Time.time > moveLeftRightCountdown) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            onScreenShape.Move(Direction.Right);
            moveLeftRightCountdown = Time.time + moveRightLeftButtonPressTime;
            if (!BoardManager.instance.ShapesAreValidPosition(onScreenShape))
            {
                onScreenShape.Move(Direction.Left);
            }
        }
        if ((Input.GetKey(KeyCode.LeftArrow) && Time.time > moveLeftRightCountdown) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            onScreenShape.Move(Direction.Left);
            moveLeftRightCountdown = Time.time + moveRightLeftButtonPressTime;
            if (!BoardManager.instance.ShapesAreValidPosition(onScreenShape))
            {
                onScreenShape.Move(Direction.Right);
            }
        }
        if ((Input.GetKey(KeyCode.DownArrow) && Time.time > moveDownButtonPressCountdown) ||
        Time.time > moveDownCountdown)
        {
            moveDownButtonPressCountdown = Time.time + moveDownButtonPressTime;
            moveDownCountdown = Time.time + moveDownTimeForShapes;
            if (onScreenShape)
            {
                onScreenShape.Move(Direction.Down);
                if (!BoardManager.instance.ShapesAreValidPosition(onScreenShape))
                {
                    ShapeIsSet();
                }
            }
        }
    }
    public Vector2 VectorToInt(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
    private void ShapeIsSet()
    {
        moveRightLeftButtonPressTime = Time.time;
        moveDownButtonPressCountdown = Time.time;

        onScreenShape.Move(Direction.Up);
        BoardManager.instance.SetShapeInToGrid(onScreenShape);
        onScreenShape = SpawnerManager.instance.SpawnShape();
    }
}

