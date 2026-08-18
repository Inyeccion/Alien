using UnityEngine;

[CreateAssetMenu(fileName = "SeekBehavior", menuName = "Scriptable Object/Steering Behaviors/Seek Behavior")]
public class SeekBehaviorSO : ScriptableObject, ISteeringBehavior
{
    public SteeringOutput Calculate(SteeringContext context)
    {
        Vector3 desiredVelocity = (context.targetPos - context.currentPos).normalized * context.maxSpeed;

        return new SteeringOutput(desiredVelocity, Vector3.zero);
    }
}
