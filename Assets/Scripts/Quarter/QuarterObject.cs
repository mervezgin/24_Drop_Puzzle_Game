using UnityEngine;

public class QuarterObject : MonoBehaviour
{
    [SerializeField] private QuarterObjectSO quarterObjectSO;
    public QuarterObjectSO GetQuarterObjectSO()
    {
        return quarterObjectSO;
    }
}
