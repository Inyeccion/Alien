using UnityEngine;
using System.Collections.Generic;
public class FleeNode : BTNode
{
    public float distanceStopFlee;
    public override NodeState Tick()
    {
        if (AIContext.blackBoard.distanceToThreat < distanceStopFlee)
        {
            AIContext.movementController.SetMoveIntent(MovementType.flee);
            AIContext.movementController.SetSteeringContextThreat(AIContext.blackBoard.threat);
            return NodeState.running;
        }
        return NodeState.succeed;
    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public FleeNode()
    {
        nodeType = NodeType.fleeNode;

        children = new List<BTNode>();
    }

    public void Initialize(float stopFleeDistance, AIContext context)
    {
        distanceStopFlee = stopFleeDistance;
        AIContext = context;
    }

}
