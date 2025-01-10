using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Down,
}
public class ShapeManager : MonoBehaviour
{
    private void Start()
    {
        Move(Direction.Down);
    }
    private void Move(Direction direction)
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
        }
    }
}
