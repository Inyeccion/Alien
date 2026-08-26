using UnityEngine;
//∏¯MovementStategy”√
[CreateAssetMenu(fileName = "SeekBehavior", menuName = "Scriptable Object/Steering Behaviors/Seek Behavior")]
public class SeekBehaviorSO : SteeringBehaviorSO
{
    public override SteeringOutput Calculate(SteeringContext context)
    {
        Vector3 desiredVelocity = (context.targetPos - context.currentPos).normalized * context.maxSpeed;

        return new SteeringOutput(desiredVelocity, Vector3.zero);
    }
}

public abstract class SteeringBehaviorSO : ScriptableObject
{
    public abstract SteeringOutput Calculate(SteeringContext context);
}