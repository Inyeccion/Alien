using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private MovementStrategySO movementStrategy;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetPos;


    public void SetTarget(Transform nodeTarget)
    {
        target = nodeTarget;
    }
}
