using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ShapeManager onScreenShape;
    //private ShapeManager[] onBoardShapes;
    private float moveTimeForShapes = 0.5f;
    private float moveCountdown;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (onScreenShape == null)
        {
            onScreenShape = SpawnerManager.instance.SetShape();
            onScreenShape.transform.position = VectorToInt(onScreenShape.transform.position);
        }
        /*if (onBoardShapes == null)
        {//boardda oluşacak şekillerin yeri için method yaz
            onBoardShapes = SpawnerManager.instance.SetShapeInBoard();
            onBoardShapes.transform.position = VectorToInt(onBoardShapes.transform.position);
        }*/
    }
    private void Update()
    {
        if (Time.time > moveCountdown)
        {
            moveCountdown = Time.time + moveTimeForShapes;
            if (onScreenShape)
            {
                onScreenShape.Move(Direction.Down);
                if (!BoardManager.instance.ShapesAreValidPosition(onScreenShape))
                {
                    onScreenShape.Move(Direction.Up);
                    onScreenShape = SpawnerManager.instance.SetShape();
                }
            }
        }
    }
    public Vector2 VectorToInt(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}

