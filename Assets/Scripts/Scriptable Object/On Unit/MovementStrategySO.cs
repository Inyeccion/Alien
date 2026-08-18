using UnityEngine;
using System.Collections.Generic;
public class MovementStrategySO : ScriptableObject
{
    public NodeType nodeType;
    public ISteeringBehavior primaryBehavior;
    public List<ISteeringBehavior> auxiliaryBehaviors;
}

