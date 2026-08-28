using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;


public class CharacterMotor : CollisionCheck
{
    //Steering Behavior
    //注意这里DeltaVelocity只是针对Steering Behavior，技能对速度的影响应该不受这个控制
    [SerializeField] private MovementProfileSO movementProfile;

    //FixedUpdate中暂存的实际移动向量
    [SerializeField] private Vector3 tempMoveVec;
    //碰撞处理之前的移动距离和方向
    [SerializeField] private Vector3 finalMoveVec;

    //两个Velocity就是真实物理意义
    //internalVelocity管理所有人类输入导致的速度
    [SerializeField] private Vector3 internalVelocity;
    //externalVelocity管理所有非人类输入导致的速度
    public Vector3 externalVelocity { get; private set; }

    //debug
    public Vector3 ExternalVelocity;

    [SerializeField] private Vector3 direction;
    
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //debug
        ExternalVelocity = externalVelocity;
        //更新外部速度
        UpdateExternalVelocity();
        //处理最终速度
        finalMoveVec = CalculateFinalMoveVec();
        tempMoveVec = OnFinalVelocityInput(finalMoveVec);
        //移动
        rb.MovePosition(rb.position + tempMoveVec);
    }

    //Get
    public float GetAcceleration()
    {
        return movementProfile.acceleration;
    }

    public float GetMaxSpeed()
    {
        return movementProfile.maxSpeed;
    }
    public Vector3 GetMoveVec()
    {
        return tempMoveVec;
    }
    
    //如果想要分开处理的话，最好给到一个变量判断是否有外部速度
    public Vector3 OnFinalVelocityInput(Vector3 finalVelocity)
    {
        //为CapsuleCast做计算
        CalculateForCast();
        //环境碰撞检测，只在有速度的时候触发
        direction = CalculateDirection(finalVelocity);
        return CollisionSolver(direction, finalVelocity);
    }

    private Vector3 CalculateFinalMoveVec()
    {
        return (internalVelocity + externalVelocity) * Time.deltaTime;
    }
    //更新外部速度，会受到摩擦影响
    private void UpdateExternalVelocity()
    {
        if (!IsVelocityNegligible(externalVelocity))
        {
            //只对外部速度进行摩擦处理，内部速度不受摩擦影响
            externalVelocity *= Mathf.Exp(-movementProfile.unitFriction * Time.fixedDeltaTime);
        }
    }

    //使得internalVelocity在acceleration的限制下逼近Steering Behavior产生的desiredVelocity
    //AI用
    public void ApplyDesiredVelocityToInternalVelocity(Vector3 desiredVelocity)
    {
        if (desiredVelocity != Vector3.zero)
            internalVelocity = Vector3.MoveTowards(internalVelocity, desiredVelocity, movementProfile.acceleration * Time.fixedDeltaTime);
        else internalVelocity = Vector3.MoveTowards(internalVelocity, desiredVelocity, movementProfile.deceleration * Time.fixedDeltaTime);
    }

    //这里加入的action语义只是一个方向
    //玩家用
    public void PlayerApplyDesiredVelocityToInternalVelocity(Vector2 action)
    {
        Vector3 direction = new Vector3(action.x, 0, action.y).normalized;
        Vector3 desiredVelocity = direction * movementProfile.maxSpeed;
        if (desiredVelocity != Vector3.zero)
        {
            internalVelocity = Vector3.MoveTowards(internalVelocity, desiredVelocity, movementProfile.acceleration * Time.deltaTime);
        }
        else
        {
            internalVelocity = Vector3.MoveTowards(internalVelocity, desiredVelocity, movementProfile.deceleration * Time.deltaTime);
        }
    }
    //这里加入的velocity必须是真实物理意义
    public void AddExternalVelocity(Vector3 velocity)
    {
        externalVelocity += velocity;
        Debug.Log("CharacterMoter: Added externalVelocity");
    }

    //Reset
    private void ResetInternalVelocity()
    {
        internalVelocity = Vector3.zero;
    }

    private void ResetExternalVelocity()
    {
       externalVelocity = Vector3.zero;
    }

    //Set
    private void SetInternalVelocity(Vector3 velocity)
    {
        internalVelocity = velocity;
    }

    public void SetExternalVelocity(Vector3 velocity)
    {
        externalVelocity = velocity;
    }


}
