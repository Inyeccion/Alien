using UnityEngine;

[CreateAssetMenu(fileName = "UniversalIdleBehavior", menuName = "Scriptable Object/Steering Behaviors/Universal Idle Behavior")]
public class IdleBehaviorSO : SteeringBehaviorSO
{
    public override SteeringOutput Calculate(SteeringContext context)
    {
        return new SteeringOutput(Vector3.zero, Vector3.zero);
    }
}
