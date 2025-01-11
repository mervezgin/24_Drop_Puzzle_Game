using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;
    [SerializeField] private ShapeManager[] allShapes;
    private void Awake()
    {
        instance = this;
    }
    public ShapeManager SetShape()
    {
        int randomShape = Random.Range(0, allShapes.Length);
        ShapeManager shape = Instantiate(allShapes[randomShape], transform.position, Quaternion.identity);
        return shape;
    }
    /*public ShapeManager SetShapeInBoard()
    {
        //oyun başladığı zaman gridde bazı yerlerde şekiller olsun, level arttıkça şekil sayısı artsın
        int randomShape = Random.Range(0, allShapes.Length);
        ShapeManager shape = Instantiate(allShapes[randomShape], buraya gridden random pozisyon gelecek, Quaternion.identity);
        return shape;
    }*/
}
