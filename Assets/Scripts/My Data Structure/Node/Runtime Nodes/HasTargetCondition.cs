using UnityEngine;
using System.Collections.Generic;
public class HasTargetCondition : BTNode
{
    public override NodeState Tick()
    {
        if (AIContext.blackBoard.target == null) return NodeState.failed;
        return NodeState.succeed;
    }

    public HasTargetCondition()
    {
        nodeType = NodeType.hasTargetCondition;

        children = new List<BTNode>();
    }

    public void Initialize(AIContext Context)
    {
        AIContext = Context;
    }
}
