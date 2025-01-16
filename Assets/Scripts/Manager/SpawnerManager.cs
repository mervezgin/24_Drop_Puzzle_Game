using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;
    [SerializeField] private CreateShape shape;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public CreateShape SpawnShape()
    {
        Vector3 spawnPosition = transform.position;
        CreateShape spawnShape = Instantiate(shape, spawnPosition, Quaternion.identity);
        return spawnShape;
    }
    public CreateShape SpawnShapeInGrid(Vector2 pos)
    {
        CreateShape spawnShapes = Instantiate(shape, pos, Quaternion.identity);
        return spawnShapes;
    }
}
