using UnityEngine;
using System.Collections.Generic;
public class IdleNode : BTNode
{
    public override NodeState Tick()
    {
        AIContext.movementController.SetMoveIntent(MovementType.idle);
        return NodeState.succeed;
    }

    public IdleNode()
    {
        nodeType = NodeType.idleNode;

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
