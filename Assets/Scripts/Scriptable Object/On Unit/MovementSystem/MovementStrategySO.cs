using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MovementStrategy", menuName = "Scriptable Object/Behavior Tree/Movement Strategy")]
public class MovementStrategySO : ScriptableObject
{
    public MovementType movementType;
    public SteeringBehaviorSO primaryBehavior;
    public List<SteeringBehaviorSO> auxiliaryBehaviors;

    public MovementStrategySO Instantiate()
    {
        MovementStrategySO instance = CreateInstance<MovementStrategySO>();
        instance.movementType = movementType;
        instance.primaryBehavior = Instantiate(primaryBehavior);
        instance.auxiliaryBehaviors = new List<SteeringBehaviorSO>();
        foreach (SteeringBehaviorSO behavior in auxiliaryBehaviors)
        {
            instance.auxiliaryBehaviors.Add(Instantiate(behavior));
        }
        return instance;
    }
}

