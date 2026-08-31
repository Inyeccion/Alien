using System.Collections.Generic;

public class AttackNode : BTNode
{

    public override NodeState Tick()
    {
        return NodeState.succeed;
    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public AttackNode()
    {
        nodeType = NodeType.attackNode;

        children = new List<BTNode>();
    }
}
