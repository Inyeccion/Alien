using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;


public class CharacterMotor : MoveMent,IPossessable
{
    [SerializeField] private Vector3 internalVelocity;
    [SerializeField] private Vector3 externalVelocity;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float unitFriction = 0.1f;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //更新外部速度
        UpdateExternalVelocity();
        //处理最终速度
        OnFinalVelocityInput(CalculateFinalVelocity());
        //重置内部速度
        ResetInternalVelocity();
    }

    //初始化逻辑外层可以放在这里？
    public void OnEnterHost()
    {
        hostManager.SetHost(this);
    }


    //最好将主角的技能逻辑放一部分进去？
    public void OnExitHost() { }  
    
    //如果想要分开处理的话，最好给到一个变量判断是否有外部速度
    public void OnFinalVelocityInput(Vector3 finalVelocity)
    {
        //有移动
        if (!IsVelocityNegligible(finalVelocity))
        {
            //为CapsuleCast做计算
            CalculateForCast();
            //环境碰撞检测，只在有速度的时候触发
            CalculateDirection(finalVelocity);
            EnvironmentCollisionCheck(direction, finalVelocity);

        }
        else Debug.Log("CharacterMotor: FinalVelocity IsNegligible.");
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

    public void AddExternalVelocity(Vector3 velocity)
    {
        externalVelocity += velocity;
    }

    public void AddInternalVelocity(Vector2 action)
    {
        Vector3 direction = new Vector3(action.x, 0, action.y).normalized;
        internalVelocity = direction * speed;
    }

    private bool IsVelocityNegligible(Vector3 velocity)
    {
        return velocity.magnitude < 0;
    }

    private void ResetInternalVelocity()
    {
        internalVelocity = Vector3.zero;
    }

    private void ResetExternalVelocity()
    {
       externalVelocity = Vector3.zero;
    }

    private void CalculateDirection(Vector3 finalVelocity)
    {
        direction = finalVelocity.normalized;
    }

}
