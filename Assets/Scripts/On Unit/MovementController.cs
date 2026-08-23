using UnityEngine;
using System.Collections.Generic;
public class MovementController : MonoBehaviour
{
    [SerializeField] private List<MovementStrategySO> movementStrategies;

    public SteeringContext steeringContext;

    private CharacterMotor characterMotor;

    [SerializeField] private MovementType moveIntent;

    private void Awake()
    {
        InitializeSteeringContext();
    }

    private void Start()
    {
        characterMotor = GetComponent<CharacterMotor>();
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
    public void SetMoveIntent(MovementType movementType)
    {
        moveIntent = movementType;
    }

    //应该要有多个重载
    //ChaseNode
    public void SetSteeringContext(Transform target)
    {
        steeringContext.targetTransform = target;
    }

    //更新steeringBehavior需要的对应信息
    private void UpdateSteeringContext()
    {
        if (steeringContext.targetTransform != null)
        {
            steeringContext.targetPos = steeringContext.targetTransform.position;
            steeringContext.targetVelocity = steeringContext.targetTransform.GetComponent<CharacterMotor>().GetMoveVec();
        }

        steeringContext.currentPos = transform.position;

        if (characterMotor != null)
        {
            steeringContext.currentVelocity = characterMotor.GetMoveVec();
        }


    }

    //更换意图之后，重置上下文
    public void ResetSteeringContext()
    {
        steeringContext.ResetContext();
    }
}
