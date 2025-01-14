using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private float minInputDistance;
    private CreateShape activeShape;
    public CreateShape ActiveShape { get => activeShape; set => activeShape = value; }

    private Vector2 initialMousePosition;
    private Vector2 currentMousePosition;
    public Vector2 CurrentMousePosition => currentMousePosition;

    private bool isActive = true;
    public bool IsActive { get => isActive; set => isActive = value; }


    private void Update()
    {
        if (isActive && GameManager.instance.IsGameActive)
            GetInput();
    }
    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(currentMousePosition.x - initialMousePosition.x) > minInputDistance)
            {
                isActive = true;
                SetActiveShapePosition();
            }
        }
    }

    private void SetActiveShapePosition()
    {

    }

    private void DeactvateInput()
    {
        isActive = false;
    }
}
