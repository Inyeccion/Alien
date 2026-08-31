using UnityEngine;

[CreateAssetMenu(fileName = "UniversalFleeBehavior", menuName = "Scriptable Object/Steering Behaviors/Universal Flee Behavior")]
public class FleeBehaviorSO : SteeringBehaviorSO
{
    public override SteeringOutput Calculate(SteeringContext context)
    {
        Vector3 desiredVelocity = (context.currentPos - context.targetPos).normalized * context.maxSpeed;

        return new SteeringOutput(desiredVelocity, Vector3.zero);
    }
}