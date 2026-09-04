using UnityEngine;
using System.Collections.Generic;

public class WanderNode : BTNode
{
    public override NodeState Tick()
    {
        AIContext.movementController.SetMoveIntent(MovementType.wander);
        return NodeState.succeed;   
    }

    public WanderNode()
    {
        nodeType = NodeType.wanderNode;
        
        children = new List<BTNode>();
    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public void Initialize(AIContext AIContext)
    {
        this.AIContext = AIContext;
    }
}
