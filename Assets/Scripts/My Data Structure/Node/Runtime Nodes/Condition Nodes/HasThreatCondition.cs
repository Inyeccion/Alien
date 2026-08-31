using UnityEngine;
using System.Collections.Generic;

public class HasThreatCondition : BTNode
{
    public override NodeState Tick()
    {
        if (AIContext.blackBoard.threat == null) return NodeState.failed;
        return NodeState.succeed;

    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public HasThreatCondition()
    {
        nodeType = NodeType.hasThreatCondition;

        children = new List<BTNode>();
    }

    public void Initialize(AIContext context)
    {
        AIContext = context;
    }
}
