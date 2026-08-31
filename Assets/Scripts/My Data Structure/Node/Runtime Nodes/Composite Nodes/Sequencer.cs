using UnityEngine;
using System.Collections.Generic;
public class Sequencer : BTNode
{
    private int currentChildrenInd = 0;
    public override NodeState Tick()
    {
        for (int i = currentChildrenInd; i < children.Count; i++)
        {
            NodeState state = children[i].Tick();
            if (state == NodeState.failed)
            {
                currentChildrenInd = 0;
                return NodeState.failed;
            }
            if (state == NodeState.running)
            {
                currentChildrenInd = i;
                return NodeState.running;
            }
            currentChildrenInd++;
        }
        currentChildrenInd = 0;
        return NodeState.succeed;
    }

    public override void Reset()
    {
        currentChildrenInd = 0;
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public Sequencer()
    {
        nodeType = NodeType.sequencer;

        children = new List<BTNode>();
    }
}
