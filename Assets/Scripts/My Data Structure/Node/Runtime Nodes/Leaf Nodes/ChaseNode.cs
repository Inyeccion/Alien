//ChaseNode不做条件判断，只做逻辑
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : BTNode
{
    private float distanceStopChase;
    public override NodeState Tick()
    {
        if (AIContext.blackBoard.distanceToTarget < distanceStopChase)
        {
            AIContext.movementController.SetMoveIntent(MovementType.idle);
            return NodeState.succeed;
        }
        else
        {
            AIContext.movementController.SetMoveIntent(MovementType.chase);
            AIContext.movementController.SetSteeringContextTarget(AIContext.blackBoard.target);
            return NodeState.running;
        }
    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public ChaseNode()
    {
        nodeType = NodeType.chaseNode;
        children = new List<BTNode>();
    }

    public void Initialize(float stopChaseDistance, AIContext context)
    {
        distanceStopChase = stopChaseDistance;
        AIContext = context;
    }
}
