using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;


public class CharacterMotor : CollisionCheck, IPossessable
{
    //Steering Behavior
    //注意这里DeltaVelocity只是针对Steering Behavior，技能对速度的影响应该不受这个控制
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxDeltaVelocity;

    //FixedUpdate中暂存实际移动向量
    [SerializeField] private Vector3 tempMovePos;
    //两个Velocity就是真实物理意义
    //internalVelocity管理所有人类输入导致的速度
    [SerializeField] private Vector3 internalVelocity;
    //externalVelocity管理所有非人类输入导致的速度
    public Vector3 externalVelocity { get; private set; }
    //debug
    public Vector3 ExternalVelocity;

    [SerializeField] private Vector3 direction;
    [SerializeField] private float unitFriction = 1f;
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
        tempMovePos = OnFinalVelocityInput(CalculateFinalVelocity());
        //移动
        rb.MovePosition(rb.position + tempMovePos);
        //重置内部速度
        ResetInternalVelocity();
    }

    //初始化逻辑外层可以放在这里？
    public void OnEnterHost()
    {
        hostManager.SetHost(this);
        AIController AIController = gameObject.GetComponent<AIController>();
        if (AIController is not null)
        {
            AIController.ShutDown();
        }
    }


    //退出寄生
    public void OnExitHost()
    {

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

    private Vector3 CalculateFinalVelocity()
    {
        return (internalVelocity + externalVelocity) * Time.deltaTime;
    }
    //更新外部速度，会受到摩擦影响
    private void UpdateExternalVelocity()
    {
        if (!IsVelocityNegligible(externalVelocity))
        {
            //只对外部速度进行摩擦处理，内部速度不受摩擦影响
            externalVelocity *= Mathf.Exp(-unitFriction * Time.fixedDeltaTime);
        }
    }

    //使得externalVelocity在maxDeltaVelocity的限制下逼近Steering Behavior产生的desiredVelocity
    public void ApplyDesiredVelocityToExternalVelocity(Vector3 desiredVelocity)
    {
        Vector3 deltaVelocity =
            desiredVelocity - externalVelocity;

        deltaVelocity =
            Vector3.ClampMagnitude(
                deltaVelocity,
                maxDeltaVelocity
            );

        externalVelocity += deltaVelocity;
    }

    //这里加入的velocity必须是真实物理意义
    public void AddExternalVelocity(Vector3 velocity)
    {
        externalVelocity += velocity;
        Debug.Log("CharacterMoter: Added externalVelocity");
    }
    //这里加入的action语义只是一个方向
    public void AddInternalVelocity(Vector2 action)
    {
        Vector3 direction = new Vector3(action.x, 0, action.y).normalized;
        internalVelocity = direction * speed;
        Debug.Log("CharacterMoter: Added internalVelocity");
    }

    private void ResetInternalVelocity()
    {
        internalVelocity = Vector3.zero;
    }

    private void ResetExternalVelocity()
    {
       externalVelocity = Vector3.zero;
    }

    private void SetInternalVelocity(Vector3 velocity)
    {
        internalVelocity = velocity;
    }

    public void SetExternalVelocity(Vector3 velocity)
    {
        externalVelocity = velocity;
    }


}
