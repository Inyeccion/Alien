using UnityEngine;

public abstract class SteeringBehaviorSO : ScriptableObject
{
    public abstract SteeringOutput Calculate(SteeringContext context);
}