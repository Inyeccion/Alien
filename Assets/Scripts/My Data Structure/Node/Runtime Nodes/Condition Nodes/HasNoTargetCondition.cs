using UnityEngine;
using System.Collections.Generic;

public class HasNoTargetCondition : BTNode
{
    public override NodeState Tick()
    {
        if (AIContext.blackBoard.target == null) return NodeState.succeed;
        return NodeState.failed;
    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public HasNoTargetCondition()
    {
        nodeType = NodeType.hasNoTargetCondition;

        children = new List<BTNode>();
    }

    public void Initialize(AIContext Context)
    {
        AIContext = Context;
    }
}
