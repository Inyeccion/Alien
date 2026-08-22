using UnityEngine;
using System.Collections.Generic;
public class MovementController : MonoBehaviour
{
    [SerializeField] private List<MovementStrategySO> movementStrategies;

    public SteeringContext steeringContext;

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

    //给结点使用
    public void SetMoveIntent(MovementType movementType, Transform transform)
    {
        moveIntent = new MoveIntent(movementType, transform);
    }

    private void UpdateSteeringContext()
    {



    }
}
