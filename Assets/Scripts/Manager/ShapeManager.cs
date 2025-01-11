using System.Collections;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Down,
    Up,
}
public class ShapeManager : MonoBehaviour
{
    public static ShapeManager instance;
    private void Start()
    {
        //StartCoroutine(MoveRoutine());
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
    /*IEnumerator MoveRoutine()
    {
        while (true)
        {
            Move(Direction.Down);
            yield return new WaitForSeconds(0.25f);
        }
    }*/
}
