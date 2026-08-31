//∏¯MovementStategy”√
using UnityEngine;

[CreateAssetMenu(fileName = "UniversalSeekBehavior", menuName = "Scriptable Object/Steering Behaviors/Universal Seek Behavior")]
public class SeekBehaviorSO : SteeringBehaviorSO
{
    public override SteeringOutput Calculate(SteeringContext context)
    {
        Vector3 desiredVelocity = (context.targetPos - context.currentPos).normalized * context.maxSpeed;

        return new SteeringOutput(desiredVelocity, Vector3.zero);
    }
}