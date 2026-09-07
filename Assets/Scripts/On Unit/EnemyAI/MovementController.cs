using UnityEngine;
using System.Collections.Generic;
public class MovementController : MonoBehaviour
{
    [SerializeField] private List<MovementStrategySO> movementStrategies;
    [SerializeField] private List<MovementStrategySO> runtimeMovementStrategies;
    private int currentMovementStrategyInd = -1;

    public SteeringContext steeringContext;

    private CharacterMotor characterMotor;

    private Vector3 primaryDesiredVelocity;
    private Vector3 auxiliaryDesiredVelocity;

    //不直接操作moveIntent
    [SerializeField] private MovementType moveIntent;
    private MovementType myMoveIntent
    {
        get { return moveIntent; }

        set
        {
            if (moveIntent == value) return;
            moveIntent = value;
            ResetSteeringContext();
            Debug.Log("MovementController: ResetSteeringContext");
            SetCurrentStrategyInd();
        }
    }


    private void Awake()
    {
        InitializeSteeringContext();
        InitializeRuntimeMovementStrategies();
    }

    private void OnEnable()
    {
        ResetSteeringContext();
        ResetMoveIntent();
        ResetStrategyInd();
    }

    private void Start()
    {
        characterMotor = GetComponent<CharacterMotor>();
    }

    private void FixedUpdate()
    {
        ResetVelocities();
        UpdateSteeringContext();
        CalculatePrimaryDesiredVelocity();
        UpdatePrimaryDesiredVelocityToContext();
        CalculateAuxiliaryDesiredVelocity();
        characterMotor.ApplyDesiredVelocityToInternalVelocity(CalculateFinalDesiredVelocity());
    }

    private bool IsCurrentMovementStrategyIndInRange()
    {
        if (currentMovementStrategyInd < 0) return false;
        if (currentMovementStrategyInd > runtimeMovementStrategies.Count - 1) return false;
        return true;
    }

    private void SetCurrentStrategyInd()
    {
        for (int i = 0; i < runtimeMovementStrategies.Count; i++)
        {
            if (runtimeMovementStrategies[i].movementType == moveIntent)
            {
                currentMovementStrategyInd = i;
                return;
            }
        }
        Debug.LogWarning("MovementController: moveIntent and MoveStrategy miss match");
    }

    private void ResetVelocities()
    {
        primaryDesiredVelocity = Vector3.zero;
        auxiliaryDesiredVelocity = Vector3.zero;
    }

    private Vector3 CalculateFinalDesiredVelocity()
    {
        Vector3 temp = primaryDesiredVelocity + auxiliaryDesiredVelocity;
        return Vector3.ClampMagnitude(temp, steeringContext.maxSpeed);
    }

    private void CalculatePrimaryDesiredVelocity()
    {
        if (IsCurrentMovementStrategyIndInRange())
            primaryDesiredVelocity = runtimeMovementStrategies[currentMovementStrategyInd].primaryBehavior.Calculate(steeringContext).desiredVelocity;
    }

    private void CalculateAuxiliaryDesiredVelocity()
    {
        auxiliaryDesiredVelocity = Vector3.zero;
        if (IsCurrentMovementStrategyIndInRange())
            foreach (var auxiliaryBehavior in runtimeMovementStrategies[currentMovementStrategyInd].auxiliaryBehaviors)
            {
                auxiliaryDesiredVelocity += auxiliaryBehavior.Calculate(steeringContext).desiredVelocity;
            }
    }

    private void InitializeSteeringContext()
    {
        //初始化steering上下文
        steeringContext = new SteeringContext(transform);
    }

    private void InitializeRuntimeMovementStrategies()
    {
        foreach (MovementStrategySO strategy in movementStrategies)
        {
            runtimeMovementStrategies.Add(strategy.Instantiate());
        }
    }

    //给结点使用
    public void SetMoveIntent(MovementType movementType)
    {
        myMoveIntent = movementType;
    }

    //应该要有多个重载
    //ChaseNode
    public void SetSteeringContextTarget(Transform target)
    {
        steeringContext.targetTransform = target;
    }
    //FleeNode
    public void SetSteeringContextThreat(Transform threat)
    {
        steeringContext.targetTransform = threat;
    }

    //更新steeringBehavior需要的对应信息
    private void UpdateSteeringContext()
    {
        if (steeringContext.targetTransform != null)
        {
            steeringContext.targetPos = steeringContext.targetTransform.position;
            steeringContext.targetMoveVec = steeringContext.targetTransform.GetComponent<CharacterMotor>().GetMoveVec();
        }

        steeringContext.currentPos = transform.position;

        if (characterMotor != null)
        {
            steeringContext.currentMoveVec = characterMotor.GetMoveVec();
            steeringContext.currentCharacterMotor = characterMotor;

            steeringContext.maxSpeed = characterMotor.GetMaxSpeed();
            steeringContext.acceleration = characterMotor.GetAcceleration();
        }
    }

    private void UpdatePrimaryDesiredVelocityToContext()
    {
        steeringContext.currentPrimaryDesiredVelocity = primaryDesiredVelocity;
    }

    //重置上下文
    private void ResetSteeringContext()
    {
        steeringContext.ResetContext();
    }

    //重置移动意图
    private void ResetMoveIntent()
    {
        moveIntent = MovementType.idle;
    }

    //重置策略Index
    private void ResetStrategyInd()
    {
        currentMovementStrategyInd = -1;
    }

    //开关
    public void ShutDown()
    {
        this.enabled = false;
    }

    public void ReBoot()
    {
        this.enabled = true;
    }

}
