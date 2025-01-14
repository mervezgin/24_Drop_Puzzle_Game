using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;
    [SerializeField] private CreateShape shape;
    private void Awake()
    {
        instance = this;
    }
    public CreateShape SpawnShape()
    {
        CreateShape spawnShape = Instantiate(shape, transform.position, Quaternion.identity);
        Debug.Log(transform.position);
        return spawnShape;
    }
    public CreateShape SpawnShapeInGrid(Vector2 pos)
    {
        CreateShape spawnShapes = Instantiate(shape, pos, Quaternion.identity);
        return spawnShapes;
    }
}
