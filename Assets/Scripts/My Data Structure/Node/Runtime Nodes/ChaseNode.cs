//ChaseNode不做条件判断，只做逻辑
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : BTNode
{
    private float distanceStopChase;
    public override NodeState Tick()
    {
        //小于仇恨范围，追击
        if (AIContext.blackBoard.distanceToTarget < distanceStopChase)
        {
            AIContext.movementController.SetMoveIntent(MovementType.chase);
            AIContext.movementController.SetSteeringContext(AIContext.blackBoard.target);
            return NodeState.running;
        }
        else return NodeState.failed;
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
