using UnityEngine;

public interface ISteeringBehavior
{
    public SteeringOutput Calculate(SteeringContext context);
}
