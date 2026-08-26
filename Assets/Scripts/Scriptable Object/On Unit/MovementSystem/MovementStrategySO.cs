using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MovementStrategy", menuName = "Scriptable Object/Behavior Tree/Movement Strategy")]
public class MovementStrategySO : ScriptableObject
{
    public MovementType movementType;
    public SteeringBehaviorSO primaryBehavior;
    public List<SteeringBehaviorSO> auxiliaryBehaviors;
}

