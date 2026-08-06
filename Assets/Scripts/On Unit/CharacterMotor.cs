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
        UpdateExternalVelocity();
        OnFinalVelocityInput(CalculateFinalVelocity());
        ResetInternalVelocity();
    }

    //初始化逻辑外层可以放在这里？
    public void OnEnterHost()
    {
        hostManager.SetHost(this);
    }


    //最好将主角的技能逻辑放一部分进去？
    public void OnExitHost() { }  
    

    public void OnFinalVelocityInput(Vector3 finalVelocity)
    {
        //有移动
        if (!IsVelocityNegligible(finalVelocity))
        {
            CalculateForCast();

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
        return velocity.magnitude < 0.01f;
    }

    private void ResetInternalVelocity()
    {
        internalVelocity = Vector3.zero;
    }

}
