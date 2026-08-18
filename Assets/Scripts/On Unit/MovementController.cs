using UnityEngine;
using System.Collections.Generic;
public class MovementController : MonoBehaviour
{
    [SerializeField] private List<MovementStrategySO> movementStrategies;

    [SerializeField] private SteeringContext steeringContext;

    [SerializeField] private MoveIntent moveIntent;

    private void Awake()
    {
        InitializeSteeringContext();
    }

    private void FixedUpdate()
    {
        
    }

    private void InitializeSteeringContext()
    {
        //初始化steering上下文
        steeringContext = new SteeringContext();
    }

    private void SetMoveIntent()
    {
        moveIntent = new MoveIntent(steeringContext.movementType, steeringContext.transform);
    }
}
